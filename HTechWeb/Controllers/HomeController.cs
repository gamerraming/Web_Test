using HTechWeb.Models.Class.GlobalVariable;
using HTechWeb.Models.DTO.Home.Cart.Request;
using HTechWeb.Models.DTO.Home.Cart.Response;
using HTechWeb.Models.DTO.Home.Order.Request;
using HTechWeb.Models.DTO.Home.Payment.Request;
using HTechWeb.Models.DTO.Home.Payment.Response;
using HTechWeb.Models.DTO.Home.Product.Request;
using HTechWeb.Models.DTO.Home.Product.Response;
using HTechWeb.Models.DTO.Home.Shipment.Request;
using HTechWeb.Models.DTO.Home.Shipment.Response;
using HTechWeb.Models.DTO.Home.Voucher.Request;
using HTechWeb.Models.DTO.User.Response;
using HTechWeb.Models.Process.Home.Cart_API;
using HTechWeb.Models.Process.Home.Order_API;
using HTechWeb.Models.Process.Home.Payment;
using HTechWeb.Models.Process.Home.Product_API;
using HTechWeb.Models.Process.Home.Shipment_API;
using HTechWeb.Models.Process.Home.Shipment_API;
using HTechWeb.Models.Process.Home.Voucher_API;
using HTechWeb.Models.Process.User.User_API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HTechWeb.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            var listProduct = ProductConnect.GetListProduct(new GetListProductRequest { PageIndex = GlobalV.PAGE_INDEX }).Result.result.data;
            Session.Add("listProduct", listProduct);
            ViewBag.listCategory = ProductConnect.GetListCategory(new GetListCategoryRequest { PageIndex = GlobalV.PAGE_INDEX }).Result.result;
            return View(listProduct);
        }

        //Product list
        public ActionResult Product_List(string id,string cName)
        {
            if (!string.IsNullOrEmpty(id))
            {
                var dataC = ProductConnect.GetDataCategoryAsync(new GetDataCategoryRequest { categoryId = id }).Result.result;
                ViewBag.dataCategory = dataC;
                ViewBag.categoryName = cName;
                return View();
            }
            string keyword = "";
            ProductResponse.Result listProduct = null;
            if (TempData["keyWord"] != null)
            {
                keyword = TempData["keyWord"].ToString();
                listProduct = ProductConnect.GetListProduct(new GetListProductRequest { PageIndex = GlobalV.PAGE_INDEX, Keyword = keyword }).Result.result;
                if (keyword != "")
                    ViewBag.keyWord = keyword;
            }
            else
            {
                listProduct = ProductConnect.GetListProduct(new GetListProductRequest { PageIndex = GlobalV.PAGE_INDEX }).Result.result;
            }
            return View(listProduct);
        }

        //Search Product
        public ActionResult Search_Product()
        {
            if (Request["txtSearch"] != null) TempData["keyWord"] = Request["txtSearch"];
            return RedirectToAction("Product_List");
        }

        //Process Start
        public ActionResult Contact()
        {
            return View();
        }

        public ActionResult Cart()
        {
            if (Session["LoginFlag"] != null)
            {
                Session["ListVoucher"] = VoucherConnect.GetListVoucher(new GetListVoucherRequest { PageIndex = GlobalV.PAGE_INDEX }).Result.result;
                Session["ListCart"] = CartConnect.GetListCart().Result.result;
                return View();
            }
            return RedirectToAction("Login", "User");
        }
        #region Cart&Buy
        //Add
        public ActionResult AddToCart(string id, int? qty, string mode)
        {
            if (id != null && id.Trim() != "")
            {
                if (GlobalV.USER_TOKEN != "")
                {
                    if (qty != null)
                    {
                        var getProduct = ProductConnect.GetDataProductAsync(id).Result.result;
                        var variantId = getProduct.productVariations[0].productVariationId;
                        CartConnect.AddToCartAsync(new AddToCartRequest { productVariationId = variantId, quantity = qty.Value });
                        Session["ListCart"] = CartConnect.GetListCart().Result.result;
                        if(mode!=null) return RedirectToAction("Cart");
                    }
                    else
                    {
                        var productVariatId = Request["productVariant"];
                        var quantity = Request["quantityProduct"];
                        productVariatId = productVariatId.Split('/')[0];
                        CartConnect.AddToCartAsync(new AddToCartRequest { productVariationId = productVariatId, quantity = int.Parse(quantity) });
                        Session["ListCart"] = CartConnect.GetListCart().Result.result;
                    }
                }
                else return RedirectToAction("Login", "User");
            }
            TempData["idProduct"] = id;
            return RedirectToAction("Product_Details", "Shop");
        }
        public ActionResult UpdateCart(string id,string pId,string pVId)
        {
            if (id != null)
            {   
                int qty = 1;
                var data = ProductConnect.GetDataProductAsync(pId).Result.result;
                int cartQuantity = CartConnect.GetListCart().Result.result.Count;
                for (int i = 0; i < cartQuantity; i++)
                {
                    if (Request["quantityProduct_" + (i + 1)] != null)
                    {
                        qty = int.Parse(Request["quantityProduct_" + (i + 1)].ToString());
                        break;
                    }
                }
                var dataV = data.productVariations.Where(x => x.productVariationId == pVId).ToList()[0];
                if (qty > dataV.quantity) qty = dataV.quantity;
                if (qty < 0) return View("Cart");
                else if (qty == 0) return RedirectToAction("RemoveCart", new { id = id });
                if (GlobalV.USER_TOKEN != "")
                {
                    CartConnect.CartUpdateAsync(new UpdateCartRequest { cartDetailId = id, quantity = qty });
                    Session["ListCart"] = CartConnect.GetListCart().Result.result;
                }
            }
            return View("Cart");
        }

        //Remove
        public ActionResult RemoveCart(string id)
        {
            if (id != null && id.Trim() != "")
            {
                if (GlobalV.USER_TOKEN != "")
                {
                    CartConnect.DeleteCart(new DeleteCartRequest { cartDetailId = id });
                    Session["ListCart"] = CartConnect.GetListCart().Result.result;
                }
                else return RedirectToAction("Login", "User");
            }
            return RedirectToAction("Cart");
        }
        #endregion

        #region Purchase
        public ActionResult Purchase()
        {
            if (Session["LoginFlag"] != null)
            {
                //Voucher Process Start
                var voucher = Request["selectVoucher"];
                if (!voucher.Contains("blankVoucher")) Session.Add("IdVoucher", voucher);
                //Voucher Process End
                //--
                ViewBag.listShipment = ShipmentConnect.GetListShipment(new GetListShipmentRequest { PageIndex = GlobalV.PAGE_INDEX }).Result.result; ;
                //Shipment Process End
                //--
                //Payment Process Start
                var paymentList = PaymentConnect.GetListPayment(new GetListPaymentRequest { PageIndex = GlobalV.PAGE_INDEX }).Result.result;
                var listPayment = new List<PaymentGetResponse.Result>();
                if (paymentList != null)
                {
                    if (paymentList.data.Count > 0)
                    {
                        foreach (var item in paymentList.data)
                        {
                            listPayment.Add(PaymentConnect.GetDataPaymentAsync(item.paymentId).Result.result);
                        }
                    }
                }
                ViewBag.listPayment = listPayment;
                //Payment Process End

                var productId = TempData["productId"] as string;
                if (productId != null)
                {
                    var dataP = ProductConnect.GetDataProductAsync(TempData["productId"].ToString()).Result;
                    if (dataP.result != null)
                    {
                        return View(dataP);
                    }
                }
                if (Session["ListCart"] != null) return View();
                return RedirectToAction("Cart");
            }
            return RedirectToAction("Login", "User");
        }

        //Order Process
        public ActionResult PurchaseProcess()
        {
            bool checkFinish = false;
            if (Session["ListCart"] != null)
            {
                //Voucher
                var voucher = "";
                if (Session["IdVoucher"] != null)
                {
                    var strArray = Session["IdVoucher"].ToString().Split('/');
                    voucher = strArray[0];
                }
                Session.Remove("IdVoucher");
                //---
                //Shipment
                var shipment = Request["selectShipment"];
                if (shipment == "blankShipment") shipment = "";
                //---
                //Payment
                var payment = Request["usPayment"];
                //---
                //User
                var user = UserConnect.GetDataUserAsync().Result.result;
                //---
                //Form User
                var usName = Request["usName"];
                var usPhone = Request["usPhone"];
                var usAddress = Request["usAddress"];
                if (usName == "") usName = user.name;
                if (usPhone == "") usPhone = user.phone;
                if (usAddress == "") usAddress = user.address;

                var listCart = Session["ListCart"] as List<CartResponse.Cart>;
                var listVariants = new List<CreateOrderRequest.Variant>();
                foreach (var item in listCart)
                {
                    listVariants.Add(new CreateOrderRequest.Variant { productVariationId = item.productVariantId, quantity = item.quantity });
                }
                var status = OrderConnect.CreateOrderAsync(new CreateOrderRequest
                {
                    paymentId = payment,
                    shipmentId = shipment,
                    voucherId = voucher,
                    deliveryName = usName,
                    deliveryAddress = usPhone,
                    deliveryPhone = usAddress,
                    status = 1,
                    shipmentPrice = 15,
                    productVariants = listVariants
                });

                if (status.Result == "OK")
                {
                    Session["ListCart"] = null;
                    Session["ListOrder"] = OrderConnect.GetListOrder(new GetListOrderRequest { PageIndex = GlobalV.PAGE_INDEX }).Result.result;
                    checkFinish = true;
                }
            }
            TempData["checkSuccess"] = false;
            if (checkFinish) TempData["checkSuccess"] = true;
            TempData["createTime"] = DateTime.Now;
            return RedirectToAction("PurchaseMessage", "Home");
        }

        //Process End
        // Message View
        public ActionResult PurchaseMessage()
        {
            if (TempData["checkSuccess"] != null)
            {
                if ((bool)TempData["checkSuccess"]) ViewBag.checkSuccess = true;
                else ViewBag.checkSuccess = false;
            }
            if (TempData["createTime"] != null)
            {
                ViewBag.createTime = TempData["createTime"];
            }
            return View();
        }
        #endregion
    }
}