using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using WebBanHangAPI.GlobalVariablesWeb;
using WebBanHangAPI.Models.CookieAccout;
using WebBanHangAPI.Models.mvcModels;

namespace WebBanHangAPI.Controllers
{
    public class ShareOutSideController : Controller
    {
        // GET: ShareOutSide
        public ActionResult LoginCustomer()
        {
            ViewBag.Title = "Login customer";
            return View();
        }

        public ActionResult TrangChu()
        {
           
            return View();
        }

        [HttpPost]
        public JsonResult SetMemory(TokenAccount tk)
        {

            if (tk != null)
            {
                if (tk.Flag.Equals("cookie"))
                {

                    Session.Abandon();
                    HttpCookie cookie = new HttpCookie("AccoutCustomer");
                    cookie.HttpOnly = true;
                    HttpContext.Response.Cookies.Remove("AccoutCustomer");
                    cookie.Value = tk.AccoutToken;
                    cookie.Expires = DateTime.Now.AddDays(2);
                    HttpContext.Response.SetCookie(cookie);

                    TokenAccount tokenAccount = new TokenAccount()
                    {
                        AccoutToken = Request.Cookies.Get("AccoutCustomer").Value,
                        Flag = "cookie"
                    };
                    return Json(tokenAccount);
                }
                else if (tk.Flag.Equals("session"))
                {
                    Response.Cookies["Accout"].Expires = DateTime.Now.AddDays(-1);

                    Session["Accouts"] = tk.AccoutToken;
                    TokenAccount tokenAccount = new TokenAccount()
                    {
                        AccoutToken = Session["AccoutCustomer"].ToString(),
                        Flag = "sesstion"
                    };
                    return Json(tokenAccount);
                }

            }

            return Json(tk);

        }

        [HttpGet]
        public string GetTooken()
        {
            if (Session["AccoutCustomer"] != null)
                return Session["AccoutCustomer"].ToString();
            if (Request.Cookies["AccoutCustomer"] != null)
            {
                var value = Request.Cookies["Accout"].Value.ToString();
                return value;
            }
            return null;
        }

        [HttpGet]
        public string ResetTooken()
        {
            Session.Abandon();
            Response.Cookies["AccoutCustomer"].Expires = DateTime.Now.AddDays(-1);

            return "Thang Cong";

        }

