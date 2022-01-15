﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Migrator;

var host = Host.CreateDefaultBuilder(args);

host.ConfigureAppConfiguration(options =>
{
    options.AddJsonFile("appsettings.Local.json");
});

host.ConfigureServices((builder, services) =>
{
    services.AddDbContext<AppDbContext>(opt =>
    {
        var conStr = builder.Configuration.GetConnectionString("BookistConnection");
        opt.UseMySql(conStr, ServerVersion.AutoDetect(conStr), o => o.MigrationsAssembly("Migrator"));
    });

});


host.Build();