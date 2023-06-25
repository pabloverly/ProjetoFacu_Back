using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ApiTools.Helper
{

    public class RecaptchaHelper
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _httpClientFactory;

        public RecaptchaHelper(IConfiguration configuration, IHttpClientFactory httpClientFactory)
        {
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<bool> VerifyRecaptcha(string token)
        {
            var secretKey = _configuration["Recaptcha:SecretKey"];
            var httpClient = _httpClientFactory.CreateClient();

            var response = await httpClient.GetAsync($"https://www.google.com/recaptcha/api/siteverify?secret={secretKey}&response={token}");
            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var json = JsonConvert.DeserializeObject<JObject>(jsonString);

                return json.Value<bool>("success");
            }

            return false;
        }
    }

}