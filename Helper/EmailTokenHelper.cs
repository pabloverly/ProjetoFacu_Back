using Microsoft.Extensions.Configuration;
// using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text;
using Microsoft.Identity.Client;
using System.Net.Http.Headers;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using ApiTools.Model;

namespace ApiTools.Helper
{

    public class EmailTokenHelper
    {
        public EmailTokenHelper()
        {
        }

        public async Task<string> SendEmails()
        {
            string accessToken = "";
            string clientId = "8e6867a4-aaa6-4550-b111-771ef9ba7c86";
            string clientSecret = "05d8Q~6.Vmku0BOo5qvBQ3v6KV9MM8~8mMOqqbEH";

            string tenantId = "d87614eb-ca0e-46d3-9be4-e949369eb034";
            try
            {
                IConfidentialClientApplication confidentialClientApplication = ConfidentialClientApplicationBuilder
                    .Create(clientId)
                    .WithClientSecret(clientSecret)
                    .WithAuthority($"https://login.microsoftonline.com/{tenantId}")
                    .Build();

                string[] scopes = new string[] { "https://graph.microsoft.com/.default" };

                AuthenticationResult authResult = await confidentialClientApplication.AcquireTokenForClient(scopes)
                    .ExecuteAsync();

                accessToken = authResult.AccessToken;

            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocorreu um erro ao enviar o correio eletrônico:" + ex.Message);
            }
            return accessToken;
        }

    }

}