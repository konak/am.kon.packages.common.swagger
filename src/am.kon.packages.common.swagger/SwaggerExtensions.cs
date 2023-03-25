using System;
using Microsoft.Extensions.DependencyInjection;

namespace am.kon.packages.common.swagger
{
    /// <summary>
    /// Extensionn methods used to configure swagger
    /// </summary>
    public static class SwaggerExtensions
	{
        /// <summary>
        /// Extension method for instance of <see cref="IServiceCollection"/> initiating configuration builder for swagger
        /// </summary>
        /// <param name="services">Instance of <see cref="IServiceCollection"/> to bind method to.</param>
        /// <returns>instance of <see cref="SwaggerConfigBuilder"/> to start Swagger configuration</returns>
        public static SwaggerConfigBuilder StartBuilder(this IServiceCollection services)
        {
            return new SwaggerConfigBuilder(services);
        }
    }
}

