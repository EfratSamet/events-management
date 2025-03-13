using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Repository.Entity;
using Repository.Repositories;
using Service.Dtos;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Service.Services
{
    public static class ExtensionService
    {
        public static IServiceCollection AddServiceExtension(this IServiceCollection services)
        {
            services.AddRepository();
            services.AddScoped<IEventService, EventService>();
            services.AddScoped<IService<GroupDto>, GroupService>();
            services.AddScoped<IService<GuestDto>, GuestService>();
            services.AddScoped<IService<GuestInEventDto>, GuestInEventService>();
            services.AddScoped<IService<OrganizerDto>, OrganizerService>();
            services.AddScoped<IService<PhotosFromEventDto>, PhotosFromEventService>();
            services.AddScoped<IService<SeatingDto>, SeatingService>();
            services.AddScoped<IService<SubGuestDto>, SubGuestService>();
            services.AddAutoMapper(typeof(MyMapper));
            return services;
        }
    }
}
