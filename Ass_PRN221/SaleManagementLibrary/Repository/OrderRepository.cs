using Microsoft.EntityFrameworkCore;
using SaleManagementLibrary.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaleManagementLibrary.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly DBContext _context;

        public OrderRepository(DBContext context)
        {
            _context = context;
        }

        public void Add(Order order)
        {
            try
            {
                _context.Orders.Add(order);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Error adding order: " + ex.Message, ex);
            }
        }

        public void Delete(int id)
        {
            try
            {
                var order = GetById(id);
                if (order != null)
                {
                    _context.Orders.Remove(order);
                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting order: " + ex.Message, ex);
            }
        }

        public IEnumerable<Order> GetAll()
        {
            return _context.Orders.ToList();
        }

        public Order GetById(int id)
        {
            try
            {
                return _context.Orders.FirstOrDefault(o => o.OrderId == id);
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving order: " + ex.Message, ex);
            }
        }

        public void Update(Order order)
        {
            try
            {
                var existingOrder = _context.Orders.Find(order.OrderId);
                if (existingOrder != null)
                {
                    _context.Entry(existingOrder).State = EntityState.Detached;
                }

                _context.Update(order);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating order: " + ex.Message, ex);
            }
        }

    }
}
