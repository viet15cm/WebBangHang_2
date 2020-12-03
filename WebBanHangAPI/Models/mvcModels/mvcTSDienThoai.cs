﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebBanHangAPI.Models.mvcModels
{
    public class mvcTSDienThoai
    {
        public string IDDT { get; set; }
        public string MangHinh { get; set; }
        public string HeDieuHanh { get; set; }

        public string CameraRaSau { get; set; }
        public string CaMeraTruoc { get; set; }

        public string CPU { get; set; }
        public int Gram { get; set; }
        public string TheNho { get; set; }
        public string DungLuongPin { get; set; }
    }
}