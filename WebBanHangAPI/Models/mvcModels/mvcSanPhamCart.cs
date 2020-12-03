using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebBanHangAPI.Models.mvcModels
{
    public class mvcSanPhamCart
    {
        public string IDSP { get; set; }

        public string TenSP { get; set; }

        
        public decimal DonGia { get; set; }

        public string Anh { get; set; }
        public int SoLuong { get; set; }
    }
}