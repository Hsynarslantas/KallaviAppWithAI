using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace YumApp.UI.Controllers
{
    public class AdminAdviceDishesController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(string ingredients)
        {
            if (string.IsNullOrWhiteSpace(ingredients))
            {
                ViewBag.RecipeResult = "Lütfen malzemeleri yaz (örnek: havuç, kuzu eti, hardal, bal).";
                return View();
            }

            var prompt = $@"
                Bir profesyonel şef gibi düşün ve aşağıda verilen MALZEMELERLE yapılabilecek en mantıklı yemeği oluştur.

                Kurallar:
                - Sadece verilen malzemeleri kullan
                - Olmayan malzeme EKLEME
                - Gerekirse sadece tuz, karabiber ve su ekleyebilirsin
                - Yemeği gerçekçi ve uygulanabilir yap
                - Uydurma veya fantezi tarif yazma
                - Açık, sade ve net anlat
                - Markdown, yıldız (**), tire (-), madde işareti (•) kullanma
                - Aşağıdaki formatın DIŞINA ASLA çıkma

                Malzemeler:
                {ingredients}

                Format:
                Yemek Adı:
                ...

                Kısa Açıklama:
                ...

                Hazırlanışı:
                1.
                2.
                3.

                Pişirme Önerisi:
                ...

                Servis Önerisi:
                ...
                ";

            var requestBody = new
            {
                messages = new[]
                {
                    new { role = "user", content = prompt }
                },
                web_access = false
            };

            using var client = new HttpClient();
            using var request = new HttpRequestMessage(HttpMethod.Post, "https://chatgpt-42.p.rapidapi.com/gpt4o");

            request.Headers.Add("x-rapidapi-key", "xxxxxxxx");
            request.Headers.Add("x-rapidapi-host", "chatgpt-42.p.rapidapi.com");

            request.Content = new StringContent(
                JsonSerializer.Serialize(requestBody),
                Encoding.UTF8,
                "application/json"
            );

            try
            {
                using var response = await client.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();

                string resultText = json;

                // RapidAPI response içinden "result" çek
                using var doc = JsonDocument.Parse(json);
                var root = doc.RootElement;

                if (root.TryGetProperty("result", out var result))
                    resultText = result.GetString() ?? json;

                ViewBag.RecipeResult = resultText;
                ViewBag.Ingredients = ingredients;
            }
            catch
            {
                ViewBag.RecipeResult = "Tarif oluşturulurken bir hata oluştu. Lütfen tekrar dene.";
                ViewBag.Ingredients = ingredients;
            }

            return View();
        }
    }
}
