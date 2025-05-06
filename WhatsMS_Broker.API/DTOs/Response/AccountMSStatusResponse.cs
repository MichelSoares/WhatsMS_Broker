namespace WhatsMS_Broker.API.DTOs.Response
{
    public class AccountMSStatusResponse
    {
        public int id { get; set; }
        public string auth_token { get; set; }
        public string client_session_id { get; set; }
        public string phone_number { get; set; }
        public bool is_active { get; set; }
    }
}
