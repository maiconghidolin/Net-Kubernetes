namespace Service
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();

            var basePath = Environment.GetEnvironmentVariable("API_PATH_BASE")?.Trim().TrimStart('/');

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
