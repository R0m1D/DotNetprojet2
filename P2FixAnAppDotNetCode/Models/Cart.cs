﻿using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace P2FixAnAppDotNetCode.Models
{
    /// <summary>
    /// The Cart class
    /// </summary>
    public class Cart : ICart
    {
        private readonly List<CartLine> _cartLines = new List<CartLine>();
        /// <summary>
        /// Read-only property for display only
        /// </summary>
        public IEnumerable<CartLine> Lines => _cartLines;

        /// <summary>
        /// Return the actual cartline list
        /// </summary>
        /// <returns></returns>
        private List<CartLine> GetCartLineList()
        {
            return new List<CartLine>();
        }
        public int GetQuantity(Product product)
        {
            var line = _cartLines.FirstOrDefault(l => l.Product.Id == product.Id);
            return line?.Quantity ?? 0; // Retourne 0 si le produit n'est pas dans le panier
        }
        /// <summary>
        /// Adds a product in the cart or increment its quantity in the cart if already added
        /// </summary>//
        public void AddItem(Product product, int quantity)
        {
            bool productExists = _cartLines.Exists(line => line.Product.Id == product.Id);

            if (productExists)
            {
                var existingLine = _cartLines.First(line => line.Product.Id == product.Id);
                existingLine.Quantity += quantity;
            }
            else
            {
                _cartLines.Add(new CartLine { Product = product, Quantity = quantity });
            }
        }

        /// <summary>
        /// Removes a product form the cart
        /// </summary>
        public void RemoveLine(Product product)
        {
            var cartLine = _cartLines.FirstOrDefault(l => l.Product.Id == product.Id);
            if (cartLine != null)
            {
                if (cartLine.Quantity > 1)
                {
                    cartLine.Quantity--;
                }
                else
                {
                    _cartLines.RemoveAll(l => l.Product.Id == product.Id);
                }
            }
        }

        /// <summary>
        /// Get total value of a cart
        /// </summary>
        public double GetTotalValue()
        {
            double totalValue = 0;
            foreach (var line in _cartLines)
            {
                totalValue+=line.Product.Price*line.Quantity;
            }
            return totalValue;
        }

        /// <summary>
        /// Get average value of a cart
        /// </summary>
        public double GetAverageValue()
        {
            int totalItem = 0;
            foreach (var line in _cartLines)
            {
                totalItem += line.Quantity;
            }
            double averageValue = GetTotalValue()/totalItem;
            if (double.IsNaN(averageValue))
            {
                return 0;
            }
            else
            {
                return averageValue;
            }
        }

        /// <summary>
        /// Looks after a given product in the cart and returns if it finds it
        /// </summary>
        public Product FindProductInCartLines(int productId)
        {
            foreach (var line in _cartLines)
            {
                if (productId==line.Product.Id)
                {
                    return line.Product;
                }
            }
            return null;
        }

        /// <summary>
        /// Get a specific cartline by its index
        /// </summary>
        public CartLine GetCartLineByIndex(int index)
        {
            return Lines.ToArray()[index];
        }

        /// <summary>
        /// Clears a the cart of all added products
        /// </summary>
        public void Clear()
        {
            List<CartLine> cartLines = _cartLines;
            cartLines.Clear();
        }
    }

    public class CartLine
    {
        public int OrderLineId { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}
