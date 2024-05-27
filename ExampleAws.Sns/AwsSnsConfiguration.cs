namespace ExampleAws.Sns;

public class AwsSnsConfiguration
{

   public String accessKey { get; set; }
   public String secret { get; set; }
   public String queueUrl { get; set; }
   public Boolean useFifo { get; set; }
   public String messageGroupId { get; set; }
   public String region { get; set; }
   public String TopicArn { get; set; }
}