import axios, { AxiosInstance, AxiosRequestConfig, Method } from 'axios';
import logger from '../utils/logger';
import https from 'https';

export class RequestAPIBroker {
  private client: AxiosInstance;

  constructor(baseURL: string) {
    this.client = axios.create({
      baseURL,
      timeout: 15000,
      headers: {
        'Content-Type': 'application/json',
      },
      httpsAgent: new https.Agent({ rejectUnauthorized: false })
    });
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
  
      const response = await this.client.request<TResponse>({
        method,
        url: endpoint, 
        data,
        ...config,
      });

      //logger.info(`Response: ${JSON.stringify(response.data)}`);

      return response.data;
    } catch (error: any) {
      if (error.response) {
        console.error(`[RequestAPIBroker] Error response from ${method.toUpperCase()} ${endpoint}:`, error.response.data);
      } else if (error.request) {
        console.error(`[RequestAPIBroker] No response from ${method.toUpperCase()} ${endpoint}:`, error.request);
      } else {
        console.error(`[RequestAPIBroker] Error setting up request for ${method.toUpperCase()} ${endpoint}:`, error.message);
      }
      return null;
    }
  }
}
