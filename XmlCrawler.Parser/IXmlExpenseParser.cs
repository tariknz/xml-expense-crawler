using XmlCrawler.Service.Controllers;

namespace XmlCrawler.Parser
{
    public interface IXmlExpenseParser
    {
        /// <summary>
        /// Extract expense object from text, throws error when not found
        /// </summary>
        /// <param name="text">Input text</param>
        /// <returns>Returns expense object, or null when not found</returns>
        Expense Parse(string text);
    }
}