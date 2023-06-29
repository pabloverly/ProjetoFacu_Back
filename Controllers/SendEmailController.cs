using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Text;
using Microsoft.Identity.Client;
using System.Net.Http.Headers;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using ApiTools.Model;
using ApiTools.Helper;

namespace ApiTools.Controllers
{
    [Route("[controller]")]
    public class SendEmailController : Controller
    {
        private readonly ILogger<SendEmailController> _logger;

        public SendEmailController(ILogger<SendEmailController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        [Route("sendemail")]
        [Authorize]
        public async Task<ActionResult<dynamic>> SendEmail(Email model)
        {
            EmailTokenHelper sendEmailController = new EmailTokenHelper();
            string accessToken = await sendEmailController.SendEmails();

            string mailUser = "itsupport@redegazeta.com.br";
            string sendMailEndpoint = $"https://graph.microsoft.com/v1.0/users/{mailUser}/sendMail";
            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                    client.BaseAddress = new Uri(sendMailEndpoint);

                    var email = new
                    {
                        message = new
                        {
                            subject = model.subject,
                            body = new
                            {
                                contentType = "HTML",
                                content = model.content
                            },
                            toRecipients = new[]
                            {
                                new
                                {
                                    emailAddress = new
                                    {
                                        address = model.recipientEmail
                                    }
                                }
                            }
                        }
                    };


                    var json = JsonSerializer.Serialize(email);
                    var contents = new StringContent(json, Encoding.UTF8, "application/json");

                    using (var clients = new HttpClient())
                    {
                        clients.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                        clients.BaseAddress = new Uri(sendMailEndpoint);

                        var responses = await clients.PostAsync(sendMailEndpoint, contents);

                        if (responses.IsSuccessStatusCode)
                        {
                            Console.WriteLine("E-mail enviado com sucesso!");
                        }
                        else
                        {
                            Console.WriteLine("Erro ao enviar o e-mail: " + responses.ReasonPhrase);
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocorreu um erro ao enviar o correio eletrônico:" + ex.Message);
            }

            return Ok("Email enviado com sucesso!");


        }
    }
}