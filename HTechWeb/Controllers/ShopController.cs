using HTechWeb.Models.DTO.Shop.Request;
using HTechWeb.Models.Process.Home.Product_API;
using HTechWeb.Models.Process.Shop.Shop_API;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HTechWeb.Controllers
{
    public class ShopController : Controller
    {
        // GET: Shop
        public ActionResult Index()
        {
            return View();
        }

        //Shop Action Start
        public ActionResult Product_Details(string id)
        {
            string idProduct = "";
            //
            var idProTmp = TempData["idProduct"];
            //
            if (id != null) idProduct = id;
            else if (idProTmp != null) idProduct = idProTmp.ToString();
            var data = ProductConnect.GetDataProductAsync(idProduct).Result;
            if (data.result != null && data.result.productId != null)
            {
                ViewBag.getShop = ShopConnect.GetDataShopAsync(new GetShopRequest { shopId = data.result.shopId }).Result.result;
                return View(data);
            }
            return RedirectToAction("Index", "Home");
        }
        public ActionResult Shop_Page(string id, string sort)
        {
            if (id != null)
            {
                var getShop = ShopConnect.GetDataShopAsync(new GetShopRequest { shopId = id }).Result.result;
                string keyword = "";
                if (TempData["keyword"] != null) keyword = TempData["keyword"].ToString();
                if (keyword != "" || sort != "")
                    getShop = ShopConnect.GetDataShopAsync(new GetShopRequest { shopId = id, Keyword = keyword, SortBy = sort }).Result.result;
                return View(getShop);
            }
            return RedirectToAction("Index", "Home");
        }

        //Search
        public ActionResult SearchInShop(string id)
        {
            var keyword = Request["shopProductKeyWord"];
            if (keyword != "") TempData["keyword"] = keyword;
            return RedirectToAction("Shop_Page", new { id = id });
        }

        public ActionResult SearchSortBy(string id, string sort)
        {
            return RedirectToAction("Shop_Page", new { id = id, sort = sort });
        }


        //Shop Action End

    }
}