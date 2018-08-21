using System.IO;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Serialization;
using XmlCrawler.Service.Controllers;

namespace XmlCrawler.Parser
{
    public class XmlExpenseParser : IXmlExpenseParser
    {
        public Expense Parse(string text)
        {
            // looks for <expense> objects within text
            var match = Regex.Match(text, "<expense>(.*)<\\/expense>", RegexOptions.Singleline);

            // attempts to deserialise the expense object
            var expense = ReadExpenseFromXml(match.Value);

            // checks if total is set
            if (expense?.Total != null) return expense;

            // returns null when null or total is not set
            return null;
        }

        private Expense ReadExpenseFromXml(string xml)
        {
            if (string.IsNullOrEmpty(xml)) return null;

            using (var reader = new StringReader(xml))
            using (var xmlReader = XmlReader.Create(reader))
            {
                var serializer = new XmlSerializer(typeof(Expense));

                if (!serializer.CanDeserialize(xmlReader)) return null;

                var expenseObject = serializer.Deserialize(xmlReader) as Expense;

                return expenseObject;
            }
        }
    }
}
