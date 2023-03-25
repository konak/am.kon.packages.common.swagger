using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace am.kon.packages.common.swagger
{
    /// <summary>
    /// Class used to configure Swagger
    /// </summary>
	public class SwaggerConfigBuilder
	{
        private IServiceCollection _services;

        /// <summary>
        /// Security scheme used for authorization.
        /// </summary>
        public string Scheme { get; set; }

        /// <summary>
        /// Description of scheme usage and configuration appearing in Swagger UI.
        /// </summary>
        public string SchemeUsageDescription { get; set; }

        /// <summary>
        /// Property indicating whether to configure Authorization in Swagger or no.
        /// </summary>
        public bool ConfigAuthorization { get; set; }

        public SwaggerConfigBuilder(IServiceCollection services)
        {
            _services = services;
            Scheme = SecuritySchemes.Bearer;
            SchemeUsageDescription = InternalConstants.BearerSchemeUsageDescription;
            ConfigAuthorization = true;
        }

        /// <summary>
        /// Method setting Scheme property of the builder
        /// </summary>
        /// <param name="scheme">Scheme used for authorization</param>
        /// <returns>Innstance of the builder</returns>
        public SwaggerConfigBuilder SetScheme(string scheme)
        {
            Scheme = scheme;

            return this;
        }

        /// <summary>
        /// Set SchemeUsageDescription property of the builder
        /// </summary>
        /// <param name="descriptionn">Description of scheme usage and configuration appearing in Swagger UI</param>
        /// <returns>Instance of <see cref="SwaggerConfigBuilder"/></returns>
        public SwaggerConfigBuilder SetSchemeUsageDescription(string descriptionn)
        {
            SchemeUsageDescription = descriptionn;

            return this;
        }

        /// <summary>
        /// Set property indicating whether to configure Authorization in Swagger or no.
        /// </summary>
        /// <param name="configAuthorization">True if authorization should be configured or nno.</param>
        /// <returns>Instance of <see cref="SwaggerConfigBuilder"/></returns>
        public SwaggerConfigBuilder SetConfigAuthorization(bool configAuthorization)
        {
            ConfigAuthorization = configAuthorization;

            return this;
        }

        /// <summary>
        /// Initiate final configuration process of the Swagger based on properties of the builder
        /// </summary>
        /// <returns>Instance of <see cref="IServiceCollection"/></returns>
        public IServiceCollection Build()
        {
            _services.AddSwaggerGen(config =>
            {
                if (ConfigAuthorization)
                {
                    config.AddSecurityDefinition(Scheme, new OpenApiSecurityScheme
                    {
                        Description = SchemeUsageDescription,
                        Name = InternalConstants.AuthorizationHeadeerName,
                        In = ParameterLocation.Header,
                        Type = SecuritySchemeType.ApiKey,
                        Scheme = Scheme
                    });

                    config.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Name = Scheme,
                                In = ParameterLocation.Header,
                                Reference = new OpenApiReference
                                {
                                    Id = Scheme,
                                    Type = ReferenceType.SecurityScheme
                                }
                            },
                            new List<string>()
                        }
                    });
                }
            });

            return _services;
        }
    }
}

