using ExampleAws.Sqs;

namespace ExampleAws.Api.Controllers;

using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class ExampleSqsController : ControllerBase
{
    private ISqsMessageProducer _sqsMessageProducer;
    private ISqsMessageConsumer _sqsMessageConsumer;


    public ExampleSqsController(ISqsMessageProducer sqsMessageProducer,ISqsMessageConsumer sqsMessageConsumer)
    {
        _sqsMessageProducer = sqsMessageProducer;
        _sqsMessageConsumer = sqsMessageConsumer;
    }


    [HttpGet(Name = "producer")]
    public async Task<IActionResult> GetProducer()
    {
        await _sqsMessageProducer.Send("oi");

        return Ok("ok");
    }
    
    [HttpGet(Name = "consumer")]
    public async Task<IActionResult> GetConsumer()
    {
        await _sqsMessageConsumer.Listen();

        return Ok("ok");
    }
    
}