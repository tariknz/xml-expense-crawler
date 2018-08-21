using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using XmlCrawler.Parser;

namespace XmlCrawler.UnitTests.Parser
{
    [TestClass]
    public class XmlExpenseParserTests
    {
        [TestMethod]
        [DataRow(@"
            Hi Yvaine,
            Please create an expense claim for the below. Relevant details are marked up as requested...
            <expense><cost_centre>DEV002</cost_centre> <total>890.55</total>
            <payment_method>personal card</payment_method>
            </expense>
        ")]
        [DataRow(@"
            From: Ivan Castle
            Sent: Friday, 16 February 2018 10:32 AM
            To: Antoine Lloyd <Antoine.Lloyd@example.com>
            Subject: test

            Hi Yvaine,
            Please create an expense claim for the below. Relevant details are marked up as requested...
            <expense><cost_centre>DEV002</cost_centre> <total>890.55</total>
            <payment_method>personal card</payment_method>
            </expense>
        ")]
        public void WhenExpenseObjectExistsInText_ShouldParseSuccessfully(string data)
        {
            // ARRANGE
            var parser = new XmlExpenseParser();
            
            // ACT
            var expense = parser.Parse(data);

            // ASSERT
            Assert.IsNotNull(expense, "Expense object was not found");
            Assert.AreEqual(expense.Total, (decimal) 890.55, "Total was not parsed correctly");
            Assert.AreEqual(expense.CostCentre, "DEV002", "Cost Centre was not parsed correctly");
            Assert.AreEqual(expense.PaymentMethod, "personal card", "Payment method was not parsed correctly");
        }

        [TestMethod]
        [DataRow(@"
            Total object is not found
            <expense><cost_centre>DEV002</cost_centre>
            <payment_method>personal card</payment_method>
            </expense>
        ")]
        public void WhenTotalIsNotAvailable_ShouldNotParseExpenseObject(string data)
        {
            // ARRANGE
            var parser = new XmlExpenseParser();

            // ACT
            var expense = parser.Parse(data);

            // ASSERT
            Assert.IsNull(expense, "No expense object should be returned");
        }

        [TestMethod]
        [DataRow(@"
            Hi Yvaine,
            Please create an expense claim for the below. Relevant details are marked up as requested...
            <expense><total>890.55</total>
            <payment_method>personal card</payment_method>
            </expense>
        ")]
        public void WhenCostCentreIsNotFound_ShouldDefaultToUnknown(string data)
        {
            // ARRANGE
            var parser = new XmlExpenseParser();

            // ACT
            var expense = parser.Parse(data);

            // ASSERT
            Assert.AreEqual(expense?.CostCentre, "UNKNOWN", "Cost Centre should be unknown");
        }

        [TestMethod]
        [DataRow(@"
            From: Ivan Castle
            Sent: Friday, 16 February 2018 10:32 AM
            To: Antoine Lloyd <Antoine.Lloyd@example.com>
            Subject: test
            Hi Antoine,
            Please create a reservation at the <vendor>Viaduct Steakhouse</vendor> our <description>development
            team’s project end celebration dinner</description> on <date>Tuesday 27 April 2017</date>. We expect to
            arrive around 7.15pm. Approximately 12 people but I’ll confirm exact numbers closer to the day.
            Regards,
            Ivan
        ")]
        public void WhenExpenseIsNotFound_ShouldReturnNull(string data)
        {
            // ARRANGE
            var parser = new XmlExpenseParser();

            // ACT
            var expense = parser.Parse(data);

            // ASSERT
            Assert.IsNull(expense, "Expense object should be null");
        }
    }
}
