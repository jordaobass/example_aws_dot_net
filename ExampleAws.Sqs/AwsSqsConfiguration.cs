namespace ExampleAws.Sqs;

public class AwsSqsConfiguration
{

   public String accessKey { get; set; }
   public String secret { get; set; }
   public String queueUrl { get; set; }
   public Boolean useFifo { get; set; }
   public String messageGroupId { get; set; }
   public String region { get; set; }
}