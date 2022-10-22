using Microsoft.Extensions.DependencyInjection;
using MiniECommerce.Application.Abstractions.Storage;
using MiniECommerce.Application.Abstractions.Token;
using MiniECommerce.Infrastructure.Enums;
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
