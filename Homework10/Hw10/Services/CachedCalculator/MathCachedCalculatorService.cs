using Hw10.DbModels;
using Hw10.Dto;
using Hw10.Services.MathCalculator;
using Microsoft.EntityFrameworkCore;

namespace Hw10.Services.CachedCalculator;

public class MathCachedCalculatorService : IMathCalculatorService
{
	private readonly ApplicationContext _dbContext;
	private readonly IMathCalculatorService _simpleCalculator;

	public MathCachedCalculatorService(ApplicationContext dbContext, IMathCalculatorService simpleCalculator)
	{
		_dbContext = dbContext;
		_simpleCalculator = simpleCalculator;
	}

	public async Task<CalculationMathExpressionResultDto> CalculateMathExpressionAsync(string? expression)
	{
		var cachedSolve = _dbContext.SolvingExpressions.FirstOrDefault(s => s.Expression == expression);
		if (cachedSolve != null) return new CalculationMathExpressionResultDto(cachedSolve.Result);
		var calculate = await _simpleCalculator.CalculateMathExpressionAsync(expression);
		if (calculate.IsSuccess)
		{
			_dbContext.SolvingExpressions.Add(new SolvingExpression()
				{ Expression = expression, Result = calculate.Result });
			await _dbContext.SaveChangesAsync();
		}
		return calculate;
	}
}