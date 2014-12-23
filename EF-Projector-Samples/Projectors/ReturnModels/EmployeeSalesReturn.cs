using System.Collections.Generic;

namespace EFSplitProjectorTests.Projectors.ReturnModels
{
    public class EmployeeSalesReturn
    {
        public ContactReturn Contact { get; set; }
        public SalesTerritoryReturn TerritorySales { get; set; }
        public IEnumerable<SalesOrderReturn> Orders { get; set; }

    }
}