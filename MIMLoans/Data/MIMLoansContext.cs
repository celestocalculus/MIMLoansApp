using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MIMLoans.Models;

namespace MIMLoans.Data
{
    public class MIMLoansContext : DbContext
    {
        public MIMLoansContext (DbContextOptions<MIMLoansContext> options)
            : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<LoanRequest> LoanRequests { get; set; }
    }
}
