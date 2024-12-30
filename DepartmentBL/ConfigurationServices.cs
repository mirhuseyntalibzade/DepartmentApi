using BusinessLayer.Services.Abstractions;
using BusinessLayer.Services.Implementations;
using DepartmentBL.ExternalServices.Implementations;
using DepartmentBL.ExternalServices.Interfaces;
using DepartmentBL.Services.Abstractions;
using DepartmentBL.Services.Concretes;
using DepartmentDAL.Repositories.Abstractions;
using DepartmentDAL.Repositories.Implementations;
using DepartmentDAL.Repositories.Iplementations;
using Microsoft.Extensions.DependencyInjection;

namespace DepartmentBL
{
    public static class ConfigurationServices
    {
        public static void AddBusinessServices(this IServiceCollection services)
        {
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IDepartmentService, DepartmentService>();
        }
    }
}
