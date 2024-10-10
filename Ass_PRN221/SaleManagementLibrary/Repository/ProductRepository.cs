using Microsoft.EntityFrameworkCore;
using SaleManagementLibrary.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaleManagementLibrary.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly DBContext _context;

        public ProductRepository(DBContext context)
        {
            _context = context;
        }

        public void Add(Product product)
        {
            try
            {
                _context.Products.Add(product);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Error adding product: " + ex.Message, ex);
            }
        }

        public void Delete(int id)
        {
            try
            {
                var product = GetById(id);
                if (product != null)
                {
                    _context.Products.Remove(product);
                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting product: " + ex.Message, ex);
            }
        }

        public IEnumerable<Product> GetAll()
        {
            return _context.Products.ToList();
        }

        public Product GetById(int id)
        {
            try
            {
                return _context.Products.FirstOrDefault(p => p.ProductId == id);
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving product: " + ex.Message, ex);
            }
        }

        public void Update(Product product)
        {
            try
            {
                var existingProduct = _context.Products.Find(product.ProductId);
                if (existingProduct != null)
                {
                    _context.Entry(existingProduct).State = EntityState.Detached;
                }

                _context.Update(product);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating product: " + ex.Message, ex);
            }
        }

    }
}
