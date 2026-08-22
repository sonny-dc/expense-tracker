using FluentValidation;
using FluentValidation.Results;

using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.Api.Features.Items;

[ApiController]
[Route("[controller]")]
public sealed class ItemsController : ControllerBase
{
    private readonly ItemService _itemService;

    private readonly IValidator<CreateItemRequest> _createItemRequestValidator;
    private readonly IValidator<UpdateItemRequest> _updateItemRequestValidator;

    public ItemsController(
        ItemService itemService,
        IValidator<CreateItemRequest> createItemRequestValidator,
        IValidator<UpdateItemRequest> updateItemRequestValidator)
    {
        _itemService = itemService;
        _createItemRequestValidator = createItemRequestValidator;
        _updateItemRequestValidator = updateItemRequestValidator;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<Item>>> GetAllAsync(
        CancellationToken cancellationToken)
    {
        IReadOnlyList<Item> items = await _itemService.GetAllAsync(
            cancellationToken);

        return Ok(items);
    }

    [HttpGet("{itemId:int}")]
    public async Task<ActionResult<Item>> GetByIdAsync(
        [FromRoute] int itemId,
        CancellationToken cancellationToken)
    {
        Item item = await _itemService.GetByIdAsync(
            itemId,
            cancellationToken);

        return Ok(item);
    }

    [HttpPost]
    public async Task<ActionResult<Item>> CreateAsync(
        [FromBody] CreateItemRequest request,
        CancellationToken cancellationToken)
    {
        ValidationResult validationResult =
            await _createItemRequestValidator.ValidateAsync(
                request,
                cancellationToken);
        
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.ToDictionary());
        }

        Item createdItem = await _itemService.CreateAsync(
            request,
            cancellationToken);

        return Created(
            $"{Request.Path}/{createdItem.ItemId}",
            createdItem);
    }

    [HttpPatch("{itemId:int}")]
    public async Task<ActionResult<Item>> UpdateAsync(
        [FromRoute] int itemId,
        [FromBody] UpdateItemRequest request,
        CancellationToken cancellationToken)
    {
        ValidationResult validationResult =
            await _updateItemRequestValidator.ValidateAsync(
                request,
                cancellationToken);

        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.ToDictionary());
        }

        Item updatedItem = await _itemService.UpdateAsync(
            itemId,
            request,
            cancellationToken);

        return Ok(updatedItem);
    }

    [HttpDelete("{itemId:int}")]
    public async Task<IActionResult> DeleteAsync(
        [FromRoute] int itemId,
        CancellationToken cancellationToken)
    {
        await _itemService.DeleteAsync(
            itemId,
            cancellationToken);

        return NoContent();
    }
}
