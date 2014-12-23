using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using EFSplitProjectorTests.Models;
using EFSplitProjectorTests.Projectors;
using EFSplitProjectorTests.Projectors.ReturnModels;
using NUnit.Framework;

namespace EF_Projector_Samples.Tests
{
    [TestFixture]
    public class SalesTerritoryTests : QueryTestsBase<AdventureWorksContext, SalesTerritory, SalesTerritoryReturn>
    {
        protected override IQueryable<SalesTerritory> GetSourceQuery()
        {
            return Context.SalesTerritories;
        }

        protected override IEnumerable<Expression<Func<SalesTerritory, SalesTerritoryReturn>>> GetProjectors()
        {
            return SalesTerritoryProjector.SplitSelectSalesTerritory(Context);
        }

        protected override void Post(IEnumerable<SalesTerritoryReturn> results)
        {
            //Console.WriteLine("Orders: {0}", results.SelectMany(r => r.SalesOrders).Count());
        }
    }

    [TestFixture]
    public class SalesOrders : QueryTestsBase<AdventureWorksContext, Customer, CustomerOrdersReturn>
    {
        protected override IQueryable<Customer> GetSourceQuery()
        {
            return Context.Customers.Where(c => c.CustomerType == "S");
        }

        protected override IEnumerable<Expression<Func<Customer, CustomerOrdersReturn>>> GetProjectors()
        {
            return CustomerProjectors.SplitSelectCustomerOrders(Context);
        }
    }
}
