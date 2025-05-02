export interface MessageInboundDTO {
    //id: number;
    idMessageWhatsApp: string;
    accountId: number;
    fromNumber: string;
    toNumber: string;
    messageType: string;
    content: string;
    dateReceived: string;
    isGroup: boolean;
  }