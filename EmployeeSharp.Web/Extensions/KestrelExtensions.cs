namespace EmployeeSharp.Web.Extensions
{
    public static class KestrelExtensions
    {
        public static void ConfigureKestrelServer(this IWebHostBuilder webHostBuilder)
        {
            webHostBuilder.ConfigureKestrel(serverOptions =>
            {
                var env = serverOptions.ApplicationServices.GetRequiredService<IHostEnvironment>();

                if (env.IsDevelopment())
                {
                    serverOptions.ListenAnyIP(5000);
                    serverOptions.ListenAnyIP(5001, listenOptions =>
                    {
                        listenOptions.UseHttps();
                    });
                }
                else
                {
                    serverOptions.ListenAnyIP(2041);
                }
            });
        }
    }
}