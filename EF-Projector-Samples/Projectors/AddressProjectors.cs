using System;
using System.Linq.Expressions;
using EFSplitProjectorTests.Models;
using EFSplitProjectorTests.Projectors.ReturnModels;

namespace EFSplitProjectorTests.Projectors
{
    public static class AddressProjectors
    {
        public static Expression<Func<Address, AddressReturn>> SelectAddress()
        {
            return a => new AddressReturn
                {
                    Line1 = a.AddressLine1,
                    Line2 = a.AddressLine2,
                    City = a.City,
                    State = a.StateProvince.Name,
                    PostalCode = a.PostalCode
                };
        }
    }
}