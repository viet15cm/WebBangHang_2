using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebBanHangAPI.Models
{
    public class TSDienThoai
    {
        [Key]
        [ForeignKey("SanPham")]
        [StringLength(10)]
        public string IDDT { get; set; }
        public string MangHinh { get; set; }
        public string HeDieuHanh { get; set; }

        public string CameraRaSau { get; set; }
        public string CaMeraTruoc { get; set; }

        public string CPU { get; set; }
        public int Gram { get; set; }
        public string TheNho { get; set; }
        public string DungLuongPin { get; set; }

        public virtual SanPham SanPham { get; set; }



    }
}