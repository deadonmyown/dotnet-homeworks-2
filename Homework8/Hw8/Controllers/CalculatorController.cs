using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using Hw8.Calculator;
using Hw8.Parser;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;

namespace Hw8.Controllers;

public class CalculatorController : Controller
{
    public ActionResult<double> Calculate([FromServices] ICalculator calculator,
        [FromQuery] string val1,
        [FromQuery] string operation,
        [FromQuery] string val2)
    {
        if (!Parser.Parser.IsArgsCountSupported(Request.Query.Count))
            return BadRequest(Messages.InvalidAmountOfData);
        (ParserResult result, ParserArgs? args) result = Parser.Parser.ParseCalcArguments(val1, operation, val2);
        return result switch
        {
            (ParserResult.InvalidNumber, _) => BadRequest(Messages.InvalidNumberMessage),
            (ParserResult.InvalidOperation, _) => BadRequest(Messages.InvalidOperationMessage),
            (ParserResult.DivisionByZero, _) => BadRequest(Messages.DivisionByZeroMessage),
            _ => Ok(calculator.Calculate(result.args!.Value1, result.args.Operation, result.args.Value2))
        };
    }

    [ExcludeFromCodeCoverage]
    public IActionResult Index()
    {
        return Content(
            "Заполните val1, operation(plus, minus, multiply, divide) и val2 здесь '/calculator/calculate?val1= &operation= &val2= '\n" +
            "и добавьте её в адресную строку.");
    }
}