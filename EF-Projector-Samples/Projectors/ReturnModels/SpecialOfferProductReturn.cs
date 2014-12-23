namespace EFSplitProjectorTests.Projectors.ReturnModels
{
    public class SpecialOfferProductReturn : ProductReturn
    {
        public string Description { get; set; }
        public string Type { get; set; }
        public string Category { get; set; }
        public decimal Discount { get; set; }
    }
}