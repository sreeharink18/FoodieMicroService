
using Newtonsoft.Json;
using System.Text;
using WebMVC.Models;
using WebMVC.Service.IService;
using static WebMVC.Utility.SD;
using System.Net;


namespace WebMVC.Service
{
    public class BaseService : IBaseService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private ITokenProvider _tokenProvider;
        public BaseService(IHttpClientFactory httpClientFactory, ITokenProvider tokenProvider)
        {
            _httpClientFactory = httpClientFactory;
            _tokenProvider = tokenProvider;
        }
        public async Task<ApiResponse?> SendAsync(RequestDTO requestDTO, bool withBearer = true)
        {
            try
            {
                HttpClient httpClient = _httpClientFactory.CreateClient("FoodieAPI");
                HttpRequestMessage message = new();
                message.Headers.Add("Accept", "application/json");


                //token
                if (withBearer) {
                    var token = _tokenProvider.GetToken();
                    message.Headers.Add("Authorization",$"Bearer {token}");
                }

                message.RequestUri = new Uri(requestDTO.Url);
                if (requestDTO.Data != null)
                {
                    message.Content = new StringContent(JsonConvert.SerializeObject(requestDTO.Data), Encoding.UTF8, "application/json");
                }

                HttpResponseMessage? apiResponse = null;

                switch (requestDTO.ApiType)
                {
                    case ApiType.POST:
                        message.Method = HttpMethod.Post;
                        break;
                    case ApiType.PUT:
                        message.Method = HttpMethod.Put;
                        break;
                    case ApiType.DELETE:
                        message.Method = HttpMethod.Delete;
                        break;
                    default:
                        message.Method = HttpMethod.Get;
                        break;
                }
                apiResponse = await httpClient.SendAsync(message);

                switch (apiResponse.StatusCode)
                {
                    case HttpStatusCode.NotFound:
                        return APIResponseHelper.ErrorResponse("Not found",HttpStatusCode.NotFound);
                    case HttpStatusCode.Unauthorized:
                        return APIResponseHelper.ErrorResponse("Unauhorized",HttpStatusCode.Unauthorized);
                    case HttpStatusCode.Forbidden:
                        return APIResponseHelper.ErrorResponse("Access Denied ",HttpStatusCode.Forbidden);
                    case HttpStatusCode.InternalServerError:
                        return APIResponseHelper.ErrorResponse("Internal Server Error",HttpStatusCode.InternalServerError);
   
                    default:
                        var apiContent = await apiResponse.Content.ReadAsStringAsync();
                        var apiResponseDto = JsonConvert.DeserializeObject<ApiResponse>(apiContent);
                        return apiResponseDto;
                }
            }
            catch (Exception ex) { 
                return APIResponseHelper.ErrorResponse(ex.ToString());
            }
           
        }
    }
}
