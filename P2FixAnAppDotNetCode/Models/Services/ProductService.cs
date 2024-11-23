using P2FixAnAppDotNetCode.Models.Repositories;
using System;
using System.Linq;

namespace P2FixAnAppDotNetCode.Models.Services
{
    /// <summary>
    /// This class provides services to manages the products
    /// </summary>
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IOrderRepository _orderRepository;

        public ProductService(IProductRepository productRepository, IOrderRepository orderRepository)
        {
            _productRepository = productRepository;
            _orderRepository = orderRepository;
        }

        /// <summary>
        /// Get all product from the inventory
        /// </summary>
        public Product[] GetAllProducts()
        {
            return _productRepository.GetAllProducts();
        }

        /// <summary>
        /// Get a product form the inventory by its id
        /// </summary>
        public Product GetProductById(int id)
        {

            ProductRepository productRepository = new ProductRepository();

            var products = productRepository.GetAllProducts();

            Product product = products.FirstOrDefault(p => p.Id == id);

            return product; 
        }

       
        /// <summary>
        /// Update the quantities left for each product in the inventory depending of ordered the quantities
        /// </summary>
        public void UpdateProductQuantities(Cart cart)
        {
            foreach (var CartItem in cart.Lines)
            {
                int quantityToremove = CartItem.Quantity;
                int productId = CartItem.Product.Id;
                _productRepository.UpdateProductStocks(productId, quantityToremove);
            }
        }
    }
}
