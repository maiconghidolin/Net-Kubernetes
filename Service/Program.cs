using StackExchange.Redis;

namespace Service
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add Redis connection
            builder.Services.AddSingleton<IConnectionMultiplexer>(sp =>
            {
                var redisConnectionString = builder.Configuration.GetConnectionString("Redis");
                return ConnectionMultiplexer.Connect(redisConnectionString);
            });

            // Add razor pages
            builder.Services.AddRazorPages();

            var basePath = builder.Configuration["ApiPathBase"]?.Trim().TrimStart('/');

            Console.WriteLine($"pathBase: {basePath}");

            var app = builder.Build();

            if (!string.IsNullOrWhiteSpace(basePath))
                app.UsePathBase('/' + basePath);

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapRazorPages();

            app.Run();
        }
    }
}
