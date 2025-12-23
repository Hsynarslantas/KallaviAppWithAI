using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace YumApp.UI.ViewComponents.AdminDashboard
{
    public class _DashboardAdviceMenuComponentPartial : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = new HttpClient();
            var seed = DateTime.Now.Ticks % 100000;

            var requestBody = new
            {
                messages = new[]
                {
                    new
                    {
                        role = "user",
                        content =
                        $"Dünya mutfakları ve yemek kültürleri hakkında AZ BİLİNEN, ŞAŞIRTICI ve GÜNLÜK hayatta pek bilinmeyen bilgiler yaz.\r\n\r\nKurallar:\r\n- 4 farklı ülkeden bahset\r\n- Ülkeler farklı kıtalardan olsun (ama kıta adlarını ASLA yazma)\r\n- Türkiye, Osmanlı veya Türk mutfağı ASLA olmasın\r\n- Her ülke için SADECE 1 kısa paragraf yaz\r\n- Bilgiler sıradan olmamalı, herkesin bildiği şeyler OLMASIN\r\n- 'En sevilen', 'ünlü', 'ikonik' gibi klişe kelimeleri KULLANMA\r\n- Wikipedia tarzı anlatım YAPMA\r\n- Akademik dil KULLANMA\r\n- İlginç, şaşırtıcı, biraz ters köşe bilgiler yaz\r\n- Liste, madde işareti, yıldız, markdown KULLANMA\r\n- Emojiyi sadece paragraf başında kullanabilirsin\r\n- Başka açıklama ekleme\r\n\r\nTam olarak şu formatta yaz:\r\n\r\n\U0001f9e0 Dünyanın dört bir yanından mutfak kültürlerine dair ilginç bilgiler\r\n\r\n[Ülke Adı]\r\n1–2 cümlelik az bilinen, şaşırtıcı bilgi.\r\n\r\n[Ülke Adı]\r\n1–2 cümlelik az bilinen, şaşırtıcı bilgi.\r\n\r\n[Ülke Adı]\r\n1–2 cümlelik az bilinen, şaşırtıcı bilgi.\r\n\r\n[Ülke Adı]\r\n1–2 cümlelik az bilinen, şaşırtıcı bilgi."
                    }
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
            var json = await response.Content.ReadAsStringAsync();

          
            string menuText = json;

            try
            {
                using var doc = JsonDocument.Parse(json);
                var root = doc.RootElement;

                if (root.TryGetProperty("result", out var result))
                    menuText = result.GetString() ?? json;
            }
            catch
            {
               
            }

            ViewBag.MenuText = menuText;
            return View();
        }
    }
}
