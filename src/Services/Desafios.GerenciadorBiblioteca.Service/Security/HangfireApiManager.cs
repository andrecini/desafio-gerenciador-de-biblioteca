using Desafios.GerenciadorBiblioteca.Domain.Exceptions;
using Desafios.GerenciadorBiblioteca.Service.Security.Interfaces;
using Flurl.Http;
using Flurl.Http.Configuration;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace Desafios.GerenciadorBiblioteca.Service.Security
{
    public class HangfireApiManager : IHangfireApiManager
    {
        private readonly string _baseUrl;
        private readonly string _sendVerificationCode = "verification-codes/generate";
        private readonly JsonSerializerOptions _options;

        public HangfireApiManager(IConfiguration configuration)
        {
            _baseUrl = configuration["Hangfire:BaseUrl"] ?? string.Empty;
        }

        private async Task<FlurlRequest> CreateRequest(string uri)
        {
            return new FlurlRequest(_baseUrl + uri)
                .AllowHttpStatus("2xx")
                .WithSettings(x => x.JsonSerializer = new DefaultJsonSerializer(new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    WriteIndented = true,
                    Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
                }));
        }

        public async Task GetAsync(int userId, string email)
        {
            try
            {
                IFlurlRequest request = await CreateRequest($"api/tasks/verification-codes/generate?userId={userId}&userEmail={email}");

                request = request
                    .SetQueryParam("userId", userId)
                    .SetQueryParam("userEmail", email);

                var response = await request.GetAsync();
            }
            catch (FlurlHttpException ex)
            {
                var error = ex.Call.Response != null ? await ex.GetResponseStringAsync() : ex.Message;
                throw new CustomException(error, HttpStatusCode.InternalServerError);
            }
        }
    }
}
