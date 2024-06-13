using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Euro24Tracker.Data;
using System.Text.Json.Serialization;
using Euro24Tracker.Controllers;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Euro24Tracker
{

    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<Euro24TrackerContext>(options =>
                options.UseSqlite(builder.Configuration.GetConnectionString("Euro24TrackerContext") ?? throw new InvalidOperationException("Connection string 'Euro24TrackerContext' not found.")));

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            // API GENERATION SWAGGER (MIT NUGGET PACKAGE)

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
                options.JsonSerializerOptions.WriteIndented = true;
            });

            builder.Services.AddSingleton<StartupUrlPrinter>();

            // END API


            
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            // API

            app.UseSwagger();
            app.UseSwaggerUI();

            // END EPI

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            

            string relativePath = "EURO2024App/port.txt";
            string port = app.Configuration.AsEnumerable().ToArray()[94].ToString().Split(',')[1].Trim(new char[] { ' ', ']' });
            string currentDirectory = Environment.CurrentDirectory;
            string parentDirectory = Directory.GetParent(currentDirectory).FullName;


            string fullPath = Path.Combine(parentDirectory, relativePath);

            

            File.WriteAllText(fullPath, port);

       


            //var baseUrl = string.Format("{ 0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content(“~”));

            //var urlPrinter = app.Services.GetRequiredService<StartupUrlPrinter>();
            //urlPrinter.PrintUrls();




            app.Run();

        }
    }

    
}
