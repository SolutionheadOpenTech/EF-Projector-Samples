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
    public class WorkOrderTests : QueryTestsBase<AdventureWorksContext, WorkOrder, WorkOrderReturn>
    {
        protected override IQueryable<WorkOrder> GetSourceQuery()
        {
            return Context.WorkOrders.OrderBy(o => o.WorkOrderID).Take(100);
        }

        protected override IEnumerable<Expression<Func<WorkOrder, WorkOrderReturn>>> GetProjectors()
        {
            return WorkOrderProjectors.SelectWorkOrder();
        }
    }
}