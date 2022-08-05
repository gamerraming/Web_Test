using HTechWeb.Models.Class.GlobalVariable;
using HTechWeb.Models.Class.Urls;
using HTechWeb.Models.DTO.Home.Order.Request;
using HTechWeb.Models.DTO.Home.Order.Response;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace HTechWeb.Models.Process.Home.Order_API
{
    public class OrderConnect
    {
        //GET List Order
        public static async Task<OrderResponse> GetListOrder(GetListOrderRequest request)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", GlobalV.USER_TOKEN);
                string url = Urls.GET_ORDER_PATH;
                var uriBuilder = new UriBuilder(url);
                var query = HttpUtility.ParseQueryString(uriBuilder.Query);

                query["SortBy"] = request.SortBy;
                query["SortOrder"] = request.SortOrder;
                query["Keyword"] = request.Keyword;
                query["FilterByStatus"] = request.FilterByStatus;
                query["PageSize"] = request.PageSize.ToString();
                query["PageIndex"] = request.PageIndex.ToString();
                query["Offset"] = request.Offset.ToString();

                uriBuilder.Query = query.ToString();
                url = uriBuilder.ToString();

                var resp = await client.GetAsync(url).ConfigureAwait(false);
                var responseString = await resp.Content.ReadAsStringAsync();
                var responseJson = JsonConvert.DeserializeObject<OrderResponse>(responseString);
                return responseJson;
            }
        }
        //
        //Add to Order
        public static async Task<string> CreateOrderAsync(CreateOrderRequest request)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", GlobalV.USER_TOKEN);
                string jsonBody = JsonConvert.SerializeObject(request);

                var requestGet = new HttpRequestMessage
                {
                    Method = HttpMethod.Post,
                    RequestUri = new Uri(Urls.GET_ORDER_PATH + "/" + "create-order"),
                    Content = new StringContent(jsonBody, Encoding.UTF8, "application/json")
                };
                var resp = client.SendAsync(requestGet).ConfigureAwait(false);
                return resp.GetAwaiter().GetResult().StatusCode.ToString();
            }
        }
        //GET Order
        public static async Task<OrderGetResponse> GetDataOrderAsync(string id)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", GlobalV.USER_TOKEN);
                string url = Urls.GET_ORDER_PATH + "/" + id;

                var resp = await client.GetAsync(url).ConfigureAwait(false);
                var responseString = await resp.Content.ReadAsStringAsync();
                var responseJson = JsonConvert.DeserializeObject<OrderGetResponse>(responseString);
                return responseJson;

            }
        }
    }
}