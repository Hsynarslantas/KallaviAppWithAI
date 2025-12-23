using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using YumApp.UI.Dtos.MessageDtos;
using YumApp.UI.Models;

namespace YumApp.UI.Controllers
{
    public class AdminMessageController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AdminMessageController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7201/api/Message");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultMessageDto>>(jsonData);
                return View(values);
            }
            return View();
        }
        public async Task<IActionResult> DeleteMessage(int id)
        {
            var client = _httpClientFactory.CreateClient();
            await client.DeleteAsync($"https://localhost:7201/api/Message?id={id}");
            return RedirectToAction("Index");

        }
        [HttpGet]
        public async Task<IActionResult> MessageDetail(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"https://localhost:7201/api/Message/GetMessage?id={id}");
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var value = JsonConvert.DeserializeObject<GetByIdMessageDto>(jsonData);
            return Json(value);
        }
        [HttpGet]
        public async Task<IActionResult> AnswerWithAI(int id)
        {

            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"https://localhost:7201/api/Message/GetMessage?id={id}");
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var mesaj = JsonConvert.DeserializeObject<GetByIdMessageDto>(jsonData);


            var client2 = new HttpClient();
            client2.DefaultRequestHeaders.Add("x-rapidapi-key", "1b01b4abbfmsh618c142c34ac024p1583c7jsn34ff990c3e13");
            client2.DefaultRequestHeaders.Add("x-rapidapi-host", "chatgpt-42.p.rapidapi.com");

            var requestData = new
            {
                messages = new[]
                {
            new
            {
                role = "system",
                content = "Sen bir restoran müşteri mesajlarını cevaplayan asistansın. Türkçe, samimi, saygılı ve çözüm odaklı cevap ver. Yapay zeka gibi konuşma. Müşterilerimiz ne yazarsa yazsın sen onlara içten ve samimi bir geri dönüş yorumu yazmanı istiyorum.Asla yapay zeka gibi konuşma bir asistan nasıl davranıyorsa öyle davranmanı istiyorum senden.Müşteri kötü bir yorum yapmış olsa bile yapıcı şekilde güzellemeler ve temennilerle beraber geri dönüş yapmanı istiyorum."
            },
            new
            {
                role = "user",
                content = mesaj?.MessageDetails ?? ""
            }
        },
                web_access = false
            };

            // 3) API'ye gönder
            var jsonContent = JsonConvert.SerializeObject(requestData);
            var content = new StringContent(jsonContent)
            {
                Headers = { ContentType = new MediaTypeHeaderValue("application/json") }
            };

            var response = await client2.PostAsync("https://chatgpt-42.p.rapidapi.com/gpt4o", content);

            // 4) Cevabı işle
            if (response.IsSuccessStatusCode)
            {
                var aiJson = await response.Content.ReadAsStringAsync();
                var aiData = JsonConvert.DeserializeObject<AnswerWithAIViewModel>(aiJson);

                ViewBag.AIAnswer = aiData?.result ?? "Cevap alınamadı.";
            }
            else
            {
                ViewBag.AIAnswer = "Bir hata oluştu: " + response.StatusCode;
            }

            return View(mesaj);
        }

    }
}
