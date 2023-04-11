﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Backend.DAL.Entities;
using Web.Backend.DTO.Addresses;
using Web.Backend.DTO.Campeigns;
using Web.Backend.DTO.Cards;
using Web.Backend.DTO.CartItem;
using Web.Backend.DTO.Category;
using Web.Backend.DTO.Coupon;
using Web.Backend.DTO.Inventories;
using Web.Backend.DTO.Orders;
using Web.Backend.DTO.PaymentDetail;
using Web.Backend.DTO.ProductColors;
using Web.Backend.DTO.ProductDetails;
using Web.Backend.DTO.ProductRating;
using Web.Backend.DTO.ProductReview;
using Web.Backend.DTO.Products;
using Web.Backend.DTO.ProductSizes;
using Web.Backend.DTO.PurchastSession;
using Web.Backend.DTO.Roles;
using Web.Backend.DTO.Users;
using Web.Backend.DTO.UserTokens;

namespace Web.Backend.BLL.ModelMappers
{
    public class ModelMapper : Profile
    {
        public ModelMapper()
        {
            CreateMap<User, UserDTO>();
            CreateMap<Role, RoleDTO>();
            CreateMap<UserAddress, AddressDTO>();           
            CreateMap<UserCard, CardRequestDTO>();
            CreateMap<ProductDetail, ProductDetailDTO>();
            CreateMap<ProductInventory, InventoryDTO>();
            CreateMap<Product, AddProductDTO>();
            CreateMap<Product, ProductDTO>();
            CreateMap<ProductSize, ProductSizeDTO>();
            CreateMap<ProductColor, ProductColorDTO>();
            CreateMap<User, RegistrationDTO>();
            CreateMap<UserToken, UserTokenDTO>();
            CreateMap<User, UserLoginDTO>();
            CreateMap<UserCard, CardResponseDTO>();
            CreateMap<DiscountCampeign, CampeignsDTO>();
            CreateMap<PurchaseSession, PurchaseSessionDTO>();
            CreateMap<CartItem, CartItemDTO>();
            CreateMap<Order, OrderDTO>();
            CreateMap<DiscountCoupon, DiscountCouponDTO>();
            CreateMap<ProductRating, ProductRatingDTO>();
            CreateMap<ProductReview, ProductReviewDTO>();
            CreateMap<ProductCategory, ProductCategoryDTO>();
            CreateMap<PaymentDetail, PaymentDetailDTO>();

            CreateMap<UserDTO, User>();
            CreateMap<RoleDTO, Role>();
            CreateMap<AddressDTO, UserAddress>();
            CreateMap<CardRequestDTO, UserCard>();
            CreateMap<ProductDetailDTO, ProductDetail>();
            CreateMap<InventoryDTO, ProductInventory>();
            CreateMap<AddProductDTO, Product>();
            CreateMap<ProductDTO, Product>();
            CreateMap<ProductSizeDTO, ProductSize>();
            CreateMap<ProductColorDTO, ProductColor>();
            CreateMap<RegistrationDTO, User>();
            CreateMap<UserTokenDTO, UserToken>();
            CreateMap<UserLoginDTO, User>();
            CreateMap<CardResponseDTO, UserCard>();
            CreateMap<CampeignsDTO, DiscountCampeign>();
            CreateMap<PurchaseSessionDTO, PurchaseSession>();
            CreateMap<CartItemDTO, CartItem>();
            CreateMap<OrderDTO, Order>();
            CreateMap<DiscountCouponDTO, DiscountCoupon>();
            CreateMap<ProductRatingDTO, ProductRating>();
            CreateMap<ProductReviewDTO, ProductReview>();
            CreateMap<ProductCategoryDTO, ProductCategory>();
            CreateMap<PaymentDetailDTO, PaymentDetail>();
        }
    }
}
