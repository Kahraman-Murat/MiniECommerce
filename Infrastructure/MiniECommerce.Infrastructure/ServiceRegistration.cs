﻿using Microsoft.Extensions.DependencyInjection;
using MiniECommerce.Application.Abstractions.Services;
using MiniECommerce.Application.Abstractions.Services.Configurations;
using MiniECommerce.Application.Abstractions.Storage;
using MiniECommerce.Application.Abstractions.Token;
using MiniECommerce.Infrastructure.Enums;
using MiniECommerce.Infrastructure.Services;
using MiniECommerce.Infrastructure.Services.Configurations;
using MiniECommerce.Infrastructure.Services.Storage;
using MiniECommerce.Infrastructure.Services.Storage.Azure;
using MiniECommerce.Infrastructure.Services.Storage.Local;
using MiniECommerce.Infrastructure.Services.Token;

namespace MiniECommerce.Infrastructure
{
    public static class ServiceRegistration
    {
        public static void AddInfraStructureServices(this IServiceCollection serviceCollection)
        {
            //serviceCollection.AddScoped<IFileService, FileService>();
            serviceCollection.AddScoped<IStorageService, StorageService>();
            serviceCollection.AddScoped<ITokenHandler, TokenHandler>();
            serviceCollection.AddScoped<IMailService, MailService>();
            serviceCollection.AddScoped<IApplicationService, ApplicationService>();
            serviceCollection.AddScoped<IQRCodeService, QRCodeService>();
        }

        public static void AddStorage<T>(this IServiceCollection serviceCollection) where T : Storage, IStorage
        {
            serviceCollection.AddScoped<IStorage, T>();
        }
        public static void AddStorage(this IServiceCollection serviceCollection, StorageType storageType)
        {
            //serviceCollection.AddScoped(storageType);
            switch (storageType)
            {
                case StorageType.Local:
                    serviceCollection.AddScoped<IStorage, LocalStorage>();
                    break;
                case StorageType.Azure:
                    serviceCollection.AddScoped<IStorage, AzureStorage>();
                    break;
                case StorageType.AWS:
                    break;
                default:
                    serviceCollection.AddScoped<IStorage, LocalStorage>();
                    break;
            }
        }
    }
}