        public ActionResult SanPham(string id)
        {
            if (id != null)
            {
                IEnumerable<mvcSanPhamHangSanXuat> sanPhamList;
                IEnumerable<mvcMathang> matHangList;

                HttpResponseMessage reponse = GlobalVariables.WebApiClient.GetAsync("SanPhams/GetFindMatHang/" + id).Result;
                HttpResponseMessage reponse2 = GlobalVariables.WebApiClient.GetAsync("MatHangs").Result;
                sanPhamList = reponse.Content.ReadAsAsync<IEnumerable<mvcSanPhamHangSanXuat>>().Result;
                matHangList = reponse2.Content.ReadAsAsync<IEnumerable<mvcMathang>>().Result;

                var hsxList = new List<mvcHangSanXuat>();
                foreach (var item in sanPhamList)
                {
                    hsxList.Add(new mvcHangSanXuat() { IDHSX = item.IDHSX, TenHSX = item.TenHSX });
                }
                var join = from pn in hsxList
                           group pn by new { pn.IDHSX, pn.TenHSX } into obGroup
                           orderby obGroup.Key.IDHSX, obGroup.Key.TenHSX
                           select new
                           {
                               IDHSX = obGroup.Key.IDHSX,
                               TenHSX = obGroup.Key.TenHSX,

                           };

                var hsxListJion = new List<mvcHangSanXuat>();
                foreach (var item in join.ToList())
                {
                    hsxListJion.Add(new mvcHangSanXuat() { IDHSX = item.IDHSX, TenHSX = item.TenHSX });
                }
                var objMultipleModels = Tuple.Create<IEnumerable<mvcSanPhamHangSanXuat>, IEnumerable<mvcMathang>, IEnumerable<mvcHangSanXuat>>
                    (sanPhamList, matHangList, hsxListJion);
                return View(objMultipleModels);
            }
            else
            {
                IEnumerable<mvcSanPhamHangSanXuat> sanPhamList;
                IEnumerable<mvcMathang> matHangList;
                HttpResponseMessage reponse = GlobalVariables.WebApiClient.GetAsync("SanPhams/GetJonSanPhamHangSanXuat").Result;
                HttpResponseMessage reponse2 = GlobalVariables.WebApiClient.GetAsync("MatHangs").Result;
                sanPhamList = reponse.Content.ReadAsAsync<IEnumerable<mvcSanPhamHangSanXuat>>().Result;
                matHangList = reponse2.Content.ReadAsAsync<IEnumerable<mvcMathang>>().Result;

                var hsxList = new List<mvcHangSanXuat>();
                foreach (var item in sanPhamList)
                {
                    hsxList.Add(new mvcHangSanXuat() { IDHSX = item.IDHSX, TenHSX = item.TenHSX });
                }
                var join = from pn in hsxList
                           group pn by new { pn.IDHSX, pn.TenHSX } into obGroup
                           orderby obGroup.Key.IDHSX, obGroup.Key.TenHSX
                           select new
                           {
                               IDHSX = obGroup.Key.IDHSX,
                               TenHSX = obGroup.Key.TenHSX,

                           };

                var hsxListJion = new List<mvcHangSanXuat>();
                foreach (var item in join.ToList())
                {
                    hsxListJion.Add(new mvcHangSanXuat() { IDHSX = item.IDHSX, TenHSX = item.TenHSX });
                }

                if(Session["numberCart"] == null)
                {
                    Session["numberCart"] = 0;
                }

                var objMultipleModels = Tuple.Create<IEnumerable<mvcSanPhamHangSanXuat>, IEnumerable<mvcMathang>, IEnumerable<mvcHangSanXuat>>
                    (sanPhamList, matHangList, hsxListJion);
                return View(objMultipleModels);
            }
        }

        public ActionResult ShowProduct(string id, string idmh)
        {

            mvcSanPhamMatHangHSX sp = null;
            mvcTSDienThoai item1 = null;
            mvcTSDongHo item2 = null;
            HttpResponseMessage reponseSP = GlobalVariables.WebApiClient.GetAsync("SanPhams/GetFindSanPhamMatHangHangSX/" + id).Result;

            HttpResponseMessage reponseDT = GlobalVariables.WebApiClient.GetAsync("TSDienThoais/GetTSSanPham/" + id).Result;
            HttpResponseMessage reponseDH = GlobalVariables.WebApiClient.GetAsync("TSDongHos/GetTSSanPham/" + id).Result;
            sp = reponseSP.Content.ReadAsAsync<mvcSanPhamMatHangHSX>().Result;
            item1 = reponseDT.Content.ReadAsAsync<mvcTSDienThoai>().Result;
            item2 = reponseDH.Content.ReadAsAsync<mvcTSDongHo>().Result;
            var objMultipleModels = Tuple.Create<mvcSanPhamMatHangHSX, mvcTSDienThoai, mvcTSDongHo>(
                                    sp, item1, item2
                                    );

            return View(objMultipleModels);

        }

