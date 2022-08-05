using HTechWeb.Models.Class.GlobalVariable;
using HTechWeb.Models.Class.Urls;
using HTechWeb.Models.DTO.Home.Cart.Request;
using HTechWeb.Models.DTO.Home.Cart.Response;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace HTechWeb.Models.Process.Home.Cart_API
{
    public class CartConnect
    {
        //GET List Cart
        public static async Task<CartResponse> GetListCart()
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", GlobalV.USER_TOKEN);
                string url = Urls.GET_SHOP_CART_PATH;

                var resp = await client.GetAsync(url).ConfigureAwait(false);
                var responseString = await resp.Content.ReadAsStringAsync();
                var responseJson = JsonConvert.DeserializeObject<CartResponse>(responseString);
                return responseJson;
            }
        }
        //
        //Add to cart
        public static async Task<string> AddToCartAsync(AddToCartRequest request)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", GlobalV.USER_TOKEN);
                string jsonBody = JsonConvert.SerializeObject(request);

                var requestGet = new HttpRequestMessage
                {
                    Method = HttpMethod.Post,
                    RequestUri = new Uri(Urls.GET_SHOP_CART_PATH + "/" + "add-to-cart"),
                    Content = new StringContent(jsonBody, Encoding.UTF8, "application/json")
                };
                var resp = client.SendAsync(requestGet).ConfigureAwait(false);
                return resp.GetAwaiter().GetResult().StatusCode.ToString();
            }
        }
        //Delete Cart
        public static async Task DeleteCart(DeleteCartRequest request)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", GlobalV.USER_TOKEN);
                string url = Urls.GET_SHOP_CART_PATH+"/"+"remove-product";
                var uriBuilder = new UriBuilder(url);
                var query = HttpUtility.ParseQueryString(uriBuilder.Query);

                query["CartDetailId"] = request.cartDetailId;

                uriBuilder.Query = query.ToString();
                url = uriBuilder.ToString();

                await client.DeleteAsync(url).ConfigureAwait(false);
            }
        }

        //Update 
        public static async Task<string> CartUpdateAsync(UpdateCartRequest request)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", GlobalV.USER_TOKEN);
                string jsonBody = JsonConvert.SerializeObject(request);

                var requestGet = new HttpRequestMessage
                {
                    Method = HttpMethod.Put,
                    RequestUri = new Uri(Urls.GET_SHOP_CART_PATH + "/" + "update-cart"),
                    Content = new StringContent(jsonBody, Encoding.UTF8, "application/json")
                };
                var resp = client.SendAsync(requestGet).ConfigureAwait(false);
                return resp.GetAwaiter().GetResult().StatusCode.ToString();
            }
        }
    }
}