namespace TestPackages.Messages
{
    public interface ICommand
    {
        Guid CommandId { get; set; }
        Guid CorrelationId { get; set; }
    }
}