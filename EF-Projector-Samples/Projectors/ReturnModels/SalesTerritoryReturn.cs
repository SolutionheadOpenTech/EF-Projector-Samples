using System.Collections.Generic;

namespace EFSplitProjectorTests.Projectors.ReturnModels
{
    public class SalesTerritoryReturn
    {
        public string Name { get; set; }
        public string CountryRegionCode { get; set; }
        public string Group { get; set; }
        
        public IEnumerable<string> Provinces { get; set; }
        public IEnumerable<CustomerOrdersReturn> SalesOrders { get; set; }
        public IEnumerable<ContactReturn> SalesPeople { get; set; }
        public IEnumerable<ContactReturn> SalesPeopleHistory { get; set; }
    }
}