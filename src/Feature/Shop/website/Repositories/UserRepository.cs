using BasicCompany.Feature.BasicContent.Models.Headstart;
using BasicCompany.Feature.BasicContent.Models.Json;
using OrderCloud.SDK;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Threading.Tasks;
using BasicCompany.Feature.BasicContent.Helpers;

namespace BasicCompany.Feature.BasicContent.Repositories
{
    public class UserRepository
    {
        private readonly OrderCloudClient _oc;

        public UserRepository()
        {
            _oc = OrderCloudClientFactory.CreateClient();
        }

        public async Task<HSUser> CreateUserAsync(CreateUserParamsJson createUserParams)
        {
            try
            {
                var user = await _oc.Users.CreateAsync<HSUser>(ConfigurationManager.AppSettings["BuyerId"] ?? "0001", new User()
                {
                    Username = createUserParams.Username,
                    Password = createUserParams.Password,
                    FirstName = createUserParams.FirstName,
                    LastName = createUserParams.LastName,
                    Email = createUserParams.Email,
                    Phone = createUserParams.Phone,
                    TermsAccepted = DateTime.Now,
                    Active = true,
                    AvailableRoles = new List<string>()
                    {
                        "HSBaseBuyer",
                        "MeAddressAdmin",
                        "MeAdmin",
                        "MeCreditCardAdmin",
                        "MeXpAdmin",
                        "ProductFacetReader",
                        "Shopper",
                        "SupplierAddressReader",
                        "SupplierReader"
                    },
                    Locale = null,
                    DateCreated = DateTime.Now,
                    PasswordLastSetDate = DateTime.Now
                });

                return user;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}