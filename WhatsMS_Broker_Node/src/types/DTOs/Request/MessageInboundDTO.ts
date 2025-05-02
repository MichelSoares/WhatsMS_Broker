export interface MessageInboundDTO {
    //id: number;
    idMessageWhatsApp: string;
    accountId: number;
    dateReceived: string;
    fromNumber: string;
    toNumber: string;
    messageType: string;
    content: string;
    type: string;
    midiaContentType?: string;
    midiaURL?: string;
    profileName?: string;
    NotifyName?: string;
    author?: string;
    latitude: number;
    longitude: number;
    isForwarded: boolean;
    forwardingScore: number;
    isGroup: boolean;
  }