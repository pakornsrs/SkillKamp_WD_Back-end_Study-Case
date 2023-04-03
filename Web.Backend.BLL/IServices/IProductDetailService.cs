﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Backend.DTO;
using Web.Backend.DTO.ProductDetails;

namespace Web.Backend.BLL.IServices
{
    public interface IProductDetailService
    {
        public ServiceResponseModel<ProductDetailDTO> CreateProductDetail(ProductDetailDTO req);
    }
}
