using Microsoft.AspNetCore.Mvc;

namespace CustomerManagementAPI.controllers;

[Route("/api/[controller]")]
public class CustomersController : ControllerBase
{
    [HttpGet]
    public  IActionResult Get()
    {
        return Ok("customers");
    }
}