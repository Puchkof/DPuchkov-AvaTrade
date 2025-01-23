using AvaTrade.News.Application.DTOs;
using AvaTrade.News.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AvaTrade.News.API.Controllers;

[ApiController]
[Route("api/public/news")]
public class PublicNewsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<PublicNewsController> _logger;

    public PublicNewsController(IMediator mediator, ILogger<PublicNewsController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet("latest")]
    public async Task<ActionResult<IEnumerable<NewsItemDto>>> GetLatestPublic()
    {
        var query = new GetLatestTopInstrumentsNewsQuery();
        var result = await _mediator.Send(query);
        return Ok(result);
    }
} 