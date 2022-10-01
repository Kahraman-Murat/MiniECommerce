using Microsoft.Extensions.DependencyInjection;
using MiniECommerce.Application.Abstractions.Storage;
using MiniECommerce.Application.Services;
using MiniECommerce.Infrastructure.Enums;
using MiniECommerce.Infrastructure.Services;
using MiniECommerce.Infrastructure.Services.Storage;
using MiniECommerce.Infrastructure.Services.Storage.Local;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniECommerce.Infrastructure
{
    public static class ServiceRegistration
    {
        public static void AddInfraStructureServices(this IServiceCollection serviceCollection)
        {
            //serviceCollection.AddScoped<IFileService, FileService>();
            serviceCollection.AddScoped<IStorageService, StorageService>();
        }

        public static void AddStorage<T>(this IServiceCollection serviceCollection) where T : class, IStorage
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
