namespace WhatsMS_Broker.API.DTOs.Response
{
    public class AccountMSStatusDTO
    {
        public string client_session_id { get; set; }
        public string phone_number { get; set; }
        public bool is_active { get; set; }
    }
}
