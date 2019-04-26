using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
//using System.Web.Script.Serialization;
//using System.Web.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SportsStorePayment.Models;

namespace SportsStorePayment.Controllers
{
    public class PaymentController : Controller
    {
        // GET: Payment
        public ActionResult Index()
        {
            return View();
        }

        // GET: Payment/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Payment/Create
        public async Task<ActionResult> Create()
        {
            string Url = Request.Path;
            string Id = Url.Split("/").Last();
            PaymentDetails payment = new PaymentDetails();
            payment.Id = Id;
            payment.AmountOfMoney = await GetAmountOfMoney(Id);
            return View("Create", payment);
        }

        private async Task<decimal> GetAmountOfMoney(string id)
        {

            string urlApi = "https://localhost:44347/api/Payment" + "/" + id;
            using (var client = new HttpClient())
            using (var request = new HttpRequestMessage())
            {
                request.Method = HttpMethod.Get;
                request.RequestUri = new Uri(urlApi);


                var result = await client.SendAsync(request);
                var amountOfMoney = await result.Content.ReadAsStringAsync();
                return decimal.Parse(amountOfMoney);
            }
            
        }

        // POST: Payment/Create
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(PaymentDetails paymentDetails)
        {
            try
            {
                string urlApi = $"https://localhost:44347/api/Payment/{paymentDetails.Id}";
                using (var client = new HttpClient())
                using (var request = new HttpRequestMessage())
                {
                    request.Method = HttpMethod.Put;
                    request.RequestUri = new Uri(urlApi);
                    var json = JsonConvert.SerializeObject(paymentDetails);
                    request.Content = new StringContent(json, Encoding.UTF8, "application/json");
                    await client.SendAsync(request);

                    return Ok();
                }
            }
            catch(Exception ex)
            {
                return View("Error", ex);
            }
        }

        // GET: Payment/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Payment/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Payment/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Payment/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }


    }
}