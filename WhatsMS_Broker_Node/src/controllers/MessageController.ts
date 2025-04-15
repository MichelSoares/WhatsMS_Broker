import { RequestHandler } from 'express';
import { sendMessageToWhatsApp } from '../services/WhatsappClient';

export const sendMessage: RequestHandler = async (req, res) => {
  const { phoneNumber, message } = req.body;

  if (!phoneNumber || !message) {
    res.status(400).json({ error: 'Phone number and message are required' });
    return;
  }

  try {
    const response = await sendMessageToWhatsApp(phoneNumber, message);
    res.status(200).json({ message: 'Message sent successfully', response });
  } catch (error) {
    res.status(500).json({
      error: 'Error sending message',
      details: (error as Error).message,
    });
  }
};
