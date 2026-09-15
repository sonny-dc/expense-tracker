using FluentValidation;
using FluentValidation.Results;

using Microsoft.AspNetCore.Mvc;

using ExpenseTracker.Api.Features.Expenses.Requests;

namespace ExpenseTracker.Api.Features.Expenses;

[ApiController]
[Route("[controller]")]
public sealed class ExpensesController : ControllerBase
{
    private readonly ExpenseService _expenseService;

    private readonly IValidator<CreateExpenseRequest> _createExpenseRequestValidator;

    public ExpensesController(
        ExpenseService expenseService,
        IValidator<CreateExpenseRequest> createExpenseRequestValidator)
    {
        _expenseService = expenseService;
        _createExpenseRequestValidator = createExpenseRequestValidator;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<ExpenseResult>>> GetAllAsync(
        CancellationToken cancellationToken)
    {
        IReadOnlyList<ExpenseResult> expenses =
            await _expenseService.GetAllAsync(cancellationToken);

        return Ok(expenses);
    }

    [HttpGet("{expenseEntryId:int}")]
    public async Task<ActionResult<ExpenseResult>> GetByIdAsync(
        [FromRoute] int expenseEntryId,
        CancellationToken cancellationToken)
    {
        ExpenseResult expense = await _expenseService.GetByIdAsync(
            expenseEntryId,
            cancellationToken);

        return Ok(expense);
    }

    [HttpPost]
    public async Task<ActionResult<ExpenseResult>> CreateAsync(
        [FromBody] CreateExpenseRequest request,
        CancellationToken cancellationToken)
    {
        ValidationResult validationResult =
            await _createExpenseRequestValidator.ValidateAsync(
                request,
                cancellationToken);

        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.ToDictionary());
        }

        ExpenseResult expense = await _expenseService.CreateAsync(
            request,
            cancellationToken);

        return Created(
            $"{Request.Path}/{expense.ExpenseEntry.ExpenseEntryId}",
            expense);
    }
}
