using System;

namespace DZ_1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var app = builder.Build();


            app.MapGet("/", () => "Hello World!");

            app.MapGet("/about", () => "ASP.NET Core");

            //використовую Postman
            app.MapPost("/echo", async (HttpContext context) =>
            {
                using var reader = new StreamReader(context.Request.Body);
                var body = await reader.ReadToEndAsync();
                return Results.Content(body, "application/json");
            });

            app.Use(async (context, next) =>
            {
                var start = DateTime.Now;
                await next.Invoke();
                var end = DateTime.Now;
                Console.WriteLine($"Time request: {end - start}");
            }); 

            app.Run();
        }
    }
}
