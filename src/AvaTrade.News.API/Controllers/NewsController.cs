using AvaTrade.News.Application.DTOs;
using AvaTrade.News.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AvaTrade.News.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class NewsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<NewsController> _logger;

    public NewsController(IMediator mediator, ILogger<NewsController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<NewsItemDto>>> GetLatest([FromQuery] int limit = 10)
    {
        var query = new GetLatestNewsQuery(limit);
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("instrument/{instrumentName}")]
    public async Task<ActionResult<IEnumerable<NewsItemDto>>> GetByInstrument(
        string instrumentName,
        [FromQuery] int limit = 10)
    {
        var query = new GetNewsByInstrumentQuery(instrumentName, limit);
        var result = await _mediator.Send(query);
        return Ok(result);
    }
} 