using HTNest.Data.Interfaces;
using HTNest.Data.Repository;
using Microsoft.Extensions.DependencyInjection;
using Project_Cursus_Group3.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTNest.Data
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddRepository(this IServiceCollection service)
        {
            service.AddTransient<ICategoryRepository, CategoryRepository>();
            service.AddTransient<IStyleRepository, StyleRepository>();
            service.AddTransient<IUserRepository, UserRepository>();
            service.AddTransient<IProductRepository, ProductRepository>();
            service.AddTransient<IAuthenRepository, AuthenRepository>();
            service.AddTransient<IUnitOfWork, UnitOfWork>();
            service.AddTransient<ICartRepository, CartRepository>();



            return service;
        }
    }
}
