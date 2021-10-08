using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;


//https://medium.com/@RobertKhou/getting-started-with-entity-framework-core-postgresql-c6fa09681624
// 지도를 블럭으로 나눈다.
// 가장 최신 블럭을 찍어준다.
// 블럭을 선택하면 해당 

namespace BlazorLeaflet.Samples
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
