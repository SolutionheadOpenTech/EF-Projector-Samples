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
    public class EmployeeSalesTests : QueryTestsBase<AdventureWorksContext, Employee, EmployeeSalesReturn>
    {
        protected override IQueryable<Employee> GetSourceQuery()
        {
            return Context.Employees;
        }

        protected override IEnumerable<Expression<Func<Employee, EmployeeSalesReturn>>> GetProjectors()
        {
            return EmployeeProjectors.SelectSales(Context);
        }
    }
}