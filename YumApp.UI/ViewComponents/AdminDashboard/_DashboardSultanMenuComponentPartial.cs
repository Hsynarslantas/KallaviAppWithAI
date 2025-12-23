using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace YumApp.UI.ViewComponents.AdminDashboard
{
    public class _DashboardSultanMenuComponentPartial : ViewComponent
    {
        private readonly IMemoryCache _cache;

        public _DashboardSultanMenuComponentPartial(IMemoryCache cache)
        {
            _cache = cache;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var today = DateTime.Today;
            var osmanliDate = today.AddYears(-200);
            var cacheKey = $"osmanli-menu-{today:yyyyMMdd}";

            if (!_cache.TryGetValue(cacheKey, out string menuText))
            {
            
                menuText = await GenerateMenuFromAi(osmanliDate);

                _cache.Set(
                    cacheKey,
                    menuText,
                    today.AddDays(1).AddSeconds(-1)
                );
            }

            ViewBag.MenuText = menuText;
            return View();
        }

        private async Task<string> GenerateMenuFromAi(DateTime osmanliDate)
        {
            var client = new HttpClient();

            var prompt = $@"
            Aşağıdaki tarih için Osmanlı saray mutfağı geleneğine uygun bir günlük menü yaz.

            Kurallar:
            - Tarih: {osmanliDate:dd.MM.yyyy}
            - Dönem: 1800–1830, kış mevsimi, II. Mahmud dönemi
            - Sadece Osmanlı/Türk mutfağı
            - Yabancı veya modern yemek/ içecek ASLA ekleme
            - Markdown, yıldız (**), tire (-), madde işareti (•) kullanma
            - Sadece düz metin yaz
            - Kesin tarihsel kayıt iddiası yapma, gelenekten esinlen
            - Şerbet mutlaka KIŞA uygun, Osmanlı’da tüketilen şerbetlerden olsun (demirhindi, gül, nar, tarçınlı vb.)
            - Aşağıdaki formatın DIŞINA ASLA çıkma

            Tam olarak şu formatta yaz:

            📅 {osmanliDate:dd.MM.yyyy}
            {osmanliDate:dd.MM.yyyy} tarihinde padişahımıza başaşçı tarafından sunulanlar:

            ○ Günün Çorbası: ...
  
            ○ Günün Ana Yemeği: ...
  
            ○ Günün Tatlısı: ...
  
            ○ Günün Şerbeti: ...
            ";

            var requestBody = new
            {
                messages = new[]
                {
                    new { role = "user", content = prompt }
                },
                web_access = false
            };

            var request = new HttpRequestMessage(HttpMethod.Post, "https://chatgpt-42.p.rapidapi.com/gpt4o");
            request.Headers.Add("x-rapidapi-key", "1b01b4abbfmsh618c142c34ac024p1583c7jsn34ff990c3e13");
            request.Headers.Add("x-rapidapi-host", "chatgpt-42.p.rapidapi.com");

            request.Content = new StringContent(
                JsonSerializer.Serialize(requestBody),
                Encoding.UTF8,
                "application/json"
            );

            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();

            // En sade haliyle içeriği çekiyoruz
            using var doc = JsonDocument.Parse(json);
            if (doc.RootElement.TryGetProperty("result", out var result))
                return result.GetString() ?? "Menü hazırlanamadı.";

            return "Menü hazırlanamadı.";
        }
    }
}
