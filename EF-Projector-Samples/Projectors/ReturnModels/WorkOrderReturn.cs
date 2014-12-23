using System;
using System.Collections.Generic;

namespace EFSplitProjectorTests.Projectors.ReturnModels
{
    public class WorkOrderReturn
    {
        public int WorkOrderId { get; set; }
        public int OrderQuantity { get; set; }
        public int StockedQuantity { get; set; }
        public string ScrapReason { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime DueDate { get; set; }
        public ProductionProductReturn Product { get; set; }
        public IEnumerable<WorkOrderRoutingReturn> Routings { get; set; }
    }

    public class ProductionProductReturn
    {
        public string Name { get; set; }
        public string ProductNumber { get; set; }
        public string ProductLine { get; set; }
        public string Class { get; set; }
        public string Style { get; set; }
        public string SubCategory { get; set; }
        public IEnumerable<BillOfMaterialReturn> BillOfMaterials { get; set; }
        public IEnumerable<ProductInventoryReturn> Inventory { get; set; }
    }

    public class WorkOrderRoutingReturn
    {
        public string Location { get; set; }
        public IEnumerable<ProductInventoryReturn> Inventory { get; set; }
    }

    public class ProductInventoryReturn
    {
        public string ProductName { get; set; }
        public string Shelf { get; set; }
        public int Quantity { get; set; }
    }

    public class BillOfMaterialReturn
    {
        public string ProductName { get; set; }
        public double Quantity { get; set; }
    }
}