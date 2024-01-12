using Microsoft.AspNetCore.Mvc;
using pos_system.Contexts;
using Microsoft.EntityFrameworkCore;
using pos_system.ProductService.Discounts;
using pos_system.Customers;
using pos_system.Order;
using pos_system.ProductService.Services;

namespace pos_system.Order.Payments
{
    public class PaymentService : IPaymentService
    {
        private readonly PosContext _context;
        public PaymentService(PosContext context)
        {
            _context = context;
        }

        public async Task<PaymentModel[]> GetPayments()
        {
            PaymentModel[] payments = await _context.Payments.ToArrayAsync();
            return payments;
        }

        public async Task<PaymentModel?> CreatePayment(PaymentPostRequest paymentInput)
        {
            OrderModel? order = await _context.Orders.FindAsync(paymentInput.OrderId);
            if (order == null)
            {
                return null;
            }
            PaymentModel payment = new PaymentModel
            {
                Id = Guid.NewGuid().ToString(),
                OrderId = paymentInput.OrderId,
                Status = paymentInput.Status,
                Type = paymentInput.Type,
            };

            _context.Add(payment);
            await _context.SaveChangesAsync();
            return payment;
        }

        public async Task<List<PaymentModel>?> GetAllUserPayments(string userId)
        {
            List<OrderModel>? orders = _context.Orders.Where(o => o.CustomerId == userId).ToList();
            List<PaymentModel> payments = new List<PaymentModel>();
            foreach (OrderModel order in orders)
            {
                PaymentModel? payment = await _context.Payments.SingleOrDefaultAsync(p => p.OrderId == order.Id);
                if (payment != null)
                {
                    payments.Add(payment);
                }
            }
            return payments;
        }

        public async Task<PaymentModel?> GetOrderPayment(string orderId)
        {
            PaymentModel? order = await _context.Payments.SingleOrDefaultAsync(p => p.OrderId == orderId);
            if (order == null)
            {
                return null;
            }
            return order;
        }

        public async Task<PaymentModel?> GetPayment(string paymentId)
        {
            PaymentModel? payment = await _context.Payments.FindAsync(paymentId);
            if (payment == null)
            {
                return null;
            }
            return payment;
        }

        public async Task<PaymentModel> UpdatePayment(string paymentId, PaymentPostRequest payment)
        {
            PaymentModel? updatedPayment = new PaymentModel();
            updatedPayment = await _context.Payments.SingleOrDefaultAsync(p => p.Id == paymentId);
            if (updatedPayment == null)
            {
                return null;
            }
            if (payment.OrderId != null)
            {
                updatedPayment.OrderId = payment.OrderId;
            }
            if (payment.Status != null)
            {
                updatedPayment.Status = payment.Status;
            }
            if (payment.Type != null)
            {
                updatedPayment.Type = payment.Type;
            }

            await _context.SaveChangesAsync();
            return updatedPayment;
        }

        public async Task<bool> DeletePayment(string paymentId)
        {
            var payment = await _context.Payments.FindAsync(paymentId);
            if (payment == null)
            {
                return false;
            }

            _context.Payments.Remove(payment);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
