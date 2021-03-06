﻿using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using SportsStore.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace SportsStore.WebUI.Controllers
{
    public class CartController : Controller
    {
        private IProductsRepository repository;
        private IOrderProcessor orderProcessor;

        public CartController(IProductsRepository repo, IOrderProcessor proc)
        {
            repository = repo;
            orderProcessor = proc;
        }

        public ViewResult Index(Cart cart, string returnUrl)
        {
            return View(new CartIndexViewModel { ReturnUrl = returnUrl, Cart = cart });
        }

        // GET: Cart
        public RedirectToRouteResult AddToCart(Cart cart, int productId, string returnUrl)
        {
            Product product = repository.Products
                .FirstOrDefault(p => p.ProductID == productId);

            if (product != null)
            {
                cart.AddItem(product, 1);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        public RedirectToRouteResult RemoveFromCart(Cart cart, int productId, string returnUrl)
        {
            Product product = repository.Products
                .FirstOrDefault(p => p.ProductID == productId);

            if (product != null)
            {
                cart.RemoveLine(product);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        private Cart GetCart()
        {
            Cart cart = (Cart)Session["Cart"];
            if (cart == null)
            {
                cart = new Cart();
                Session["Cart"] = cart;
            }
            return cart;
        }

        public PartialViewResult Summary(Cart cart)
        {
            return PartialView(cart);
        }

        public ViewResult Checkout()
        {
            return View(new ShippingDetails());
        }

        [HttpPost]
        public async Task<ActionResult> Checkout(Cart cart, ShippingDetails shippingDetails)
        {
            if (cart.Lines.Count() == 0)
            {
                ModelState.AddModelError("", "Sorry, your cart is empty!");
            }

            if (ModelState.IsValid)
            {
                orderProcessor.ProcessOrder(cart, shippingDetails);
                //cart.Clear();
                var amount = cart.ComputeTotalValue();
                //envoyer vers api pour creation de la futur transaction
                //creation d'un objet httpclient
                string urlApi = "https://localhost:44347/api/Payment";
                using (var client = new HttpClient())
                using (var request = new HttpRequestMessage())
                {
                    request.Method = HttpMethod.Post; 
                    request.RequestUri = new Uri(urlApi);
                    var json = new JavaScriptSerializer().Serialize(amount);
                    request.Content = new StringContent(json, Encoding.UTF8, "application/json");

                    var result = await client.SendAsync(request);
                    var transactionId = await result.Content.ReadAsStringAsync();
                    //return await client.SendAsync(request);

                    if (result.IsSuccessStatusCode)
                        return Redirect("https://localhost:44317/Payment/Create" + "/" + transactionId);
                    else
                        return View("CheckoutError");

                };
                //postasjsonasync ===> sans devoir serializer
            }
            else
            {               
                return Redirect("http://localhost:55147/Cart/Checkout");
            }
        }

        public void GetPaymentId(int paymentId)
        {

        }
    }
}