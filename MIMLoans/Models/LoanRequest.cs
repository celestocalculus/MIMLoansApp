using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MIMLoans.Models
{
    public class LoanRequest
    {
        public static readonly int InterestRate = 15;
        public static readonly int LoanTenureInMonths = 18; 

        public int Id { get; set; }
        public DateTime TenureStartsUtc { get; set; }
        public DateTime TenureEndsUtc { get; set; }
        public double LoanAmount { get; set; }
        public double AmountRepayable { get; set; }
        public LoanStatus Status { get; set; } = LoanStatus.InProgress;
        public int CustomerId { get; set; }
        public DateTime DateAddedUtc { get; set; }

        public Customer Customer { get; set; }
    }

    public enum LoanStatus
    {
        InProgress,
        Approved,
        Declined
    }
}
