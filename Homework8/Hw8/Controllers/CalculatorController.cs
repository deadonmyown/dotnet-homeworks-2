using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using Hw8.Calculator;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;

namespace Hw8.Controllers;

public class CalculatorController : Controller
{
    [HttpGet("calculator/calculate")]
    public ActionResult<double> Calculate([FromServices] ICalculator calculator,
        [FromQuery] string val1,
        [FromQuery] string operation,
        [FromQuery] string val2)
    {
        if (!IsQueryArgsCountSupported())
            return BadRequest(Messages.InvalidAmountOfData);
        /*var values = Request.Query.Keys.Select(k => HttpContext.Request.Query[k].ToString().ToLower()).ToArray();
        (val1, operation, val2) = (values[0], values[1], values[2]);*/
        var parsedOperation = ParseOperation(operation);
        if (parsedOperation == Operation.Invalid)
            return BadRequest(Messages.InvalidOperationMessage);
        if (!Double.TryParse(val1, out double parsedVal1) || !Double.TryParse(val2, out double parsedVal2))
            return BadRequest(Messages.InvalidNumberMessage);
        if (parsedOperation == Operation.Divide && parsedVal2 == 0)
            return BadRequest(Messages.DivisionByZeroMessage);
        return parsedOperation switch
        {
            Operation.Plus => calculator.Plus(parsedVal1, parsedVal2),
            Operation.Minus => calculator.Minus(parsedVal1, parsedVal2),
            Operation.Multiply => calculator.Multiply(parsedVal1, parsedVal2),
            Operation.Divide => calculator.Divide(parsedVal1, parsedVal2)
        };
    }
    
    [ExcludeFromCodeCoverage]
    public IActionResult Index()
    {
        return Content(
            "Заполните val1, operation(plus, minus, multiply, divide) и val2 здесь '/calculator/calculate?val1= &operation= &val2= '\n" +
            "и добавьте её в адресную строку.");
    }

    private Operation ParseOperation(string arg)
    {
        return arg switch
        {
            "plus" => Operation.Plus,
            "minus" => Operation.Minus,
            "multiply" => Operation.Multiply,
            "divide" => Operation.Divide,
            _ => Operation.Invalid
        };
    }

    private bool IsQueryArgsCountSupported() => Request.Query.Count == 3;

}