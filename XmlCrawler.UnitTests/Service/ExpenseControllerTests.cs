using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using XmlCrawler.Parser;
using XmlCrawler.Service.Controllers;

namespace XmlCrawler.UnitTests.Service
{
    [TestClass]
    public class ExpenseControllerTests
    {
        [TestMethod]
        public void WhenExpenseIsValid_ShouldReturnOk()
        {
            // ARRANGE
            var parser = new Mock<IXmlExpenseParser>();
            parser.Setup(p => p.Parse(It.IsAny<string>())).Returns(new Expense());

            var controller = new ExpenseController(parser.Object);
            var text = "test123";

            // ACT
            var result = controller.Post(text);

            // ASSERT
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }

        [TestMethod]
        public void WhenExpenseIsNull_ShouldReturnBadRequest()
        {
            // ARRANGE
            var parser = new Mock<IXmlExpenseParser>();
            var controller = new ExpenseController(parser.Object);
            var text = "test123";

            // ACT
            var result = controller.Post(text);

            // ASSERT
            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
        }
    }
}
