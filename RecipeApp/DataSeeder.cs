using RecipeApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeApp
{
    public class DataSeeder
    {
        public DataSeeder()
        {

        }

        public static void Seed(IAccountService accountService)
        {
            accountService.seedDataIfEmpty();
        }
    }
}
