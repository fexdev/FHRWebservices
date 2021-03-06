﻿using ChuckNorrisService.Models;
using ChuckNorrisService.Providers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ChuckNorrisService.Startups
{
    public class StartupExercise4
    {
        public void ConfigureServices(IServiceCollection services)
        {
            // Dies fügt die von der Routing-Middleware benötigten
            // Dienste zum Dependency Injection Container hinzu.
            services.AddRouting();
        }

        public void Configure(IApplicationBuilder app)
        {
            FileSystemJokeProvider jokeProvider = new FileSystemJokeProvider();

            RouteBuilder routes = new RouteBuilder(app);

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {

                endpoints.MapGet("api/jokes/random", async context =>
                            {
                                context.Response.StatusCode = (int)HttpStatusCode.OK;
                                context.Response.ContentType = "application/json";
                                await context.Response.WriteAsync(await GetSerializedJoke(jokeProvider), Encoding.UTF8);
                            });

                endpoints.MapGet("{*path}", context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return context.Response.WriteAsync("Well, IT'S YOUR FAULT!");
                });
            });


        }

        private async Task<string> GetSerializedJoke(IJokeProvider provider)
        {
            return JsonSerializer.Serialize(await provider.GetRandomJokeAsync());
        }
    }
}
