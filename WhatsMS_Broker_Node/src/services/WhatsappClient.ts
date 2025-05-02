import { Client, LocalAuth } from 'whatsapp-web.js';
import qrcode from 'qrcode-terminal';
import logger from '../utils/logger';
import path from 'path';
import { RequestAPIBroker } from './RequestAPIBroker';
import { QRCode } from '../types/Entities/QRCode';
import { MessageInboundDTO } from '../types/DTOs/Request/MessageInboundDTO';
import { AccountWhatsMSDTO } from '../types/DTOs/Response/AccountWhatsMSDTO';
import { Helper } from '../utils/Helper';

const sender = process.env.PHONE_NUMBER;
const baseUrlAPI = process.env.URL_BASE_API_BROKER;
const portApp = process.env.PORT;

const doRequetBrokerAPI = new RequestAPIBroker(baseUrlAPI || '');
const qrcode_url = require('qrcode');
//const os = require('os');
const fs = require('fs').promises;
//const clientWhatsMS: object;
let accountWhatsMS: AccountWhatsMSDTO | null;;
let client: Client;
let sessionData: any;
let isAuth: boolean;
let isConnecting = false;

const SESSION_FILE_PATH = path.join(__dirname, '..', 'sessions');

const argsPuppeteer = 
[ 
  '--no-sandbox', 
  '--disable-setuid-sandbox',
  '--disable-dev-shm-usage',
  '--disable-accelerated-2d-canvas',
  '--no-first-run',
  '--no-zygote',
  '--single-process',
  '--disable-gpu'
];

const initializeWhatsAppClient = () => {
  client.on('qr', (qr) => {
    qrcode_url.toDataURL(qr).then(async (url: string) => {
      let qrcode_b64 = url.split(',');
      if(qrcode_b64[1] != null){
        try{

          let hasQRCode = false;
          let numberExists = await doRequetBrokerAPI.requestAPI('GET', 'ClientWhatsMS/check-phonenumber-exists/', undefined, { params: { phoneNumber: sender } });

          if(numberExists === 1){
            let startUp = await doRequetBrokerAPI.requestAPI('GET', 'ClientWhatsMS/check-uptime-qrcode/', undefined, { params: { phoneNumber: sender } });
            if(startUp.genQrcode){
              logger.info('[QR Code] - GERANDO/ATUALIZANDO');
              qrcode.generate(qr, { small: true });
              logger.info('[QR Code] Escaneie o código acima para logar no WhatsApp Web.');

              if (!portApp || !sender) {
                throw new Error('Variáveis de ambiente não definidas');
              }

              const qrcodeJ: QRCode = {
                qrcode_b64: qrcode_b64[1],
                porta: portApp!,
                phoneNumber: sender!
              }

              await doRequetBrokerAPI.requestAPI('PUT', `ClientWhatsMS/${sender}/qrcode`,
                  {
                    QRCode: qrcodeJ.qrcode_b64,
                    Port: qrcodeJ.porta
                  });
              hasQRCode = true;

            } 
            else {
              logger.warn('Updated_At acima de 1 minuto. QR Code não será gerado! aguardando nova solicitação!');
              if(hasQRCode){
                await doRequetBrokerAPI.requestAPI('PUT', `ClientWhatsMS/${sender}/reset-qrcode`);
                hasQRCode = false;
              }
            }
            
          }
          else {
            logger.warn(`Nenhum row correspondente, para o número: +${sender} . QR Code não gerado, Finalizando programa!`);
            process.exit(1);
          }

        } catch(e){
          logger.error(e);
        }
      }  
    });
 
  });

  client.on('authenticated', async (session) => {
    sessionData = session;
    isAuth = true;
  });

  client.on('auth_failure', async (msg) => {
    logger.error('Erro autenticação', msg);
    await deletarFileSession(SESSION_FILE_PATH)

    setTimeout(() => {
      connectWpp()
    }, 3000)
  });

  client.on('disconnected', async (reason) => {
    if (isAuth) {
      try {
        sessionData = null;
        await doRequetBrokerAPI.requestAPI('PUT', `ClientWhatsMS/${client.info.wid.user}/reset-qrcode`);
        logger.info('Cliente desconectou, número: ' + client.info.wid.user);
        logger.info('Motivo desconexão: ' + reason);
        await resetClient();
        //await deletarFileSession(SESSION_FILE_PATH);
        setTimeout(() => {
          connectWpp()
        }, 3000)

      } catch (error) {
        logger.info('Erro em terminar a sessão %s', error)
      }
    } else {
      logger.info('Por favor autentique-se com o número pré-cadastrado. Solicitando autenticação novamente...')
    }
  });

  client.on('message_ack', async (message, ack) => {

  });

  client.on('ready', async () => {
    if (isAuth && `${client.info.wid.user}` === `${sender}`) { 
        logger.info('Versão WhatsApp Web: ' + await client.getWWebVersion());
        logger.info('Profile Name cliente: ' + client.info.pushname);
        await doRequetBrokerAPI.requestAPI('PUT', `ClientWhatsMS/${client.info.wid.user}/set-auth`);
    } else {
        logger.info('Tentativa de login com número não autorizado. Desconectando...');
        await deletarFileSession(SESSION_FILE_PATH);
        setTimeout(() => {
            client.logout();
            isAuth = false;
            connectWpp();
        }, 3000);
    }
    logger.info('[WhatsApp] Cliente pronto!');
});

  client.on('message', async (message) => {
    logger.info(`[Mensagem recebida] ${message.from}: ${message.body}`);
    
    logger.info(`obj integro evento message Whats: ${JSON.stringify(message)}`);

    if(!accountWhatsMS){
      logger.warn("CLIENT accountWhatsMS null;")
      return;
    }

    const msgInboundDTO: MessageInboundDTO = {
      idMessageWhatsApp: message.id.id,
      accountId: accountWhatsMS.id,
      fromNumber: message.from,
      toNumber: message.to,
      messageType: message.type,
      content: message.body,
      dateReceived: Helper.formatUnixToBR(message.timestamp),
      isGroup: false
    };

    await doRequetBrokerAPI.requestAPI('POST', 'MessageInbound/MessageReceived/', msgInboundDTO);

  });

  client.initialize();
  logger.info('Inicializando Cliente...');
};

