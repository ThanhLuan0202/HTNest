using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using HTNest.Data.Entities;
using HTNest.Data.Model.Category;
using HTNest.Data.Model.Login;
using HTNest.Data.Model.Product;
using HTNest.Data.Model.Style;
using HTNest.Data.Model.User;
using HTNest.Data.ViewModels;

namespace HTNest.Data.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<CreateCategoryModel, Category>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.categoryName))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ReverseMap();

            CreateMap<Category, CategoryViewModel>().ReverseMap();
            CreateMap<Category, UpdateCategoryModel>().ReverseMap();


            CreateMap<Style, CreateStyleModel>().ReverseMap();
            CreateMap<Style, StyleViewModel>().ReverseMap();
            CreateMap<Style, UpdateStyleModel>().ReverseMap();


            CreateMap<User, CreateUserModel>().ReverseMap();
            CreateMap<User, UserViewModel>().ReverseMap();
            CreateMap<User, UpdateUserModel>().ReverseMap();


            CreateMap<Product, CreateProductModel>().ReverseMap();
            CreateMap<Product, ProductViewModel>().ReverseMap();
            CreateMap<Product, UpdateProductModel>().ReverseMap();

            CreateMap<User, RegisterLoginModel>().ReverseMap();
            CreateMap<User, RegisterViewModel>().ReverseMap();



        }
    }
}
