namespace TestTwoAPI.Configurations
{
    public class ConnectionStringsOptions
    {
        public const string ConnectionStrings = "ConnectionStrings";

        public string RabbitMq { get; set; }
        public string MySQL { get; set; }
    }
}
