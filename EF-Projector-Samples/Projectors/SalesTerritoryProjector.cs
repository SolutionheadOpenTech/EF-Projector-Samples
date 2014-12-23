using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using EFSplitProjectorTests.Models;
using EFSplitProjectorTests.Projectors.ReturnModels;
using EF_Split_Projector;
using EF_Split_Projector.Helpers.Extensions;
using LinqKit;

namespace EFSplitProjectorTests.Projectors
{
    public static class SalesTerritoryProjector
    {
        public static Expression<Func<SalesTerritory, SalesTerritoryReturn>> SelectSalesTerritory(AdventureWorksContext context)
        {
            return SplitSelectSalesTerritory(context).Aggregate((Expression<Func<SalesTerritory, SalesTerritoryReturn>>) null,
                (c, s) => c == null ? s : c.Merge(s));
        }

        public static IEnumerable<Expression<Func<SalesTerritory, SalesTerritoryReturn>>> SplitSelectSalesTerritory(AdventureWorksContext context)
        {
            var contact = ContactProjectors.SelectContact();
            var splitSelectCustomerOrders = CustomerProjectors.SplitSelectCustomerOrders(context);

            return new Projectors<SalesTerritory, SalesTerritoryReturn>
                {
                    //t => new SalesTerritoryReturn
                    //    {
                    //        Name = t.Name,
                    //        CountryRegionCode = t.CountryRegionCode,
                    //        Group = t.Group,
                    //        Provinces = t.StateProvinces.Select(p => p.Name),
                    //    },
                    //t => new SalesTerritoryReturn
                    //    {
                    //        SalesPeople = t.SalesPersons.Select(p => contact.Invoke(p.Employee.Contact))
                    //    },
                    //t => new SalesTerritoryReturn
                    //    {
                    //        SalesPeopleHistory = t.SalesTerritoryHistories.Select(h => contact.Invoke(h.SalesPerson.Employee.Contact))
                    //    },
                    { splitSelectCustomerOrders, p => t => new SalesTerritoryReturn
                        {
                            //SalesOrders = t.SalesPersons.SelectMany(r => r.Stores.Select(e => p.Invoke(e.Customer))),
                            SalesOrders = t.Customers.Where(c => c.CustomerType == "S").Select(u => p.Invoke(u)),
                        }
                    }
                };
        }
    }
}