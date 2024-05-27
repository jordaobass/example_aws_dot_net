namespace ExampleAws.Sqs;

public interface ISqsMessageConsumer
{
    Task Listen();
}