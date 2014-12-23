namespace EFSplitProjectorTests.Projectors.ReturnModels
{
    public class SalesOrderDetailReturn
    {
        public string CarrierTrackingNumber { get; set; }
        public short Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal UnitDiscount { get; set; }
        public decimal LineTotal { get; set; }

        public SpecialOfferProductReturn SpecialOfferProduct { get; set; }
        public ProductionProductReturn Product { get; set; }

    }
}