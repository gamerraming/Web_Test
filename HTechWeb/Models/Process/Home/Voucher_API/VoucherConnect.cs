using HTechWeb.Models.Class.GlobalVariable;
using HTechWeb.Models.Class.Urls;
using HTechWeb.Models.DTO.Home.Voucher.Request;
using HTechWeb.Models.DTO.Home.Voucher.Response;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;

namespace HTechWeb.Models.Process.Home.Voucher_API
{
    public class VoucherConnect
    {
        //GET List Voucher
        public static async Task<VoucherResponse> GetListVoucher(GetListVoucherRequest request)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", GlobalV.USER_TOKEN);
                string url = Urls.GET_VOUCHER_PATH;
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
                var responseJson = JsonConvert.DeserializeObject<VoucherResponse>(responseString);
                return responseJson;
            }
        }
        //

    }
}