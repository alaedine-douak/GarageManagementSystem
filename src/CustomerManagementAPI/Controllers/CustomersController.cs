using Microsoft.AspNetCore.Mvc;
using CustomerManagementAPI.Model;
using Microsoft.EntityFrameworkCore;
using CustomerManagementAPI.DataAccess;

namespace CustomerManagementAPI.controllers;

[Route("/api/[controller]")]
public class CustomersController : ControllerBase
{
    private readonly CustomerManagementDbContext _ctx;

    public CustomersController(CustomerManagementDbContext ctx)
    {
        _ctx = ctx;
    }

    [HttpGet(Name = "GetAllCustomers")]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _ctx.Customers.ToListAsync());
    }

    [HttpGet]
    [Route("{customerId}", Name = "GetCustomerById")]
    public async Task<IActionResult> GetCustomerById(int customerId)
    {
        var customer = await _ctx.Customers
            .FirstOrDefaultAsync(x => x.CustomerId == customerId);

        if (customer is null) return NotFound();

        return Ok(customer);
    }

    [HttpPost(Name = "RegisterCustomer")]
    public async Task<IActionResult> Register([FromBody] Customer model)
    {
        try
        {
            if (ModelState.IsValid)
            {
                var customer = new Customer
                {
                    Name = model.Name,
                    Address = model.Address,
                    City = model.City
                };

                _ctx.Customers.Add(customer);
                await _ctx.SaveChangesAsync();


                return CreatedAtRoute(
                    nameof(GetCustomerById), 
                    new { customerId = customer.CustomerId }, 
                    customer);
            }

            return BadRequest();
        }
        catch (DbUpdateException)
        {
            ModelState.AddModelError("", "Unable to save changes");
            return StatusCode(StatusCodes.Status500InternalServerError);
            throw;
        }
    }
}