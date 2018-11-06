using Microsoft.AspNetCore.Mvc;
using Onion.Model;
using Onion.Service;

namespace Onion.API.Controllers
{
    [Route("api/[controller]/[action]")]
    public class PersonController : CrudController<Person>
    {
        public PersonController(IEntityService<Person> entityService)
            : base(entityService) { }
    }
}
