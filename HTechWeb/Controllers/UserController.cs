using HTechWeb.Models.Class.GlobalVariable;
using HTechWeb.Models.DTO.Home.Cart.Request;
using HTechWeb.Models.DTO.Home.Order.Request;
using HTechWeb.Models.DTO.Home.Voucher.Request;
using HTechWeb.Models.DTO.Shop.Request;
using HTechWeb.Models.DTO.User.Request;
using HTechWeb.Models.DTO.User.Response;
using HTechWeb.Models.Process.Home.Cart_API;
using HTechWeb.Models.Process.Home.Order_API;
using HTechWeb.Models.Process.Home.Voucher_API;
using HTechWeb.Models.Process.Shop.Shop_API;
using HTechWeb.Models.Process.User.User_API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HTechWeb.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        //User Action Start
        #region Action
        //Login
        public ActionResult Login()
        {
            if (TempData["usRegister"] != null)
            {
                if ((bool)TempData["usRegister"]) ViewBag.usRegister = true;
                else ViewBag.usRegister = false;
            }
            if (TempData["usLogin"] != null)
            {
                if ((bool)TempData["usLogin"]) ViewBag.usLogin = true;
                else ViewBag.usLogin = false;
            }
            return View();
        }


        [HttpPost]
        public ActionResult CheckLogin()
        {
            TempData["usLogin"] = false;
            string id = Request["usernameLogin"];
            string pwd = Request["passwordLogin"];
            var resultUser = UserConnect.UserLoginAsync(new UserLoginRequest { UserNameOrEmailAddress = id, Password = pwd });
            if (resultUser.Result.result != null)
            {
                GlobalV.USER_TOKEN = resultUser.Result.result.accessToken;
                Session.Add("LoginFlag", true);
                var listOrder = OrderConnect.GetListOrder(new GetListOrderRequest { PageIndex = GlobalV.PAGE_INDEX }).Result.result;
                var user = UserConnect.GetDataUserAsync().Result.result;
                Session.Add("UserName", user.username);
                //
                Session.Add("ListVoucher", VoucherConnect.GetListVoucher(new GetListVoucherRequest { PageIndex = GlobalV.PAGE_INDEX }).Result.result);
                Session.Add("ListCart", CartConnect.GetListCart().Result.result);
                Session.Add("User", user);
                Session.Add("ListOrder", listOrder);
                TempData["usLogin"] = true;
                return RedirectToAction("User_Profile");
            }
            return RedirectToAction("Login");
        }

        [HttpPost]
        public ActionResult CheckRegister()
        {
            TempData["usRegister"] = false;
            //
            string id = Request["usUsername"];
            string pwd = Request["usPassword"];
            string repwd = Request["usRePassword"];
            string email = Request["usEmail"];
            string fname = Request["usFName"];
            string lname = Request["usLName"];
            string phone = Request["usPhone"];
            if (id != "" && pwd != "" && email != "" && repwd != "")
            {
                var status = UserConnect.UserRegisterAsync(new UserRegisterRequest { email = email, password = pwd, userName = id, firstName = fname, lastName = lname, phoneNumber = phone, retypePassword = repwd });
                if (status.Result == "OK") TempData["usRegister"] = true;
            }
            return RedirectToAction("Login");
        }
        //change password
        [HttpPost]
        public ActionResult UserPasswordChange()
        {
            string oldPwd = Request["oldPassword"];
            string newPwd = Request["newPassword"];
            string reNewPwd = Request["reNewPassword"];
            if (oldPwd != null && newPwd != null && reNewPwd != null)
            {
                if (oldPwd != "" && newPwd != "" && reNewPwd != "")
                {
                    var status = UserConnect.UserChangePasswordAsync(new UserChangePasswordRequest { oldPassword = oldPwd, newPassword = newPwd, confirmPassword = reNewPwd });
                    if (status.Result == "OK")
                    {
                        TempData["changeSuccess"] = true;
                        return RedirectToAction("User_Profile");
                    }
                }
            }
            TempData["changeSuccess"] = false;
            return RedirectToAction("User_Profile");
        }

        //update
        public ActionResult UserUpdate()
        {
            var user = UserConnect.GetDataUserAsync().Result.result;
            string name = Request["usFName"];
            string surName = Request["usLName"];
            string address = Request["usAddress"];
            string phone = Request["usPhone"];
            string birthday = Request["usBirthday"];
            string gender = Request["usGender"];
            string picture = Request["usPicture"];
            if (picture == "") picture = user.profilePicture;
            var status = UserConnect.UserUpdateAsync(new UserUpdateRequest { profilePicture = picture, name = name, address = address, birthDay = DateTime.Parse(birthday), gender = int.Parse(gender), phone = phone, surname = surName });
            if (status.Result == "OK")
            {
                TempData["updateUser"] = true;
                user = UserConnect.GetDataUserAsync().Result.result;
                Session["User"] = user;
                Session["userPicture"] = user.profilePicture;
                return RedirectToAction("User_Profile");
            }
            TempData["updateUser"] = false;
            return RedirectToAction("User_Profile");
        }

        //user profile
        public ActionResult User_Profile(string id)
        {
            if (Session["LoginFlag"] != null)
            {
                if (TempData["showListOrder"] != null)
                {
                    if ((bool)TempData["showListOrder"]) ViewBag.showListOrder = true;
                    else ViewBag.showListOrder = false;
                }
                if (TempData["shopOpened"] != null)
                {
                    if ((bool)TempData["shopOpened"]) ViewBag.shopOpened = true;
                    else ViewBag.shopOpened = false;
                }
                if (TempData["changeSuccess"] != null)
                {
                    if ((bool)TempData["changeSuccess"]) ViewBag.changeSuccess = true;
                    else ViewBag.changeSuccess = false;
                }
                if (TempData["updateUser"] != null)
                {
                    if ((bool)TempData["updateUser"]) ViewBag.updateUser = true;
                    else ViewBag.updateUser = false;
                }
                if (id != null)
                {
                    ViewBag.eventActionUser = "";
                    if (id == "orderListShow") ViewBag.eventActionUser = "orderListShow";
                }
                return View(Session["User"] as UserProfileResponse.Result);
            }
            return RedirectToAction("Login");
        }

        //Logout
        public ActionResult UserLogout()
        {
            //remove all sessions information of user
            Session.Remove("LoginFlag");
            Session.Remove("UserName");
            Session.Remove("ListCart");
            Session.Remove("ListOrder");
            Session.Remove("ListVoucher");
            return RedirectToAction("Login");
        }
        #endregion

        #region Process Other
        //Change page Order
        public ActionResult ChangePageOrder(int pageIndex)
        {
            Session["ListOrder"] = OrderConnect.GetListOrder(new GetListOrderRequest { PageIndex = pageIndex }).Result.result;
            TempData["showListOrder"] = true;
            return RedirectToAction("User_Profile", "User");
        }

        public ActionResult Order_Details(string id)
        {
            if (id != null)
                return View(OrderConnect.GetDataOrderAsync(id).Result.result);
            return RedirectToAction("User_Profile");
        }

        //Cancel Order
        public ActionResult CancelOrder(string id)
        {
            var status = UserConnect.OrderCancelAsync(new CancelOrderRequest { orderId = id });
            if (status.Result == "OK")
            {
                Session["ListOrder"] = OrderConnect.GetListOrder(new GetListOrderRequest { PageIndex = GlobalV.PAGE_INDEX }).Result.result;
                return RedirectToAction("User_Profile");
            }
            return RedirectToAction("Order_Details", new { id = id });
        }
        #endregion

        #region shop
        public ActionResult ShopRegister()
        {
            string name = Request["shopName"];
            var status = ShopConnect.ShopRegisterAsync(new ShopRegisterRequest { shopName = name });
            TempData["shopOpened"] = false;
            if (status.Result == "OK") TempData["shopOpened"] = true;
            Session["User"] = UserConnect.GetDataUserAsync().Result.result;
            return RedirectToAction("User_Profile");
        }
        #endregion

        //User Action End
    }
}