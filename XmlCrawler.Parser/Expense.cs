using System.Xml.Serialization;

namespace XmlCrawler.Service.Controllers
{
    [XmlRoot("expense")]
    public class Expense
    {
        [XmlElement("cost_centre")]
        public string CostCentre { get; set; } = "UNKNOWN";
        [XmlElement("total")]
        public decimal? Total { get; set; }
        [XmlElement("payment_method")]
        public string PaymentMethod { get; set; }
    }
}