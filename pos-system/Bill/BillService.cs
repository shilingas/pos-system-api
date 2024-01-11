using pos_system.Contexts;
using pos_system.Order;
using Microsoft.EntityFrameworkCore;
using pos_system.Customers;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using pos_system.Discounts;

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
            List<BillProductModel> billProducts = new List<BillProductModel>();

            foreach (var product in orderProducts)
            {
                decimal? discountedPrice = await CalculateDiscountedProductPrice(product);

                billProducts.Add(new BillProductModel
                {
                    Name = product.Name,
                    Price = discountedPrice,
                    Vat = discountedPrice * vatPercentage,
                    Quantity = product.Quantity
                });
            }

            List<OrderServiceModel> orderServices = await _context.OrderServices.Where(o => o.OrderId == orderId).ToListAsync();
            List<BillServiceModel> billServices = new List<BillServiceModel>();

            foreach (var service in orderServices)
            {
                decimal? discountedPrice = await CalculateDiscountedServicePrice(service);

                billServices.Add(new BillServiceModel
                {
                    Name = service.Name,
                    Price = discountedPrice,
                    Vat = discountedPrice * vatPercentage,
                    Quantity = service.Quantity
                });
            }

            BillModel bill = new BillModel()
            {
                CustomerId = order.CustomerId,
                Customer = customer.Name,
                GeneratedDateTime = DateTime.Now,
                Products = billProducts,
                Services = billServices,
                Total = getTotal(billProducts, billServices, vatPercentage),
                Tip = order.Tip,
            };

            return bill;
        }

        private async Task<decimal?> CalculateDiscountedProductPrice(OrderProductModel product)
        {
            List<DiscountProductModel> discountProductEntries = await _context.DiscountProducts
                .Where(d => d.ProductId == product.ProductId)
                .ToListAsync();

            decimal discountMultiplier = 1m;

            foreach (var discountProduct in discountProductEntries)
            {
                var discount = await _context.Discounts
                    .FirstOrDefaultAsync(d => d.Id == discountProduct.DiscountId && (d.ValidUntilDateTime == null || d.ValidUntilDateTime >= DateTime.Now));

                if (discount != null)
                {
                    discountMultiplier *= 1 - (decimal)(discount.Percentage ?? 0);
                }
            }

            return product.Price * discountMultiplier;
        }

        private async Task<decimal?> CalculateDiscountedServicePrice(OrderServiceModel service)
        {
            List<DiscountServiceModel> discountServiceEntries = await _context.DiscountServices
                .Where(d => d.ServiceId == service.ServiceId)
                .ToListAsync();

            decimal discountMultiplier = 1m;

            foreach (var discountProduct in discountServiceEntries)
            {
                var discount = await _context.Discounts
                    .FirstOrDefaultAsync(d => d.Id == discountProduct.DiscountId && (d.ValidUntilDateTime == null || d.ValidUntilDateTime >= DateTime.Now));

                if (discount != null)
                {
                    discountMultiplier *= 1 - (decimal)(discount.Percentage ?? 0);
                }
            }

            return service.Price * discountMultiplier;
        }

        private decimal? getTotal(List<BillProductModel> billProducts, List<BillServiceModel> billServices, decimal vatPercentage)
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

            return total * (1 + vatPercentage);
        }
    }
}
