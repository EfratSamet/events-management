using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Repository.Entity;
using Repository.Interfaces;
using Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public static class ExtensionRepository
    {
        public static IServiceCollection AddRepository(this IServiceCollection services)
        {
            services.AddRepository();
            services.AddScoped<IRepository<Event>, EventRepository>();
            services.AddScoped<IRepository<Group>, GroupRepository>();
            services.AddScoped<IRepository<Guest>, GuestRepository>();
            services.AddScoped<IRepository<GuestInEvent>, GuestInEventRepository>();
            services.AddScoped<IRepository<Organizer>, OrganizerRepository>();
            services.AddScoped<IRepository<PhotosFromEvent>, PhotosFromEventRepository>();
            services.AddScoped<IRepository<Seating>, SeatingRepository>();
            services.AddScoped<IRepository<SubGuest>, SubGuestRepository>();
            return services;
        }
    }
}
