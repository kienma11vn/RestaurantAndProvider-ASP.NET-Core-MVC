using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RestaurantAndProvider.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace RestaurantAndProvider.Data
{
    public class RestaurantAndProviderContext : IdentityDbContext
    {
        public RestaurantAndProviderContext (DbContextOptions<RestaurantAndProviderContext> options)
            : base(options)
        {
        }

        public DbSet<RestaurantAndProvider.Models.Provider> Provider { get; set; } = default!;
        public DbSet<RestaurantAndProvider.Models.Restaurant> Restaurant { get; set; } = default!;
    }
}
