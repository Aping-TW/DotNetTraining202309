using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class TwseController : Controller
    {
        public async Task<IActionResult> Index()
        {
            try
            {
                var address = "https://openapi.twse.com.tw/v1/exchangeReport/STOCK_DAY_ALL";
                var httpClient = new HttpClient();

                // Async 平行化 (非同步) -->用於處理多線程資料

                // Thread 執行序--> 多個Task
                var task = httpClient.GetAsync(address);

                // 要等資料回傳回來
                var response = await task;

                if (response.IsSuccessStatusCode)
                {
                    var resultJson = await response.Content.ReadAsStringAsync();
                    // Json轉型
                    //IEnumberable -->可列舉陣列
                    var result = JsonSerializer.Deserialize<IEnumerable<twseModels>>(resultJson);
                }
            }
            catch (Exception)
            {

                throw;
            }
            
            
            

            return View();
        }
    }
}
