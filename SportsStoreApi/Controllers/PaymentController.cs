using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SportsStoreApi.Context;
using SportsStoreApi.Entities;

namespace SportsStoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly PaymentDbContext db;       
        public PaymentController(PaymentDbContext db)
        {
            this.db = db;
        }
        // GET: api/Payment
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "Pay1", "Pay2" };
        }

        // GET: api/Payment/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
           return db.PaymentDatas.FirstOrDefault(p => p.Id == id).AmountOfMoney.ToString();
            
        }

        // POST: api/Payment
        [HttpPost]
        
        public int Post([FromBody]decimal amount)
        {
            PaymentData paymentData = new PaymentData();
            paymentData.AmountOfMoney = amount;

            db.Add(paymentData);
            db.SaveChanges();
            //return "ok" vers mvc 5 avec l'id de la future transaction
            return paymentData.Id;
        }

        // PUT: api/Payment/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
