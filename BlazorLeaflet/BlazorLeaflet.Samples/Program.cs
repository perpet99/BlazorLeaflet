using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;


//https://medium.com/@RobertKhou/getting-started-with-entity-framework-core-postgresql-c6fa09681624
// ������ ������ ������.
// ���� �ֽ� ���� ����ش�.
// ���� �����ϸ� �ش� 

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
