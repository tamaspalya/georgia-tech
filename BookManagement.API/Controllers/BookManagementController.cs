using Microsoft.AspNetCore.Mvc;
using System;

namespace BookManagement.API.Controllers;

[ApiController]
[Route("[controller]")]
public class BookManagementController : ControllerBase
{
    private readonly ILogger<BookManagementController> _logger;

    public BookManagementController(ILogger<BookManagementController> logger)
    {
        _logger = logger;
    }

    // This method can be used to add a new book
    [HttpPost(Name = "AddBook")]
    public IActionResult AddBook()
    {
        throw new NotImplementedException("This method is not implemented yet.");
    }

    // This method can be used to update an existing book
    [HttpPut(Name = "UpdateBook")]
    public IActionResult UpdateBook()
    {
        throw new NotImplementedException("This method is not implemented yet.");
    }

    // This method can be used to remove an existing book
    [HttpDelete(Name = "RemoveBook")]
    public IActionResult RemoveBook()
    {
        throw new NotImplementedException("This method is not implemented yet.");
    }
}
