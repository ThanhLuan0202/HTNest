using HTNest.Data.Repository;
using HTNest.Service.Interfaces;
using HTNest.Service.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTNest.Service
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddServices(this IServiceCollection service)
        {
            service.AddTransient<ICategoryService, CategoryService>();
            service.AddTransient<IStyleService, StyleService>();
            service.AddTransient<IUserService, UserService>();
            service.AddTransient<IProductService, ProductService>();
            service.AddTransient<IAuthenService, AuthenService>();
            service.AddTransient<ICartService, CartService>();


            return service;
        }
    }
}