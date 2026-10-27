using FluentValidation;
using FluentValidation.Results;

using Microsoft.AspNetCore.Mvc;

using ExpenseTracker.Api.Features.Budget.Accounts.Requests;

namespace ExpenseTracker.Api.Features.Budget.Accounts;

[ApiController]
[Route("budget/[controller]")]
public sealed class AccountsController : ControllerBase
{
    private readonly BudgetAccountService _budgetAccountService;
    private readonly BudgetService _budgetService;

    private readonly IValidator<CreateBudgetAccountRequest> _createValidator;
    private readonly IValidator<UpdateBudgetAccountRequest> _updateValidator;

    public AccountsController(
        BudgetAccountService budgetAccountService,
        BudgetService budgetService,
        IValidator<CreateBudgetAccountRequest> createValidator,
        IValidator<UpdateBudgetAccountRequest> updateValidator)
    {
        _budgetAccountService = budgetAccountService;
        _budgetService = budgetService;
        _createValidator = createValidator;
        _updateValidator = updateValidator;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<BudgetAccount>>> GetAllAsync(
        CancellationToken cancellationToken)
    {
        IReadOnlyList<BudgetAccount> accounts =
            await _budgetAccountService.GetAllAsync(cancellationToken);

        return Ok(accounts);
    }

    [HttpGet("{budgetAccountId:int}")]
    public async Task<ActionResult<BudgetAccount>> GetByIdAsync(
        [FromRoute] int budgetAccountId,
        CancellationToken cancellationToken)
    {
        BudgetAccount account = await _budgetAccountService.GetByIdAsync(
            budgetAccountId,
            cancellationToken);

        return Ok(account);
    }

    [HttpPost]
    public async Task<ActionResult<BudgetAccount>> CreateAsync(
        [FromBody] CreateBudgetAccountRequest request,
        CancellationToken cancellationToken)
    {
        ValidationResult validationResult =
            await _createValidator.ValidateAsync(
                request,
                cancellationToken);

        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.ToDictionary());
        }

        BudgetAccount budgetAccount = await _budgetService.CreateBudgetAccountAsync(
            request,
            cancellationToken);

        return Created(
            $"{Request.Path}/{budgetAccount.BudgetAccountId}",
            budgetAccount);
    }

    [HttpPatch("{budgetAccountId:int}")]
    public async Task<ActionResult<BudgetAccount>> UpdateAsync(
        [FromRoute] int budgetAccountId,
        [FromBody] UpdateBudgetAccountRequest request,
        CancellationToken cancellationToken)
    {
        ValidationResult validationResult =
            await _updateValidator.ValidateAsync(
                request,
                cancellationToken);

        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.ToDictionary());
        }

        BudgetAccount budgetAccount = await _budgetService.UpdateBudgetAccountAsync(
            budgetAccountId,
            request,
            cancellationToken);

        return Ok(budgetAccount);
    }

    [HttpDelete("{budgetAccountId:int}")]
    public async Task<ActionResult> DeleteAsync(
        [FromRoute] int budgetAccountId,
        CancellationToken cancellationToken)
    {
        await _budgetAccountService.DeleteAsync(
            budgetAccountId,
            cancellationToken);

        return NoContent();
    }
}
