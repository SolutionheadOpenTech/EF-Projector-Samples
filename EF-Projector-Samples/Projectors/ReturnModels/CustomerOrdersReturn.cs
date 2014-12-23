using System.Collections.Generic;

namespace EFSplitProjectorTests.Projectors.ReturnModels
{
    public class CustomerOrdersReturn
    {
        public string AccountNumber { get; set; }
        public string Type { get; set; }

        public ContactReturn Contact { get; set; }
        public IEnumerable<SalesOrderReturn> Orders { get; set; }
        public IEnumerable<AddressReturn> Addresses { get; set; }
    }
}