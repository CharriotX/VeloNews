﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Data.Sql
{
    public class Startup
    {
        public void RegisterDbContext(IServiceCollection services)
        {
            var connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=VeloNews;Integrated Security=True;";
            services.AddDbContext<WebContext>(op => op.UseSqlServer(connectionString));
        }
    }
}
