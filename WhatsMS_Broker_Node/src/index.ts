import dotenv from 'dotenv';
//dotenv.config({ path: '.env.development' });
dotenv.config();

import express from 'express';
import logger from '../src/utils/logger';
import { connectWpp } from './services/WhatsappClient';
import { sendMessage } from './controllers/MessageController';

const app = express();
const port = process.env.PORT || 3000;;
app.use(express.json());

app.get('/', (req, res) => {
  res.send('WhatsMS Broker Node está no rodando!');
});

app.post('/send-message', sendMessage);

app.listen(port, () => {
  logger.info(`Servidor rodando em http://localhost:${port}`);
});

connectWpp();