export async function connectWpp(forceNewSession = false) {
  if (isConnecting) {
    logger.warn('Já existe uma conexão em andamento. Ignorando novo connectWpp.');
    return;
  }

  isConnecting = true;
  try {
    logger.info(`DIRETORIO sessões -> ${SESSION_FILE_PATH}`);

    accountWhatsMS = await doRequetBrokerAPI.requestAPI<AccountWhatsMSDTO>('GET', 'ClientWhatsMS/check-status/', undefined, {
      params: { phoneNumber: sender }
    });

    if (!accountWhatsMS) {
      logger.warn("Retorno nulo da API. Abortando recuperação de sessão.");
      return;
    }

    if (accountWhatsMS.is_active && accountWhatsMS.client_session_id != null && !forceNewSession) {
      let client_id_db = accountWhatsMS.client_session_id;
      logger.info(`VERIFICANDO SE SESSÃO ESTÁ AUTENTICADA: ${accountWhatsMS.is_active}`);
      logger.info('RECUPERANDO SESSÃO...');
      logger.info(`CLIENTE: ${sender} tem sessão ATIVA! RECUPERANDO SESSÃO - session-${client_id_db}`);
      await Restore_Session(client_id_db);

    } else {
      logger.info('NÃO EXISTE SESSÃO PARA RECUPERAR - criando nova sessão client ID');
      logger.info('DELETANDO DADOS DE SESSÕES INATIVAS...');
      
      await deletarFileSession(SESSION_FILE_PATH);
      await resetClient();
    
      let client_new_session = "client-" + Math.floor(Math.random() * 1000);
      logger.info(`NOVA SESSÃO CLIENT ID: ${client_new_session}`);
      
      client = new Client({
        takeoverOnConflict: true,
        takeoverTimeoutMs: 0,
        webVersion: '2.2408.1',
        webVersionCache: { type: "local" },
        puppeteer: { headless: true, args: argsPuppeteer },
        authStrategy: new LocalAuth({ dataPath: SESSION_FILE_PATH, clientId: client_new_session }),
        session: sessionData
      });

      await doRequetBrokerAPI.requestAPI('PUT', `ClientWhatsMS/${sender}/${client_new_session}/new-session`);
    }

    initializeWhatsAppClient();

  } catch (e) {
    logger.error('Erro no connectWpp:', e);
  } finally {
    isConnecting = false;
  }
}

async function resetClient() {
  if (client) {
    try {
      await client.destroy();
      client.removeAllListeners();
    } catch (error) {
      logger.error('Erro ao resetar o cliente:', error);
    }
  }
}

async function Restore_Session(client_id : string){
  client = new Client({
    takeoverOnConflict: true,
    takeoverTimeoutMs:  0,
    webVersion: '2.2408.1',
    webVersionCache:  { type: "local" },
    puppeteer: { headless: true, args: argsPuppeteer },
    authStrategy: new LocalAuth({dataPath: SESSION_FILE_PATH, clientId: client_id })             
  });
}

async function deletarFileSession(directoryPath : string) {
  try {
    const files = await fs.readdir(directoryPath);
    for (const file of files) {
      const filePath = path.join(directoryPath, file);
      await fs.rm(filePath, { recursive: true });
      logger.info(`${filePath} - Foi deletado`);
    }
    logger.info('Conteúdo do diretório foi deletado.');
  } catch (err) {
    logger.error(`Erro ao deletar conteúdo do diretório: ${err}`);
  }
}

//---
export const sendMessageToWhatsApp = async (phoneNumber: string, message: string) => {
  if (!client) {
    throw new Error('Cliente WhatsApp não inicializado!');
  }
  // WhatsApp aqui vou esperar o num no formato -> (DDI + DDD + número) +  @c.us
  const chatId = `${phoneNumber}@c.us`;

  return client.sendMessage(chatId, message);
};


