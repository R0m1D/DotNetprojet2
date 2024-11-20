﻿using P2FixAnAppDotNetCode.Models.Repositories;
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
            // TODO change the return type from array to List<T> and propagate the change
            // throughout the application
            return _productRepository.GetAllProducts();
        }

        /// <summary>
        /// Get a product form the inventory by its id
        /// </summary>
        public Product GetProductById(int id)
        {
            // Instanciation du ProductRepository
            ProductRepository productRepository = new ProductRepository();

            // Récupération de tous les produits
            var products = productRepository.GetAllProducts();

            // Recherche du produit par ID
            Product product = products.FirstOrDefault(p => p.Id == id);

            return product; // Retourne null si aucun produit n'est trouvé
        }


        /// <summary>
        /// Update the quantities left for each product in the inventory depending of ordered the quantities
        /// </summary>
        public void UpdateProductQuantities(Cart cart)
        {
            // TODO implement the method
            // update product inventory by using _productRepository.UpdateProductStocks() method.
            foreach (var CartItem in cart.Lines)
            {
                int quantityToremove = CartItem.Quantity;
                int productId = CartItem.Product.Id;
                _productRepository.UpdateProductStocks(productId, quantityToremove);
            }
        }
    }
}
