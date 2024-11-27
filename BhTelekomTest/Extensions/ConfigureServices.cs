using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Firebase.Auth.Providers;
using Firebase.Auth.Repository;
using Firebase.Auth;
using Firebase.Database;
using BhTelekomTest.Interfaces;
using BhTelekomTest.Services;
using BhTelekomTest.ViewModel.Authentication;
using BhTelekomTest.View.Authentication;
using BhTelekomTest.ViewModel.Products;
using BhTelekomTest.View.Products;
using Microsoft.Extensions.Configuration;

namespace BhTelekomTest.Extensions
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddAppServices(this IServiceCollection services, IConfiguration configuration)
        {
            var firebaseConfig = configuration.GetSection("Firebase");
            var apiKey = firebaseConfig["ApiKey"];
            var authDomain = firebaseConfig["AuthDomain"];
            var databaseUrl = firebaseConfig["DatabaseUrl"];

            services.AddSingleton(new FirebaseAuthClient(new FirebaseAuthConfig()
            {
                ApiKey = apiKey,
                AuthDomain = authDomain,
                Providers = new FirebaseAuthProvider[]
                {
                    new EmailProvider()
                },
                UserRepository = new FileUserRepository("BhTelekomTest")
            }));

            services.AddScoped<FirebaseClient>(provider => new FirebaseClient(databaseUrl));
            services.AddScoped<IProductService, ProductService>();

            services.AddSingleton<SignIn>();
            services.AddSingleton<SignInViewModel>();
            services.AddSingleton<SignUp>();
            services.AddSingleton<SignUpViewModel>();

            services.AddScoped<ProductsViewModel>();
            services.AddScoped<ProductsPage>();
            services.AddScoped<CreateEditProduct>();
            services.AddScoped<CreateEditProductViewModel>();

            return services;
        }
    }
}
