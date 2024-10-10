using SaleManagementLibrary.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaleManagementLibrary.Repository
{
    public interface IOrderRepository
    {
        // Create
        void Add(Order order);

        // Read
        Order GetById(int id);
        IEnumerable<Order> GetAll();

        // Update
        void Update(Order order);

        // Delete
        void Delete(int id);
    }

}
