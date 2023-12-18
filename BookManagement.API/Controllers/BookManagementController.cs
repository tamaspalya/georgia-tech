using Microsoft.AspNetCore.Mvc;

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

    [HttpPost(Name = "AddBook")]
    public IActionResult AddBook()
    {
        throw new NotImplementedException("This method is not implemented yet.");
    }

    [HttpPut(Name = "UpdateBook")]
    public IActionResult UpdateBook()
    {
        throw new NotImplementedException("This method is not implemented yet.");
    }

    [HttpDelete(Name = "RemoveBook")]
    public IActionResult RemoveBook()
    {
        throw new NotImplementedException("This method is not implemented yet.");
    }
}
