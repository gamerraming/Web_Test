using HTechWeb.Models.Class.GlobalVariable;
using HTechWeb.Models.Class.Urls;
using HTechWeb.Models.DTO.Home.Product.Request;
using HTechWeb.Models.DTO.Home.Product.Response;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;

namespace HTechWeb.Models.Process.Home.Product_API
{
    public class ProductConnect
    {
        //GET List Product
        public static async Task<ProductResponse> GetListProduct(GetListProductRequest request)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", GlobalV.USER_TOKEN);
                string url = Urls.GET_SHOP_PRODUCT_PATH;
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
                var responseJson = JsonConvert.DeserializeObject<ProductResponse>(responseString);
                return responseJson;

            }
        }
        //
        //GET List Category
        public static async Task<CategoryResponse> GetListCategory(GetListCategoryRequest request)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", GlobalV.USER_TOKEN);
                string url = Urls.GET_SHOP_PRODUCT_PATH + "/category";
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
                var responseJson = JsonConvert.DeserializeObject<CategoryResponse>(responseString);
                return responseJson;

            }
        }
        //
        //GET Product
        public static async Task<ProductGetResponse> GetDataProductAsync(string id)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", GlobalV.USER_TOKEN);
                string url = Urls.GET_SHOP_PRODUCT_PATH + "/" + id;

                var resp = await client.GetAsync(url).ConfigureAwait(false);
                var responseString = await resp.Content.ReadAsStringAsync();
                var responseJson = JsonConvert.DeserializeObject<ProductGetResponse>(responseString);
                return responseJson;

            }
        }
        //Get Category
        public static async Task<CategoryGetResponse> GetDataCategoryAsync(GetDataCategoryRequest request)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", GlobalV.USER_TOKEN);
                string url = Urls.GET_SHOP_PRODUCT_PATH + "/group-by-category";

                var uriBuilder = new UriBuilder(url);
                var query = HttpUtility.ParseQueryString(uriBuilder.Query);

                query["categoryId"] = request.categoryId;
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
                var responseJson = JsonConvert.DeserializeObject<CategoryGetResponse>(responseString);
                return responseJson;

            }
        }

    }
}