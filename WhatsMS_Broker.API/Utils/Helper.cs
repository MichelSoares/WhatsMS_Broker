namespace WhatsMS_Broker.API.Utils
{
    public static class Helper
    {
        public static DateTime FormatUnixTimeToDate(string unixTimeString)
        {
            if (!long.TryParse(unixTimeString, out long unixTime))
                throw new ArgumentException("Formato de timestamp inválido.");

            return DateTimeOffset.FromUnixTimeSeconds(unixTime).UtcDateTime;
        }
    }
}
