import dotenv from 'dotenv';
//dotenv.config({ path: '.env.development' });
dotenv.config();

import express from 'express';
import logger from '../src/utils/logger';
import { initializeWhatsAppClient } from './services/WhatsappClient';
import { sendMessage } from './controllers/MessageController';

const app = express();
const port = process.env.PORT || 3000;;

app.use(express.json());

//initializeWhatsAppClient();

app.get('/', (req, res) => {
  res.send('WhatsMS Broker Node (TypeScript) está no ar 🚀');
});

app.post('/send-message', sendMessage);

app.listen(port, () => {
  logger.info(`Servidor rodando em http://localhost:${port}`);
});
