using Microsoft.EntityFrameworkCore;
using SportsStoreApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStoreApi.Context
{
    public class PaymentDbContext : DbContext
    {
        public PaymentDbContext(DbContextOptions<PaymentDbContext> options)
            : base(options)
        {

        }
        public DbSet<PaymentData> PaymentDatas { get; set; }      
    }
}
