using System.Threading.Tasks;
using BasicCompany.Feature.BasicContent.Repositories;
using System.Web.Mvc;
using BasicCompany.Feature.BasicContent.Models.Headstart;
using BasicCompany.Feature.BasicContent.Models.Json;

namespace BasicCompany.Feature.BasicContent.Controllers
{
    [Route("user")]
    public class UserController : Controller
    {
        [HttpPost, Route("create")]
        public async Task<HSUser> CreateUser(CreateUserParamsJson createUserParams)
        {
            var userRepository = new UserRepository();
            return await userRepository.CreateUserAsync(createUserParams);
        }
    }
}