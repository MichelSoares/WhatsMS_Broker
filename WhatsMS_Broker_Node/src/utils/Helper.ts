export class Helper {
    static formatUnixToBR2(unixTimestamp: number): string {
      const date = new Date(unixTimestamp * 1000);
  
      return date.toLocaleString('pt-BR', {
        timeZone: 'America/Sao_Paulo',
        year: 'numeric',
        month: '2-digit',
        day: '2-digit',
        hour: '2-digit',
        minute: '2-digit',
        second: '2-digit',
        hour12: false,
      });
    }

    public static formatUnixToBR(unixTime: number): string {
        return new Date(unixTime * 1000).toISOString();
      }
  }
  