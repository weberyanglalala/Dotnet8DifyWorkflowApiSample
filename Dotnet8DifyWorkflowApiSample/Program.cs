using Dotnet8DifyWorkflowApiSample.Middlewares;
using Dotnet8DifyWorkflowApiSample.Services.DifyWorkflow;
using Dotnet8DifyWorkflowApiSample.Services.OpenAI;
using OpenAI.Chat;

namespace Dotnet8DifyWorkflowApiSample;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        
        var configuration = builder.Configuration;

        // Add services to the container.
        builder.Services.AddControllersWithViews();
        builder.Services.AddHttpClient();
        builder.Services.AddScoped<DifyCreateProductService>();
        builder.Services.AddScoped<OpenAIService>();
        builder.Services.AddSingleton<ChatClient>(new ChatClient("gpt-4o-mini", configuration["OPENAI_API_KEY"]));

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseMiddleware<ResultErrorHandlingMiddleware>();
        app.UseStaticFiles();
        app.UseRouting();
        app.UseAuthorization();
        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.Run();
    }
}