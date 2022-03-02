using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using MIMLoans.Data;
using MIMLoans.Models;
using System;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace MIMLoans.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private MIMLoansContext _context;

        [BindProperty]
        public Customer Customer { get; set; }
        [BindProperty]
        public double LoanAmount { get; set; }

        public IndexModel(ILogger<IndexModel> logger, MIMLoansContext context)
        {
            _logger = logger;
            _context = context;
        }

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                TempData["Flash"] = string.Join("; ", ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage));
                return Page();
            }

            if(DateTime.Now.Year - Customer.DateOfBirthUtc.Year < 18)
            {
                TempData["Flash"] = "You must be at least 18 years";
                return Page();
            }

            Customer.DateAddedUtc = DateTime.UtcNow;
            _context.Customers.Add(Customer);
            await _context.SaveChangesAsync();

            var loanRequest = new LoanRequest
            {
                Customer = Customer,
                DateAddedUtc = DateTime.UtcNow,
                LoanAmount = LoanAmount,
                Status = LoanStatus.InProgress,
                AmountRepayable = (((LoanAmount / LoanRequest.LoanTenureInMonths) * (LoanRequest.InterestRate / 100.0)) + (LoanAmount / LoanRequest.LoanTenureInMonths)) * LoanRequest.LoanTenureInMonths
            };

            _context.LoanRequests.Add(loanRequest);
            await _context.SaveChangesAsync();

            return RedirectToPage("ProcessingRequest", new { loanId = loanRequest.Id });
        }
    }
}
