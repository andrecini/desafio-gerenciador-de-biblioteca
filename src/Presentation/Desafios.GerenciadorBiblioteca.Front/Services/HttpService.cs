using Desafios.GerenciadorBiblioteca.Website.Models.HttpService;
using Desafios.GerenciadorBiblioteca.Website.Models.Responses;
using Desafios.GerenciadorBiblioteca.Website.Services.Auth;
using Flurl.Http;
using Flurl.Http.Configuration;
using System.Net;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace Desafios.GerenciadorBiblioteca.Website.Services
{
    public class HttpService
    {
        private readonly string _baseUrl;
        private readonly AuthService _authService;
        private readonly JsonSerializerOptions _options;

        public HttpService(string baseUrl, AuthService authService)
        {
            _baseUrl = baseUrl;
            _authService = authService;
            _options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true,
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            };
        }

        private IFlurlRequest CreateRequest(string uri)
        {
            var tokenModel = _authService.GetTokenModel();

            var token = tokenModel?.Token;

            return new FlurlRequest(_baseUrl + uri)
                .AllowHttpStatus("2xx")
                .WithOAuthBearerToken(token)
                .WithSettings(x => x.JsonSerializer = new DefaultJsonSerializer(new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    WriteIndented = true,
                    Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
                }));
        }

        public async Task<HttpServiceResponse<T>> GetAsync<T>(string uri)
        {
            try
            {
                var response = await CreateRequest(uri).GetAsync();
                return await ProcessResponse<T>(response);
            }
            catch (FlurlHttpException ex)
            {
                return await ProcessErrorResponse<T>(ex);
            }
        }

        public async Task<HttpServiceResponse> PostAsync<TRequest>(string uri, TRequest data)
        {
            try
            {
                var response = await CreateRequest(uri).PostJsonAsync(data);
                return await ProcessResponse(response);
            }
            catch (FlurlHttpException ex)
            {
                return await ProcessErrorResponse(ex);
            }
        }

        public async Task<HttpServiceResponse<T>> PostAsync<TRequest, T>(string uri, TRequest data)
        {
            try
            {
                var response = await CreateRequest(uri).PostJsonAsync(data);
                return await ProcessResponse<T>(response);
            }
            catch (FlurlHttpException ex)
            {
                return await ProcessErrorResponse<T>(ex);
            }
        }

        public async Task<HttpServiceResponse<T>> PutAsync<TRequest, T>(string uri, TRequest data)
        {
            try
            {
                var response = await CreateRequest(uri).PutJsonAsync(data);
                return await ProcessResponse<T>(response);
            }
            catch (FlurlHttpException ex)
            {
                return await ProcessErrorResponse<T>(ex);
            }
        }

        public async Task<HttpServiceResponse> DeleteAsync<TRequest>(string uri, TRequest data)
        {
            try
            {
                var response = await CreateRequest(uri).SendJsonAsync(HttpMethod.Delete, data);
                return await ProcessResponse(response);
            }
            catch (FlurlHttpException ex)
            {
                return await ProcessErrorResponse(ex);
            }
        }

        public async Task<HttpServiceResponse> DeleteAsync(string uri)
        {
            try
            {
                var response = await CreateRequest(uri).SendAsync(HttpMethod.Delete);
                return await ProcessResponse(response);
            }
            catch (FlurlHttpException ex)
            {
                return await ProcessErrorResponse(ex);
            }
        }

        private async Task<HttpServiceResponse<T>> ProcessResponse<T>(IFlurlResponse response)
        {
            var httpServiceResponse = new HttpServiceResponse<T>
            {
                StatusCode = (HttpStatusCode)response.StatusCode,
                RawContent = await response.ResponseMessage.Content.ReadAsStringAsync()
            };

            if (response.ResponseMessage.IsSuccessStatusCode)
            {
                httpServiceResponse.Content = JsonSerializer.Deserialize<T>(httpServiceResponse.RawContent, _options);
            }
            else
            {
                var errorResponse = JsonSerializer.Deserialize<CustomResponse<string[]>>(httpServiceResponse.RawContent, _options);
                httpServiceResponse.ErrorDetails = errorResponse.Data;
            }

            return httpServiceResponse;
        }

        private async Task<HttpServiceResponse> ProcessResponse(IFlurlResponse response)
        {
            var httpServiceResponse = new HttpServiceResponse
            {
                StatusCode = (HttpStatusCode)response.StatusCode,
                Content = await response.ResponseMessage.Content.ReadAsStringAsync()
            };

            if (!response.ResponseMessage.IsSuccessStatusCode)
            {
                var errorResponse = JsonSerializer.Deserialize<CustomResponse<string[]>>(httpServiceResponse.RawContent, _options);
                httpServiceResponse.ErrorDetails = errorResponse.Data;
            }

            return httpServiceResponse;
        }

        private async Task<HttpServiceResponse<T>> ProcessErrorResponse<T>(FlurlHttpException ex)
        {
            var response = new HttpServiceResponse<T>
            {
                StatusCode = ex.Call.Response != null ? (HttpStatusCode)ex.Call.Response.StatusCode : HttpStatusCode.InternalServerError,
                RawContent = ex.Call.Response != null ? await ex.GetResponseStringAsync() : ex.Message
            };

            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                response.ErrorDetails = ["Acesso Negado. Realize o Login para Acessar as Funcionalidades da Aplicação."];
            }
            else
            {
                var errorResponse = JsonSerializer.Deserialize<CustomResponse<string[]>>(response.RawContent, _options);
                response.ErrorDetails = errorResponse.Data;
            }

            return response;
        }

        private async Task<HttpServiceResponse> ProcessErrorResponse(FlurlHttpException ex)
        {
            var response = new HttpServiceResponse
            {
                StatusCode = ex.Call.Response != null ? (HttpStatusCode)ex.Call.Response.StatusCode : HttpStatusCode.InternalServerError,
                Content = ex.Call.Response != null ? await ex.GetResponseStringAsync() : ex.Message
            };

            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                response.ErrorDetails = ["Acesso Negado. Realize o Login para Acessar as Funcionalidades da Aplicação."];
            }
            else
            {
                var errorResponse = JsonSerializer.Deserialize<CustomResponse<string[]>>(response.RawContent, _options);
                response.ErrorDetails = errorResponse.Data;
            }

            return response;
        }
    }
}
