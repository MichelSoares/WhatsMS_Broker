import { RequestHandler } from 'express';
import { sendMessageToWhatsApp } from '../services/WhatsappClient';
import { SendMessageDTO } from '../types/DTOs/Request/SendMessageDTO';

export const sendMessage: RequestHandler = async (req, res) => {
  const msg = req.body as SendMessageDTO;

  if (!msg.toNumber || !msg.content) {
    res.status(400).json({ error: 'Bad request - phoneNumber e Body' });
    return;
  }

  try {
    const response = await sendMessageToWhatsApp(msg.toNumber, msg.content);
    res.status(200).json({ message: 'Mensagem enviada com sucesso', response });
  } catch (error) {
    res.status(500).json({
      error: 'Error sending message',
      details: (error as Error).message,
    });
  }
};
