using HTechWeb.Models.Class.GlobalVariable;
using HTechWeb.Models.Class.Urls;
using HTechWeb.Models.DTO.Home.Order.Request;
using HTechWeb.Models.DTO.User.Request;
using HTechWeb.Models.DTO.User.Response;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace HTechWeb.Models.Process.User.User_API
{
    public class UserConnect
    {
        #region Method
        //Login
        public static async Task<UserLoginResponse> UserLoginAsync(UserLoginRequest request)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                string jsonBody = JsonConvert.SerializeObject(request);

                var requestGet = new HttpRequestMessage
                {
                    Method = HttpMethod.Post,
                    RequestUri = new Uri(Urls.GET_USERLOGIN_PATH + "/login"),
                    Content = new StringContent(jsonBody, Encoding.UTF8, "application/json")
                };
                var resp = client.SendAsync(requestGet).Result;
                var responseString = await resp.Content.ReadAsStringAsync();
                var responseJson = JsonConvert.DeserializeObject<UserLoginResponse>(responseString);
                return responseJson;
            }
        }
        //Get User Profile
        public static async Task<UserProfileResponse> GetDataUserAsync()
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", GlobalV.USER_TOKEN);
                string url = Urls.GET_USERLOGIN_PATH + "/" + "profile";

                var resp = await client.GetAsync(url).ConfigureAwait(false);
                var responseString = await resp.Content.ReadAsStringAsync();
                var responseJson = JsonConvert.DeserializeObject<UserProfileResponse>(responseString);
                return responseJson;

            }
        }

        //Register
        public static async Task<string> UserRegisterAsync(UserRegisterRequest request)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", GlobalV.USER_TOKEN);
                string jsonBody = JsonConvert.SerializeObject(request);

                var requestGet = new HttpRequestMessage
                {
                    Method = HttpMethod.Post,
                    RequestUri = new Uri(Urls.GET_USERLOGIN_PATH+"/"+"register"),
                    Content = new StringContent(jsonBody, Encoding.UTF8, "application/json")
                };
                var resp = client.SendAsync(requestGet).ConfigureAwait(false);
                return resp.GetAwaiter().GetResult().StatusCode.ToString();
                
            }
        }

        //Change Password
        public static async Task<string> UserChangePasswordAsync(UserChangePasswordRequest request)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", GlobalV.USER_TOKEN);
                string jsonBody = JsonConvert.SerializeObject(request);

                var requestGet = new HttpRequestMessage
                {
                    Method = HttpMethod.Post,
                    RequestUri = new Uri(Urls.GET_USERLOGIN_PATH + "/" + "change-password"),
                    Content = new StringContent(jsonBody, Encoding.UTF8, "application/json")
                };
                var resp = client.SendAsync(requestGet).ConfigureAwait(false);
                return resp.GetAwaiter().GetResult().StatusCode.ToString();
            }
        }

        //Update
        public static async Task<string> UserUpdateAsync(UserUpdateRequest request)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", GlobalV.USER_TOKEN);
                string jsonBody = JsonConvert.SerializeObject(request);

                var requestGet = new HttpRequestMessage
                {
                    Method = HttpMethod.Put,
                    RequestUri = new Uri(Urls.GET_USERLOGIN_PATH + "/" + "profile"),
                    Content = new StringContent(jsonBody, Encoding.UTF8, "application/json")
                };
                var resp = client.SendAsync(requestGet).ConfigureAwait(false);
                return resp.GetAwaiter().GetResult().StatusCode.ToString();
            }
        }
        public static async Task<string> UploadImageAsync(string picturePath)
        {
            using (var multipartFormContent = new MultipartFormDataContent())
            {
                HttpClient client = new HttpClient();
                //Load the file and set the file's Content-Type header
                var fileStreamContent = new StreamContent(File.OpenRead(picturePath));
                fileStreamContent.Headers.ContentType = new MediaTypeHeaderValue("image/png");

                //Add the file
                multipartFormContent.Add(fileStreamContent);

                //Send it
                var response = await client.PostAsync("https://api.imgbb.com/1/upload?key=90a870cf57f31daa44e84340904c27b9", multipartFormContent);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            }
        }

        //Order cancel
        public static async Task<string> OrderCancelAsync(CancelOrderRequest request)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", GlobalV.USER_TOKEN);
                string jsonBody = JsonConvert.SerializeObject(request);

                var requestGet = new HttpRequestMessage
                {
                    Method = HttpMethod.Post,
                    RequestUri = new Uri(Urls.GET_ORDER_PATH + "/cancel/"+request.orderId),
                    Content = new StringContent(jsonBody, Encoding.UTF8, "application/json")
                };
                var resp = client.SendAsync(requestGet).ConfigureAwait(false);
                return resp.GetAwaiter().GetResult().StatusCode.ToString();
            }
        }

        #endregion
    }
}