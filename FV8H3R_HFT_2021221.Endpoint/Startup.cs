using FV8H3R_HFT_2021221.Data;
using FV8H3R_HFT_2021221.Endpoint.Services;
using FV8H3R_HFT_2021221.Logic;
using FV8H3R_HFT_2021221.Models;
using FV8H3R_HFT_2021221.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FV8H3R_HFT_2021221.Endpoint
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve);

            services.AddTransient<UserLogic>();
            services.AddTransient<MatchLogic, MatchLogic>();
            services.AddTransient<MessageLogic, MessageLogic>();

            services.AddTransient<StatsLogic, StatsLogic>();

            services.AddTransient<IRepository<User>, UserRepository>();
            services.AddTransient<IRepository<Match>, MatchRepository>();
            services.AddTransient<IRepository<Message>, MessageRepository>();

            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IMatchRepository, MatchRepository>();
            services.AddTransient<IMessageRepository, MessageRepository>();

            services.AddTransient<DbContext, TinderDbContext>();
            services.AddTransient<TinderDbContext, TinderDbContext>();

            services.AddSignalR();
            services.AddTransient<UserLogic>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseCors(x => x
            .AllowCredentials()
            .AllowAnyMethod()
            .AllowCredentials()
            .WithOrigins("http://localhost:48623"));

            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<SignalRHub>("/hub");
            });
        }
    }
}
