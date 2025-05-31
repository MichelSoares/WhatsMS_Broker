import axios, { AxiosInstance, AxiosRequestConfig, Method } from 'axios';
import logger from '../utils/logger';
import https from 'https';
import { AuthTokenService } from '../services/AuthTokenService'; 

export class RequestAPIBroker {
  private client: AxiosInstance;
  private authTokenServiceInstance: AuthTokenService;

  constructor(baseURL: string) {
    this.client = axios.create({
      baseURL,
      timeout: 15000,
      headers: {
        'Content-Type': 'application/json',
      },
      httpsAgent: new https.Agent({ rejectUnauthorized: false })
    });

    this.authTokenServiceInstance = AuthTokenService.getInstance();
  }

  async requestAPI<TResponse = any>(
    method: Method,
    endpoint: string,
    data?: any,
    config?: AxiosRequestConfig
  ): Promise<TResponse | null> {
    try {
      const url = this.client.defaults.baseURL + endpoint;
      logger.info(`Request URL:\t [${method}] - ${url}`);

      const headers: any = { ...(config?.headers || {}) };

      const tokenResponse = await this.authTokenServiceInstance.getToken(); 

      if (tokenResponse && tokenResponse.token) {
        headers['Authorization'] = `Bearer ${tokenResponse.token}`;
      } else {
        logger.error('Falha ao obter o token JWT para a requisição. Requisição não autorizada.');
        return null; 
      }

      const response = await this.client.request<TResponse>({
        method,
        url: endpoint,
        data,
        ...config,
        headers,
      });

      return response.data;
    } catch (error: any) {
      if (axios.isAxiosError(error)) {
        if (error.response && error.response.status === 401) {
            logger.warn(`[RequestAPIBroker] Erro 401 Unauthorized para ${method.toUpperCase()} ${endpoint}. Invalidando token local.`);
            this.authTokenServiceInstance.invalidateToken(); 
        }
        console.error(`[RequestAPIBroker] Error response from ${method.toUpperCase()} ${endpoint}:`, error.response?.data || error.message);
      } else if (error.request) {
        console.error(`[RequestAPIBroker] No response from ${method.toUpperCase()} ${endpoint}:`, error.request);
      } else {
        console.error(`[RequestAPIBroker] Error setting up request for ${method.toUpperCase()} ${endpoint}:`, error.message);
      }
      return null;
    }
  }
}