using pos_system.Contexts;
using pos_system.Order;
using Microsoft.EntityFrameworkCore;
using pos_system.Customers;
using System.Runtime.InteropServices;
using System.Collections.Generic;

namespace pos_system.Bill
{
    public class BillService : IBillService
    {
        private readonly PosContext _context;
        public BillService(PosContext context)
        {
            _context = context;
        }

        public async Task<BillModel> getBill(string orderId)
        {
            decimal vatPercentage = 0.21m;

            OrderModel? order = new OrderModel();
            order = await _context.Orders.FindAsync(orderId);
            if (order == null)
            {
                return new BillModel();
            }

            CustomerModel? customer = await _context.Customers.FindAsync(order.CustomerId);
            if (customer == null)
            {
                return new BillModel();
            }

            List<OrderProductModel> orderProducts = await _context.OrderProducts.Where(o => o.OrderId == orderId).ToListAsync();
            List<BillProductModel> billProducts = orderProducts.Select(orderProduct => new BillProductModel
            {
                Name = orderProduct.Name,
                Price = orderProduct.Price,
                Vat = orderProduct.Price * vatPercentage,
                Quantity = orderProduct.Quantity
            }).ToList();

            List<OrderServiceModel> orderServices = await _context.OrderServices.Where(o => o.OrderId == orderId).ToListAsync();
            List<BillServiceModel> billServices = orderServices.Select(orderService => new BillServiceModel
            {
                Name = orderService.Name,
                Price = orderService.Price,
                Vat = orderService.Price * vatPercentage,
                Quantity = orderService.Quantity
            }).ToList();

            BillModel bill = new BillModel()
            {
                CustomerId = order.CustomerId,
                Customer = customer.Name,
                GeneratedDateTime = DateTime.Now,
                Products = billProducts,
                Services = billServices,
                Total = getTotal(billProducts, billServices),
            };

            return bill;
        }

        private decimal? getTotal (List<BillProductModel> billProducts, List<BillServiceModel> billServices)
        {
            decimal? total = 0;
            foreach (var product in billProducts)
            {
                total += product.Price * product.Quantity;
            }
            foreach (var service in billServices)
            {
                total += service.Price * service.Quantity;
            }

            return total;
        }
    }
}
