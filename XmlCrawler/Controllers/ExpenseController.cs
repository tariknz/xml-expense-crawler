using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using XmlCrawler.Parser;

namespace XmlCrawler.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpenseController : ControllerBase
    {
        private readonly IXmlExpenseParser _xmlExpenseParser;

        public ExpenseController(IXmlExpenseParser xmlExpenseParser)
        {
            _xmlExpenseParser = xmlExpenseParser;
        }

        [HttpPost]
        public IActionResult Post([FromBody] string text)
        {
            var expense = _xmlExpenseParser.Parse(text);

            if (expense == null) return BadRequest("No valid expense object found in text");

            return Ok(expense);
        }
    }
}
