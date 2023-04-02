using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Backend.DAL.Entities;
using Web.Backend.DTO;
using Web.Backend.DTO.Addresses;
using Web.Backend.DTO.Cards;
using Web.Backend.DTO.Inventories;
using Web.Backend.DTO.ProductColors;
using Web.Backend.DTO.ProductDetails;
using Web.Backend.DTO.Products;
using Web.Backend.DTO.Roles;
using Web.Backend.DTO.Users;

namespace Web.Backend.BLL.ObjectMappers
{
    public class ObjectMapper : Profile
    {
        public ObjectMapper()
        {
            CreateMap<User, UserDTO>();
            CreateMap<Role, RoleDTO>();
            CreateMap<UserAddress, AddressDTO>();           
            CreateMap<UserCard, CardDTO>();
            CreateMap<ProductDetail, ProductDetailDTO>();
            CreateMap<ProductInventory, InventoryDTO>();
            CreateMap<Product, AddProductDTO>();
            CreateMap<Product, ProductDTO>();
            CreateMap<ProductSize, ProductSizeDTO>();
            CreateMap<ProductColor, ProductColorDTO>();

            CreateMap<UserDTO, User>();
            CreateMap<RoleDTO, Role>();
            CreateMap<AddressDTO, UserAddress>();
            CreateMap<CardDTO, UserCard>();
            CreateMap<ProductDetailDTO, ProductDetail>();
            CreateMap<InventoryDTO, ProductInventory>();
            CreateMap<AddProductDTO, Product>();
            CreateMap<ProductDTO, Product>();
            CreateMap<ProductSizeDTO, ProductSize>();
            CreateMap<ProductColorDTO, ProductColor>();

        }
    }
}
