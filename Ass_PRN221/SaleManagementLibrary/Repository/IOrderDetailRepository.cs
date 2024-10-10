using SaleManagementLibrary.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaleManagementLibrary.Repository
{
    public interface IOrderDetailRepository
    {
        // Create
        void Add(OrderDetail orderDetail);

        // Read
        List<OrderDetail> GetById(int id);
        IEnumerable<OrderDetail> GetAll();

        // Update
        void Update(OrderDetail orderDetail);

        // Delete
        void Delete(int id);
    }

}
