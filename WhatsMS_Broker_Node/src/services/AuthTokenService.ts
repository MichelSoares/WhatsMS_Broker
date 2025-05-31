import axios, { AxiosInstance } from 'axios';
import logger from '../utils/logger';
import dotenv from 'dotenv';
import { TokenJwtResponse } from '../types/DTOs/Response/TokenJwtResponse';

dotenv.config();

interface GetTokenResult {
  token: string | null;
  expiresAt: Date | null; 
}

class AuthTokenService {
  private static instance: AuthTokenService;
  private token: string | null = null;
  private expiresAt: Date | null = null;
  private isFetchingToken: boolean = false;
  private fetchPromise: Promise<GetTokenResult | null> | null = null;
  private refreshTimeout: NodeJS.Timeout | null = null;

  private readonly TOKEN_REFRESH_THRESHOLD_MS = (60 * 1000) * 5;
  private static readonly API_BASE_URL = process.env.URL_BASE_API_BROKER;
  private static readonly API_EMAIL = process.env.API_EMAIL;
  private static readonly API_PASS = process.env.API_PASS;

  private authClient: AxiosInstance;

  private constructor() {
    if (!AuthTokenService.API_BASE_URL) {
      logger.error("URL_BASE_API_BROKER não definido nas variáveis de ambiente.");
      throw new Error("URL_BASE_API_BROKER não definido.");
    }
    if (!AuthTokenService.API_EMAIL || !AuthTokenService.API_PASS) {
        logger.error("Credenciais de API (API_EMAIL ou API_PASS) não definidas nas variáveis de ambiente.");
        throw new Error("Credenciais de API não definidas.");
    }

    this.authClient = axios.create({
      baseURL: AuthTokenService.API_BASE_URL,
      timeout: 10000,
      headers: {
        'Content-Type': 'application/json',
      },
      
      httpsAgent: new (require('https').Agent)({ rejectUnauthorized: false })
    });
  }

  public static getInstance(): AuthTokenService {
    if (!AuthTokenService.instance) {
      AuthTokenService.instance = new AuthTokenService();
    }
    return AuthTokenService.instance;
  }

  private isTokenValid(): boolean {
    if (!this.token || !this.expiresAt) {
      return false;
    }
    const now = new Date().getTime();
    const expiryTime = this.expiresAt.getTime();
    
    return expiryTime - now > this.TOKEN_REFRESH_THRESHOLD_MS;
  }

  private async fetchAndScheduleToken(): Promise<GetTokenResult | null> {
    if (this.isFetchingToken && this.fetchPromise) {
      return this.fetchPromise;
    }

    this.isFetchingToken = true;
    this.fetchPromise = (async (): Promise<GetTokenResult | null> => {
      try {
        logger.info('Obtendo/Renovando token JWT...');
        const loginBody = {
          email: AuthTokenService.API_EMAIL,
          senha: AuthTokenService.API_PASS,
        };

        const response = await this.authClient.post<TokenJwtResponse>('Login/login', loginBody);
        
        const { token, expiresAt } = response.data; // Pega o campo 'expiresAt'

        this.token = token;
        const [datePart, timePart] = expiresAt.split(' ');
        const [day, month, year] = datePart.split('/');
        const isoFormattedDate = `${year}-${month}-${day}T${timePart}`;
        this.expiresAt = new Date(isoFormattedDate);

        if (isNaN(this.expiresAt.getTime())) {
            logger.error(`Falha ao parsear 'expiresAt' da API: ${expiresAt}.`);
            throw new Error("Data de expiração inválida da API.");
        }
        
        logger.info(`Novo token JWT obtido. Expira em: ${this.expiresAt.toLocaleString()}`);
        
        if (this.refreshTimeout) {
          clearTimeout(this.refreshTimeout);
        }

        const timeUntilRefresh = (this.expiresAt.getTime() - Date.now()) - this.TOKEN_REFRESH_THRESHOLD_MS;
        if (timeUntilRefresh > 0) {
          logger.info(`Agendando renovação do token em ${Math.floor(timeUntilRefresh / 1000 / 60)} minutos.`);
          this.refreshTimeout = setTimeout(() => {
            this.fetchAndScheduleToken(); 
          }, timeUntilRefresh);
        } else {
          logger.warn('Tempo de vida do token é menor que o threshold de renovação ou já expirou. Renovando imediatamente na próxima vez.');
        }

        return { token, expiresAt: this.expiresAt };

      } catch (error: any) {
        logger.error('Erro ao obter/renovar token JWT:', error.response ? error.response.data : error.message);
        this.token = null;
        this.expiresAt = null;
        if (this.refreshTimeout) {
            clearTimeout(this.refreshTimeout);
            this.refreshTimeout = null;
        }
        return null;
      } finally {
        this.isFetchingToken = false;
        this.fetchPromise = null;
      }
    })();

    return this.fetchPromise;
  }

  public async getToken(): Promise<GetTokenResult | null> {
    if (this.isTokenValid()) {
      //logger.info('Token JWT em cache ainda válido.');
      return { token: this.token, expiresAt: this.expiresAt };
    }
    return this.fetchAndScheduleToken();
  }

  public invalidateToken(): void {
    this.token = null;
    this.expiresAt = null;
    if (this.refreshTimeout) {
      clearTimeout(this.refreshTimeout);
      this.refreshTimeout = null;
    }
    logger.info('Token JWT invalidado e agendamento de renovação cancelado.');
  }
}

export { AuthTokenService };