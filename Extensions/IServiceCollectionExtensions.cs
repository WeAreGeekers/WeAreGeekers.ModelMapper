using Microsoft.Extensions.DependencyInjection;
using System;
using WeAreGeekers.ModelMapper.Settings;

namespace WeAreGeekers.ModelMapper.Extensions
{

    /// <summary>
    /// Static class that contains the 'IServiceCollection' extension methods
    /// </summary>
    public static class IServiceCollectionExtensions
    {

        /// <summary>
        /// Add ModelMapper service to Service Collection
        /// </summary>
        /// <param name="services"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static IServiceCollection AddModelMapper(this IServiceCollection services, Action<ModelMapper> action)
        {
            // Init new Model Mapper Builder
            ModelMapperSettings.ModelMapper = new ModelMapper();

            // Run the action
            action(ModelMapperSettings.ModelMapper);

            // Return services
            return services;
        }

    }

}
