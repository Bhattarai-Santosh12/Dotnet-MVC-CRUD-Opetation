using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TicketSystem.Models;
using System.Threading.Tasks;
using TicketSystem.Models.ViewModels;

namespace TicketSystem.Controllers
{
    public class CustomerController1 : Controller
    {
        private readonly AppDbContext dbContext;

        public CustomerController1(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult AddCustomer()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddCustomer(AddCustomerViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return Content("Model state is not valid");

            var customer = new Customer
            {
                Title = viewModel.Title,
                Description = viewModel.Description,
                // Image = viewModel.Image, // Uncomment and handle image if needed
                Status = viewModel.Status,
                CreatedBy = viewModel.CreatedBy,
                last_updated_on = viewModel.last_updated_on
            };

            await dbContext.Customers.AddAsync(customer);
            await dbContext.SaveChangesAsync();

            TempData["Message"] = "Data Inserted Successfully";
            return RedirectToAction("List");


        }


        [HttpGet]
        public async Task<IActionResult> List()
        {
            var customers = await dbContext.Customers.ToListAsync();
            var cViewModel = new List<CustomerViewModel>();
            int i = 1;
            foreach (var customer in customers)
            {
                var a = new CustomerViewModel
                {
                    Id = i,
                    id=customer.Id,
                    Title = customer.Title,
                    Description= customer.Description,
                    CraetedBy = customer.CreatedBy,
                    Status = customer.Status,
                   

                };
                i++;
                cViewModel.Add(a);

            }
            return View(cViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var customer = await dbContext.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            TempData["Message"] = TempData["Messages"];
            return View(customer);

          //  return View(AddCustomerViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Customer viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var customer = await dbContext.Customers.FindAsync(viewModel.Id);
            if (customer == null)
            {
                return NotFound();
            }

            customer.Title = viewModel.Title;
            customer.Description = viewModel.Description;
            customer.Status = viewModel.Status;
            customer.Created_on = viewModel.Created_on;
            customer.last_updated_on = DateTime.UtcNow;
            customer.CreatedBy = viewModel.CreatedBy ?? customer.CreatedBy;

            await dbContext.SaveChangesAsync();
            TempData["Message"] = "Data Edited Successfully";
            return RedirectToAction("List");
        }

        [HttpPost]
        [Route("Customer/DeleteById")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Customer viewModel)
        {
            var customer = await dbContext.Customers.AsNoTracking().FirstOrDefaultAsync(x => x.Id == viewModel.Id);
            if (customer != null)
            {
                dbContext.Customers.Remove(customer);
                await dbContext.SaveChangesAsync();
                return RedirectToAction("List");
            }
            return Content("No Customer Found");
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = dbContext.Customers.FirstOrDefault(m => m.Id == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var customer = await dbContext.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }

            dbContext.Customers.Remove(customer);
            await dbContext.SaveChangesAsync();
            return RedirectToAction("List");
        }
    }
}
