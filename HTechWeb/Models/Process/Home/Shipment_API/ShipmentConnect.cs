using HTechWeb.Models.Class.GlobalVariable;
using HTechWeb.Models.Class.Urls;
using HTechWeb.Models.DTO.Home.Shipment.Request;
using HTechWeb.Models.DTO.Home.Shipment.Response;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;

namespace HTechWeb.Models.Process.Home.Shipment_API
{
    public class ShipmentConnect
    {
        //GET List Shipment
        public static async Task<ShipmentResponse> GetListShipment(GetListShipmentRequest request)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", GlobalV.USER_TOKEN);
                string url = Urls.GET_SHIPMENT_PATH;
                var uriBuilder = new UriBuilder(url);
                var query = HttpUtility.ParseQueryString(uriBuilder.Query);

                query["SortBy"] = request.SortBy;
                query["SortOrder"] = request.SortOrder;
                query["Keyword"] = request.Keyword;
                query["PageSize"] = request.PageSize.ToString();
                query["PageIndex"] = request.PageIndex.ToString();
                query["Offset"] = request.Offset.ToString();

                uriBuilder.Query = query.ToString();
                url = uriBuilder.ToString();

                var resp = await client.GetAsync(url).ConfigureAwait(false);
                var responseString = await resp.Content.ReadAsStringAsync();
                var responseJson = JsonConvert.DeserializeObject<ShipmentResponse>(responseString);
                return responseJson;
            }
        }
        //
        //GET Shipment
        public static async Task<ShipmentGetResponse> GetDataShipmentAsync(string id)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", GlobalV.USER_TOKEN);
                string url = Urls.GET_SHIPMENT_PATH + "/" + id;

                var resp = await client.GetAsync(url).ConfigureAwait(false);
                var responseString = await resp.Content.ReadAsStringAsync();
                var responseJson = JsonConvert.DeserializeObject<ShipmentGetResponse>(responseString);
                return responseJson;

            }
        }
    }
}