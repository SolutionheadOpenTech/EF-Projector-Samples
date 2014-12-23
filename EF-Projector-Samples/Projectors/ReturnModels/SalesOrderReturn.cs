using System.Collections.Generic;

namespace EFSplitProjectorTests.Projectors.ReturnModels
{
    public class SalesOrderReturn
    {
        public string SalesOrderNumber { get; set; }
        public string PurchaseOrderNumber { get; set; }
        public string AccountNumber { get; set; }
        public decimal TotalValue { get; set; }
        public string ShipMethod { get; set; }
        
        //public ContactReturn Contact { get; set; }
        public ContactReturn SalesPerson { get; set; }
        public IEnumerable<string> Reasons { get; set; }
        public IEnumerable<SalesOrderDetailReturn> Details { get; set; }

    }
}
