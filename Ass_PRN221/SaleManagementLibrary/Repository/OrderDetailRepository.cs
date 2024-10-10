using Microsoft.EntityFrameworkCore;
using SaleManagementLibrary.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaleManagementLibrary.Repository
{
    public class OrderDetailRepository : IOrderDetailRepository
    {
        private readonly DBContext _context;

        public OrderDetailRepository(DBContext context)
        {
            _context = context;
        }

        public void Add(OrderDetail orderDetail)
        {
            try
            {
                _context.OrderDetails.Add(orderDetail);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Error adding order detail: " + ex.Message, ex);
            }
        }

        public void Delete(int id)
        {
            try
            {
                IEnumerable<OrderDetail> orderDetailList = GetById(id);
                if (orderDetailList != null)
                {
                    foreach (OrderDetail o in orderDetailList)
                    {
                        _context.OrderDetails.Remove(o); 
                    }
                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting order detail: " + ex.Message, ex);
            }
        }

        public IEnumerable<OrderDetail> GetAll()
        {
            return _context.OrderDetails.ToList();
        }

        public List<OrderDetail> GetById(int id)
        {
            try
            {
                return _context.OrderDetails.Where(od => od.OrderId == id).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving order detail: " + ex.Message, ex);
            }
        }

        public void Update(OrderDetail orderDetail)
        {
            try
            {
                var existingOrderDetail = _context.OrderDetails.FirstOrDefault(x => x.OrderId == orderDetail.OrderId && x.ProductId == orderDetail.ProductId);
                if (existingOrderDetail != null)
                {
                    _context.Entry(existingOrderDetail).State = EntityState.Detached;
                }

                _context.Update(orderDetail);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating order detail: " + ex.Message, ex);
            }
        }
    }
}
