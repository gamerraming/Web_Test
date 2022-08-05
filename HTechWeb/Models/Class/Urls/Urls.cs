using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HTechWeb.Models.Class.Urls
{
    public class Urls
    {
        private const string BASE_URL = "https://localhost:44311/";

        //UserLogin
        public const string GET_USERLOGIN_PATH = BASE_URL + "api/web";

        //Shop Product
        public const string GET_SHOP_PRODUCT_PATH = BASE_URL + "api/web/product";

        //Shop
        public const string GET_SHOP_PATH = BASE_URL + "api/web/shop";

        //Shop Cart
        public const string GET_SHOP_CART_PATH = BASE_URL + "api/web/cart";

        //Shipment
        public const string GET_SHIPMENT_PATH = BASE_URL + "api/admin/shipment";

        //Voucher
        public const string GET_VOUCHER_PATH = BASE_URL + "api/admin/voucher";

        //Payment
        public const string GET_PAYMENT_PATH = BASE_URL + "api/admin/payment";

        //Order
        public const string GET_ORDER_PATH = BASE_URL + "api/web/order";
    }
}