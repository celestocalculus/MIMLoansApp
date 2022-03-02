using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MIMLoans.Data;
using MIMLoans.Models;

namespace MIMLoans.Pages
{
    public class ProcessingRequestModel : PageModel
    {
        private MIMLoansContext _context;
        public LoanRequest LoanRequest { get; set; }

        public ProcessingRequestModel(MIMLoansContext context)
        {
            _context = context;
        }

        public void OnGet(int loanId)
        {
            LoanRequest = _context.LoanRequests.FirstOrDefault(lr => lr.Id == loanId);
        }
    }
}
