using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace YumApp.UI.Controllers
{
    public class AIController : Controller
    {
        private readonly string apiKey = "xxxxxxxx";

        private readonly string apiUrl =
            "https://generativelanguage.googleapis.com/v1beta/models/gemini-1.5-flash:generateContent?key=";

        [HttpGet]
        public IActionResult CreateFoodRecipe()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateFoodRecipe(string prompt)
        {
            if (string.IsNullOrWhiteSpace(prompt))
            {
                ViewBag.Answer = "Şefim, bana bir tarif isteği yazmalısın.";
                return View();
            }

            string scenario = $@"
                Sen yalnızca TÜRK MUTFAĞI tarifleri veren,
                samimi, esprili ve usta bir Yapay Zeka Şefisin.

                Kurallar:
                - Kullanıcıya 'Şefim' veya 'Ustam' diye hitap et.
                - Sadece Türk mutfağı öner.
                - Tarifi şu formatta ver:
                   1) Malzemeler
                   2) Hazırlanışı
                   3) Şefin Notu
                - Birimler tipik Türk usulü olmalı: bir tutam, bir bardak, bir kaşık vb.
                - Uluslararası mutfak YASAK.

                KULLANICININ İSTEĞİ:
{prompt}";

            var bodyObj = new
            {
                contents = new[]
                {
                    new {
                        role = "user",
                        parts = new[] { new { text = scenario } }
                    }
                }
            };

            string json = JsonConvert.SerializeObject(bodyObj);

            using var client = new HttpClient();

            var response = await client.PostAsync(
                apiUrl + apiKey,
                new StringContent(json, Encoding.UTF8, "application/json")
            );

            string rawResponse = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                ViewBag.Answer = $"Şefim hata oluştu: {response.StatusCode}";
                return View();
            }

            dynamic parsed = JsonConvert.DeserializeObject(rawResponse);

            string answer =
                parsed?.candidates?[0]?.content?.parts?[0]?.text ??
                "Şefim sistem cevap vermedi.";

            ViewBag.Answer = answer;

            return View();
        }
    }
}
