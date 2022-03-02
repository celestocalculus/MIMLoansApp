using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MIMLoans.Data;
using MIMLoans.Models;

namespace MIMLoans.Pages
{
    public class AdminModel : PageModel
    {
        private MIMLoansContext _context;
        public List<LoanRequest> LoanRequests { get; set; } = new List<LoanRequest>();

        public AdminModel(MIMLoansContext context)
        {
            _context = context;
        }

        public void OnGet()
        {
            LoanRequests = _context.LoanRequests.Include(lr => lr.Customer).ToList();
        }

        public async Task<IActionResult> OnGetProcessLoan(LoanStatus status, int loanId)
        {
            var loanRequest = _context.LoanRequests.FirstOrDefault(lr => lr.Id == loanId);
            if(loanRequest == null)
            {
                TempData["Flash"] = "No loan request found with ID supplied";
                return Page();
            }

            loanRequest.Status = status;
            loanRequest.TenureStartsUtc = DateTime.UtcNow;
            loanRequest.TenureEndsUtc = DateTime.UtcNow.AddMonths(LoanRequest.LoanTenureInMonths);
            _context.LoanRequests.Update(loanRequest);
            await _context.SaveChangesAsync();

            return RedirectToPage();
        }
    }
}
