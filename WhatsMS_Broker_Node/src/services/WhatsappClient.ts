import { Client, LocalAuth } from 'whatsapp-web.js';
import qrcode from 'qrcode-terminal';
import logger from '../utils/logger';
import { RequestAPIBroker } from './RequestAPIBroker';
import path from 'path';
const doRequetBrokerAPI = new RequestAPIBroker(process.env.URL_BASE_API_BROKER || '');
const os = require('os');
const fs = require('fs').promises;

let client: Client;
const SESSION_FILE_PATH = path.join(__dirname, '..', 'sessions');
logger.info(`DIRETORIO sessões ${SESSION_FILE_PATH}`);

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

export const initializeWhatsAppClient = () => {
  /*client = new Client({
    authStrategy: new LocalAuth(),
    puppeteer: {
      headless: true,
      args: argsPuppeteer,
    },
  });*/

  logger.info('Inicializando Cliente');

  client.on('qr', (qr) => {
    qrcode.generate(qr, { small: true });
    logger.info('[QR Code] Escaneie o código acima para logar no WhatsApp Web.');
  });

  client.on('ready', () => {
    logger.info('[WhatsApp] Cliente pronto!');
  });

  client.on('message', (message) => {
    logger.info(`[Mensagem recebida] ${message.from}: ${message.body}`);
  });

  client.initialize();
};

async function connectWpp(forceNewSession = false) {
  
  try{
    
    let sessionData = null;
    let ret = await doRequetBrokerAPI.requestAPI('GET', 'ClientWhatsMS/check-status/', undefined, {
      params: { phoneNumber: process.env.PHONE_NUMBER }
    });
    
    if(ret.is_active && ret.client_session_id != null && forceNewSession == false){
      let client_id_db = ret.client_session_id;
      logger.info('RECUPERANDO SESSÃO...');
      logger.info('VERIFICANDO SE SESSÃO ESTÁ AUTENTICADA: ' + ret.is_active);
      logger.info(`CLIENTE: ${process.env.NUMERO_CLIENTE} tem sessão ATIVA! RECUPERANDO SESSÃO - session-${client_id_db}`);

      await Restore_Session(client_id_db);


    } else {
      logger.info('NÃO EXISTE SESSÃO PARA RECUPERAR - criando nova sessão client ID');
      logger.info('DELETANDO DADOS DE SESSÕES INATIVAS...');
      await deletarFileSession(SESSION_FILE_PATH);
      let client_new_session = "client-"+ Math.floor(Math.random() * 1000)
        client = new Client({
          takeoverOnConflict: true,
          takeoverTimeoutMs:  0,
          webVersion: '2.2408.1',
          webVersionCache:  { type: "local" },
          puppeteer: { headless: true, args: argsPuppeteer },
          authStrategy: new LocalAuth({dataPath: SESSION_FILE_PATH, clientId: client_new_session}),
          //session: sessionData
        });
    }

    logger.info(`DIRETÓRIO SESSÃO: ${SESSION_FILE_PATH}`);
    initializeWhatsAppClient();

  } catch(e){

  }
}

connectWpp(true);

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
  // WhatsApp espera o número com DDI + DDD + número, e sufixo @c.us
  const chatId = `${phoneNumber}@c.us`;

  return client.sendMessage(chatId, message);
};


