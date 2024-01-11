using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using ApiTools.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ApiTools.Controllers
{
    [ApiController]
    [Route("ADSELFService")]
    public class ADSELFService
    {
        [HttpPost]
        [Route("validate")]
        [AllowAnonymous]
        public async Task<ActionResult<AdselfServiceModel>> PostAdselfService(string code)
        {

            var authValue = "Tm5kMTkyRDZkYXVVbmVSblphdmx5dm94VTp6TmNHbE5KUHYya1J4RnpjMUVQekdROW03aU1YUk5hSGVydm4wTElV";

            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

            // Pass the handler to httpclient(from you are calling api)
            HttpClient client = new HttpClient(clientHandler);
            // var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authValue);

            // var parameters = new Dictionary<string, string>
            // {
            //     { "code", code },
            //     { "redirect_uri", "http://localhost:4200/%23/signin" },
            //     { "grant_type", "authorization_code" }
            // };

            var redirectUri = "http://localhost:4200/";
            var grantType = "authorization_code";

            // Construir a URL com os parâmetros como query parameters
            var apiUrl = $"https://gztvix-admfa:9251/sso/oauth/4746788d1260b35e296915fe66c37c4d596b3966/token?code={code}&redirect_uri={redirectUri}&grant_type={grantType}";


            //"https://gztvix-admfa:9251/sso/oauth/325d404db359b7d93958fbbf423d36f4ac33ab8b/token?code=Xr56t-9RoopIOKgmRk&redirect_uri=http://localhost:4200/%23/signin&grant_type=authorization_code"
            var response = await client.PostAsync(apiUrl, null);
            string responseContent = await response.Content.ReadAsStringAsync();
            AdselfServiceModel responseObject = JsonConvert.DeserializeObject<AdselfServiceModel>(responseContent);
            // AdselfServiceModel responseBody = await response.Content.ReadAsStringAsync();
            //dynamic responseObject = JsonConvert.DeserializeObject(responseBody);
            return responseObject;
        }

    }
}