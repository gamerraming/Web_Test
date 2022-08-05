using HTechWeb.Models.Class.GlobalVariable;
using HTechWeb.Models.Class.Urls;
using HTechWeb.Models.DTO.Shop.Request;
using HTechWeb.Models.DTO.Shop.Response;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace HTechWeb.Models.Process.Shop.Shop_API
{
    public class ShopConnect
    {
        //Register
        public static async Task<string> ShopRegisterAsync(ShopRegisterRequest request)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", GlobalV.USER_TOKEN);
                string jsonBody = JsonConvert.SerializeObject(request);

                var requestGet = new HttpRequestMessage
                {
                    Method = HttpMethod.Post,
                    RequestUri = new Uri(Urls.GET_SHOP_PATH + "/" + "shop-register"),
                    Content = new StringContent(jsonBody, Encoding.UTF8, "application/json")
                };
                var resp = client.SendAsync(requestGet).ConfigureAwait(false);
                return resp.GetAwaiter().GetResult().StatusCode.ToString();

            }
        }
        //Get Shop
        public static async Task<ShopGetResponse> GetDataShopAsync(GetShopRequest request)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", GlobalV.USER_TOKEN);
                string url = Urls.GET_SHOP_PATH + "/" + request.shopId;
                var uriBuilder = new UriBuilder(url);
                var query = HttpUtility.ParseQueryString(uriBuilder.Query);

                query["Keyword"] = request.Keyword;
                query["SortBy"] = request.SortBy;
                query["SortOrder"] = request.SortOrder;

                uriBuilder.Query = query.ToString();
                url = uriBuilder.ToString();

                var resp = await client.GetAsync(url).ConfigureAwait(false);
                var responseString = await resp.Content.ReadAsStringAsync();
                var responseJson = JsonConvert.DeserializeObject<ShopGetResponse>(responseString);
                return responseJson;

            }
        }

    }
}