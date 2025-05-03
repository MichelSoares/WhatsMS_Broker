export interface SendMessageDTO{
  //id: number;
  idMsg: string;
  accountId: number;
  account: any;
  createdAt: string;
  sentAt: string;
  fromNumber: string;
  toNumber: string;
  messageType: string;
  content: string;
  type?: string;
  midiaContentType?: string;
  midiaURL?: string;
  latitude?: number;
  longitude?: number;
  isGroup: boolean;
  status: string;
}