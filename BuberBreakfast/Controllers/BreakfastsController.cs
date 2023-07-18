using Microsoft.AspNetCore.Mvc;
using ErrorOr;

using BuberBreakfast.Contracts.Breakfast;
using BuberBreakfast.Services.Breakfasts;
using BuberBreakfast.Models;

namespace BuberBreakfast.Controllers;

public class BreakfastsController : ApiController
{
    private readonly IBreakfastService _breakfastService;

    private static BreakfastResponse MapBreakfastResponse(Breakfast breakfast)
    {
        return new BreakfastResponse(
            breakfast.Id,
            breakfast.Name,
            breakfast.Description,
            breakfast.StartDateTime,
            breakfast.EndDateTime,
            breakfast.LastModifiedDateTime,
            breakfast.Savory,
            breakfast.Sweet
        );
    }

    private IActionResult CreatedAtGetBreakfast(Breakfast breakfast)
    {
        return CreatedAtAction(
            actionName: nameof(GetBreakfast),
            routeValues: new { id = breakfast.Id },
            value: MapBreakfastResponse(breakfast)
        );
    }

    public BreakfastsController(IBreakfastService breakfastService)
    {
        _breakfastService = breakfastService;
    }

    [HttpPost]
    public IActionResult CreateBreakfast(CreateBreakfastRequest request)
    {
        ErrorOr<Breakfast> breakfastCreation = Breakfast.Create(
            request.Name,
            request.Description,
            request.StartDateTime,
            request.EndDateTime,
            request.Savory,
            request.Sweet
        );

        if (breakfastCreation.IsError)
        {
            return Problem(breakfastCreation.Errors);
        }

        Breakfast breakfast = breakfastCreation.Value;

        ErrorOr<Created> createdResult = _breakfastService.CreateBreakfast(breakfast);

        if (createdResult.IsError)
        {
            return Problem(createdResult.Errors);
        }

        return CreatedAtGetBreakfast(breakfast);
    }

    [HttpGet]
    public IActionResult ListBreakfasts()
    {
        ErrorOr<List<Breakfast>> listBreakfastsResult = _breakfastService.ListBreakfasts();

        return listBreakfastsResult.Match(
            breakfasts => Ok(breakfasts.Select(MapBreakfastResponse)),
            errors => Problem(errors)
        );
    }

    [HttpGet("{id:guid}")]
    public IActionResult GetBreakfast(Guid id)
    {
        ErrorOr<Breakfast> getBreakfastResult = _breakfastService.GetBreakfast(id);
        return getBreakfastResult.Match(
            breakfast => Ok(MapBreakfastResponse(breakfast)),
            errors => Problem(errors)
        );
    }

    [HttpPut("{id:guid}")]
    public IActionResult UpsertBreakfast(Guid id, UpsertBreakfastRequest request)
    {
        ErrorOr<Breakfast> breakfastCreation = Breakfast.Create(
            request.Name,
            request.Description,
            request.StartDateTime,
            request.EndDateTime,
            request.Savory,
            request.Sweet
        );

        if (breakfastCreation.IsError)
        {
            return Problem(breakfastCreation.Errors);
        }

        Breakfast breakfast = breakfastCreation.Value;

        ErrorOr<UpsertBreakfast> upsertResult = _breakfastService.UpsertBreakfast(breakfast);
        
        return upsertResult.Match(
            upserted => upserted.isNewlyCreated ? CreatedAtGetBreakfast(breakfast) : NoContent(),
            errors => Problem(errors)
        );
    }

    [HttpDelete("{id:guid}")]
    public IActionResult DeleteBreakfast(Guid id)
    {
        ErrorOr<Deleted> deletedBreakfastResult = _breakfastService.DeleteBrakFast(id);

        return deletedBreakfastResult.Match(
            deleted => NoContent(),
            errors => Problem(errors)
        );
    }
}