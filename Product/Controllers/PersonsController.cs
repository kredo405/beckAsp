using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Product.Models;

namespace Product.Controllers;

[ApiController]
[Route("api/persons")]
public class PersonsController: ControllerBase
{
    private List<Person> _persons;

    public PersonsController()
    {
        _persons = new List<Person>
        {
            new Person { Id = 1, Name = "Alice", Age = 30 },
            new Person { Id = 2, Name = "Bob", Age = 25 },
            new Person { Id = 3, Name = "Charlie", Age = 40 }
        };
    }
    [HttpGet]
    [Authorize]
    public ActionResult<IEnumerable<Person>> GetPersons()
    {
        return Ok(_persons);
    }
}