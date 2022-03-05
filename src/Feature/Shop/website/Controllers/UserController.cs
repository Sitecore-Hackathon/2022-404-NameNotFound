using System.Threading.Tasks;
using Tshirts.Feature.Shop.Repositories;
using System.Web.Mvc;
using Tshirts.Feature.Shop.Models.Headstart;
using Tshirts.Feature.Shop.Models.Json;

namespace Tshirts.Feature.Shop.Controllers
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