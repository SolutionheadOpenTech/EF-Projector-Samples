using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using EFSplitProjectorTests.Models;
using EFSplitProjectorTests.Projectors.ReturnModels;
using EF_Split_Projector;
using LinqKit;

namespace EFSplitProjectorTests.Projectors
{
    public static class EmployeeProjectors
    {
        public static IEnumerable<Expression<Func<Employee, EmployeeSalesReturn>>> SelectSales(AdventureWorksContext context)
        {
            var contact = ContactProjectors.SelectContact();
            var sales = SalesOrderProjectors.SplitSelectOrder(context);
            var territories = SalesTerritoryProjector.SplitSelectSalesTerritory(context);

            return new Projectors<Employee, EmployeeSalesReturn>
                {
                    e => new EmployeeSalesReturn
                        {
                            Contact = contact.Invoke(e.Contact)
                        },
                    { sales, p => e => new EmployeeSalesReturn
                        {
                            Orders = e.SalesPerson.SalesOrderHeaders.Select(o => p.Invoke(o))
                        }
                    },
                    { territories, p => e => new EmployeeSalesReturn
                        {
                            TerritorySales = new [] { e.SalesPerson.SalesTerritory }.Select(t => p.Invoke(t)).FirstOrDefault()
                        }
                    }
                };
        }
    }
}