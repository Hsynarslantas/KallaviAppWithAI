using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using YumApp.UI.Dtos.MessageDtos;

namespace YumApp.UI.Controllers
{
    public class ContactController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ContactController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpPost]
        public async Task<IActionResult> SendMessage(CreateMessageDto createMessageDto)
        {
            var client2=new HttpClient();
            var apiKey = "xxxxxxxxxxxxxx";
            client2.DefaultRequestHeaders.Authorization=new AuthenticationHeaderValue("bearer",apiKey);
            try
            {
                var translateRequestBody = new
                {
                    inputs = createMessageDto.MessageDetails
                };
                var translateJson=System.Text.Json.JsonSerializer.Serialize(translateRequestBody);
                var translateContent=new StringContent(translateJson,Encoding.UTF8,"application/json");

                var translateResponse = await client2.PostAsync("https://api-inference.huggingface.com/models/helsinki-NLP/opus-mt-tr-en",translateContent);
                var translateResponseString=await translateResponse.Content.ReadAsStringAsync();

                string englishText = createMessageDto.MessageDetails;
                if (translateResponseString.TrimStart().StartsWith("["))
                {
                    var translateDoc=JsonDocument.Parse(translateResponseString);
                    englishText = translateDoc.RootElement[0].GetProperty("translation_text").GetString();
                    
                }
                var toxicRequestBody = new
                {
                    inputs = englishText
                };
                var toxicJson= System.Text.Json.JsonSerializer.Serialize(toxicRequestBody);
                var toxicContent=new StringContent(toxicJson,Encoding.UTF8,"application/json");
                var toxicResponse = await client2.PostAsync("https://api-inference.huggingface.com/models/unitary/toxic-bert", toxicContent);
                var toxicResponseString=await toxicResponse.Content.ReadAsStringAsync();
                if(toxicResponseString.TrimStart().StartsWith("["))
                {
                    var toxicDoc=JsonDocument.Parse(toxicResponseString);
                    foreach(var item in toxicDoc.RootElement[0].EnumerateArray())
                    {
                        string label=item.GetProperty("label").GetString();
                        double score=item.GetProperty("score").GetDouble();
                        if(score > 0.25)
                        {
                            createMessageDto.Status = "🤬 Toksik Mesaj";
                            break;
                        }
                    }
                }
                if (string.IsNullOrEmpty(createMessageDto.Status))
                {
                    createMessageDto.Status = "Mesaj Alındı";
                }
            }
            catch(Exception ex) 
            {
                createMessageDto.Status = "Onay Bekleniyor";
            }


            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(createMessageDto);
            StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync("https://localhost:7201/api/Message", content);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index","Default");
            }
            return View();
        }
    }
}