        public JsonResult PushCart(mvcSanPhamCart spc)
        {
            List<mvcSanPhamCart> myList = new List<mvcSanPhamCart>();
            List<mvcSanPhamCart> myList2 = new List<mvcSanPhamCart>();
            List<mvcSanPhamCart> myList3 = new List<mvcSanPhamCart>();
            if (Session["Cart"] == null )
            {
                myList.Add(spc);
                Session["Cart"] = myList;
            }
            else
            {
                myList = (List<mvcSanPhamCart>)Session["Cart"];
                myList.Add(spc);
                Session["Cart"] = myList;

                

                myList2 = (from sp in myList
                           group sp by new { sp.IDSP, sp.Anh, sp.TenSP, sp.DonGia } into obGroup
                           orderby obGroup.Key.IDSP, obGroup.Key.TenSP, obGroup.Key.DonGia, obGroup.Key.Anh
                           select new mvcSanPhamCart()
                           {

                               IDSP = obGroup.Key.IDSP,
                               TenSP = obGroup.Key.TenSP,
                               DonGia = obGroup.Key.DonGia,
                               Anh = obGroup.Key.Anh,
                               SoLuong = obGroup.Sum(x => x.SoLuong),


                           }).ToList();

                foreach(var item in myList2)
                {
                    if (item.SoLuong > 20)
                        myList3.Add(new mvcSanPhamCart()
                        {
                            IDSP = item.IDSP,
                            TenSP = item.TenSP,
                            DonGia = item.DonGia,
                            Anh = item.Anh,
                            SoLuong = 20
                        });
                    else
                        myList3.Add(item);
                }

                var sl = 0;
                foreach (var item in myList3)
                {
                    
                    sl += item.SoLuong;
                    
                }
                Session["numberCart"] = sl;
                Session["Cart"] = myList3;

            }

            return Json(spc, JsonRequestBehavior.AllowGet);
        }
        
        public ActionResult Cart()
        {
            List<mvcSanPhamCart> myList = new List<mvcSanPhamCart>();
            
            if (Session["Cart"] != null)
            {
                myList = (List<mvcSanPhamCart>)Session["Cart"];

            }

            else
            {
                return View(myList);
            }

            return View(myList);
        }

        [HttpGet]
        public JsonResult EditCart(string id, int value)
        {
          
            List<mvcSanPhamCart> myList = new List<mvcSanPhamCart>();
            List<mvcSanPhamCart> myList2 = new List<mvcSanPhamCart>();
            myList = (List<mvcSanPhamCart>)Session["Cart"];
            if (value == 0)
            {
                foreach (var item in myList)
                {
                    if (item.IDSP == id)
                        continue;
                    myList2.Add(item);
                }
            }
            else 
            {
             
                foreach (var item in myList)
                {
                    if (item.IDSP == id)
                    {
                        if (value > 20)
                            myList2.Add(item);
                        else
                            myList2.Add(new mvcSanPhamCart()
                            {
                                IDSP = item.IDSP,
                                TenSP = item.TenSP,
                                DonGia = item.DonGia,
                                Anh = item.Anh,
                                SoLuong = value,
                            });
                    }
                    else                       
                    myList2.Add(item);
                }
            }
            var s = 0;
            foreach(var item in myList2)
            {
                s += item.SoLuong;
            }
            Session["numberCart"] = s;
            Session["Cart"] = myList2;

            return Json(myList2, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult DeleteProduct(string id)
        {
            List<mvcSanPhamCart> myList = new List<mvcSanPhamCart>();
            List<mvcSanPhamCart> myList2 = new List<mvcSanPhamCart>();

            if(Session["Cart"] != null)
            {
                myList = (List<mvcSanPhamCart>)Session["Cart"];

                foreach(var item in myList)
                {
                    if (item.IDSP == id)
                        continue;
                    myList2.Add(new mvcSanPhamCart()
                    {
                        IDSP = item.IDSP,
                        TenSP = item.TenSP,
                        DonGia = item.DonGia,
                        Anh = item.Anh,
                        SoLuong = item.SoLuong,
                    });
                }

                var s = 0;
                foreach (var item in myList2)
                {
                    s += item.SoLuong;
                }
                Session["numberCart"] = s;

                Session["Cart"] = myList2;
            }

            return Json(myList2, JsonRequestBehavior.AllowGet);
        }

       [HttpGet]
       public int NumberCart()
        {
            
            int c = (int)Session["numberCart"];
            return c;
        }


    }
}