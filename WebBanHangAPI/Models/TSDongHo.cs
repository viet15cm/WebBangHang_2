using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebBanHangAPI.Models
{
    public class TSDongHo
    {
        
        [Key]
        [ForeignKey("SanPham")]
        [StringLength(10)]
        public string IDDH { get; set; }
        public int DuongKinh { get; set; }
        public string ChatLieuMat { get; set; }
        public string ChatLieuDay { get; set; }
        public int DoRongDay { get; set; }
        public string ChongNuoc { get; set; }

        public string ThoiGianPin { get; set; }

        public int GoiTinh { get; set; }

        public virtual SanPham SanPham { get; set; }




    }
}