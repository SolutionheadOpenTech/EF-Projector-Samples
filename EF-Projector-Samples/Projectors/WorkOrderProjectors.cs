using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using EF_Split_Projector;
using EFSplitProjectorTests.Models;
using EFSplitProjectorTests.Projectors.ReturnModels;
using LinqKit;

namespace EFSplitProjectorTests.Projectors
{
    public static class WorkOrderProjectors
    {
        public static IEnumerable<Expression<Func<WorkOrder, WorkOrderReturn>>> SelectWorkOrder()
        {
            var product = ProductProjectors.SelectProductionProduct();
            var routings = WorkOrderRoutingProjectors.SelectWorkOrderRouting();

            return new Projectors<WorkOrder, WorkOrderReturn>
                {
                    w => new WorkOrderReturn
                        {
                            WorkOrderId = w.WorkOrderID,
                            OrderQuantity = w.OrderQty,
                            StockedQuantity = w.StockedQty,
                            StartDate = w.StartDate,
                            EndDate = w.EndDate,
                            DueDate = w.DueDate,
                            ScrapReason = new[] { w.ScrapReason}.Select(r => r.Name).FirstOrDefault()
                        },
                    { product, p => w => new WorkOrderReturn
                        {
                            Product = p.Invoke(w.Product)
                        } },
                    { routings, p => w => new WorkOrderReturn
                        {
                            Routings = w.WorkOrderRoutings.Select(r => p.Invoke(r))
                        }
                    }
                };
        }
    }

    public static class WorkOrderRoutingProjectors
    {
        public static IEnumerable<Expression<Func<WorkOrderRouting, WorkOrderRoutingReturn>>> SelectWorkOrderRouting()
        {
            var inventory = ProductInventoryProjectors.SelectProductInventory();

            return new Projectors<WorkOrderRouting, WorkOrderRoutingReturn>
                {
                    r => new WorkOrderRoutingReturn
                        {
                            Location = r.Location.Name,
                        },
                    { inventory, p => r => new WorkOrderRoutingReturn
                        {
                            Inventory = r.Location.ProductInventories.Select(i => p.Invoke(i))
                        }
                    }
                };
        }
    }

    public static class ProductInventoryProjectors
    {
        public static IEnumerable<Expression<Func<ProductInventory, ProductInventoryReturn>>> SelectProductInventory()
        {
            return new Projectors<ProductInventory, ProductInventoryReturn>
                {
                    i => new ProductInventoryReturn
                        {
                            ProductName = i.Product.Name,
                            Shelf = i.Shelf,
                            Quantity = (int)i.Quantity
                        }
                };
        }
    }

    public static class BillOfMaterialsProjectors
    {
        public static IEnumerable<Expression<Func<BillOfMaterial, BillOfMaterialReturn>>> SelectBillOfMaterial()
        {
            return new Projectors<BillOfMaterial, BillOfMaterialReturn>
                {
                    b => new BillOfMaterialReturn
                        {
                            ProductName = b.Product.Name,
                            Quantity = (double)b.PerAssemblyQty
                        }
                };
        }
    }
}