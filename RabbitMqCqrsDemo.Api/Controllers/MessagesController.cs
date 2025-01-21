using Microsoft.AspNetCore.Mvc;
using RabbitMqCqrsDemo.Application.Handlers;
using RabbitMqCqrsDemo.Application.Queries;

namespace RabbitMqCqrsDemo.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class MessagesController : ControllerBase
{
    private readonly GetMessageQueryHandler _queryHandler;

    public MessagesController(GetMessageQueryHandler queryHandler)
    {
        _queryHandler = queryHandler;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var query = new GetMessageQuery { MessageId = id };
        var result = await _queryHandler.HandleAsync(query);

        if (result == null)
            return NotFound();

        return Ok(result);
    }
} 