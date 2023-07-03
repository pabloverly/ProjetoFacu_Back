using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ApiTools.Helper;
using System.Globalization;
using Newtonsoft.Json.Linq;
using ApiTools.Model;

namespace ApiTools.Controllers
{
    [ApiController]
    [Route("texttospeach/send")]
    public class Texttospeech : ControllerBase
    {
        private readonly ILogger<SendEmailController> _logger;

        public Texttospeech(ILogger<SendEmailController> logger)
        {
            _logger = logger;
        }

        [HttpGet("send")]
        // [Authorize]
        public async Task<ActionResult<dynamic>> SendText(string Text, float Pitch, float Speed)
        {

            string TypeSudio = "pt-BR-Standard-B";
            string AudioDevice = "large-home-entertainment-class-device";

            ContentHeper contentHeper = new ContentHeper();
            HttpClient client = new HttpClient();
            HttpResponseMessage response = new HttpResponseMessage();
            var completo = DateTime.Now.ToString("ddMMyyyyHHmm");
            string json = "";
            string retorno = "";


            try
            {

                //var data = File.ReadAllText(dirText);  

                json = $@"{{            
                ""input"":{{
                    ""text"": ""{Text}""
                }},	
                ""voice"":{{
                    ""languageCode"": ""pt-BR"",
                    ""name"": ""{TypeSudio}"",

                }},
                ""audioConfig"":{{
                    ""audioEncoding"": ""MP3"",
                    ""effectsProfileId"":  ""{AudioDevice}"",
                     ""pitch"": {Pitch},
                     ""speakingRate"": {Speed}
                }}            

                }}";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            try
            {
                var content = contentHeper.ContentString(json);

                try
                {
                    response = await client.PostAsync("https://texttospeech2.googleapis.com/v1/text:synthesize?key=AIzaSyAgPGDPvB8Je_Q6nrLnSisuVF8SpDeomrU", content);

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);

                }
                try
                {
                    string responseContent = await response.Content.ReadAsStringAsync();
                    retorno = await response.Content.ReadAsStringAsync();
                    try
                    {
                        // JObject obj = JObject.Parse(responseContent);

                        // string audioContentBase64 = obj.GetValue("audioContent").ToString();

                        try
                        {

                            // byte[] audioData = Convert.FromBase64String(audioContentBase64);
                            // File.WriteAllBytes(dirAudio + $@"\{tbNomeArq.Text}.mp3", audioData);
                            // MessageBox.Show("Gerado com sucesso!");
                            // tbTexto.Enabled = false;
                            // pbPlay.Enabled = true;
                            // pbStop.Enabled = true;
                            // pnControl.Visible = true;
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            // return retorno;
            return retorno;
        }
    }
}