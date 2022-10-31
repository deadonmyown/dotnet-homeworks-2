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
    private IParser _parser;
    public CalculatorController(IParser parser) => _parser = parser; 
    
    public ActionResult<double> Calculate([FromServices] ICalculator calculator,
        [FromQuery] string val1,
        [FromQuery] string operation,
        [FromQuery] string val2)
    {
        try
        {
            if (!_parser.IsArgsCountSupported(Request.Query.Count))
                return BadRequest(Messages.InvalidAmountOfData);

            _parser.ParseCalcArguments(out double parsedVal1, out Operation parsedOperation,
                out double parsedVal2, val1, operation, val2);
            return Ok(calculator.Calculate(parsedVal1, parsedOperation, parsedVal2));
        }
        catch (Exception ex) when (ex is ArgumentException || ex is InvalidOperationException || ex is DivideByZeroException)
        {
            return BadRequest(ex.Message);
        }
    }

    [ExcludeFromCodeCoverage]
    public IActionResult Index()
    {
        return Content(
            "Заполните val1, operation(plus, minus, multiply, divide) и val2 здесь '/calculator/calculate?val1= &operation= &val2= '\n" +
            "и добавьте её в адресную строку.");
    }
}