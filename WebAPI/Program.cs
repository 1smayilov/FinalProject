using Autofac;
using Autofac.Extensions.DependencyInjection;
using Business.DependencyResolvers.Autofac;
using Core.DependencyResolvers;
using Core.Extensions;
using Core.Utilities.IoC;
using Core.Utilities.Security.Encryption;
using Core.Utilities.Security.JWT;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();


//Autofac's Configurations

// .Net - ə deyirik ki sənin IOC konteynerin var ama ondan istifadə etmə, fabrika olaraq Autafac istifadə et 
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory()); // 1.
builder.Host.ConfigureContainer<ContainerBuilder>(builder =>
{
    builder.RegisterModule(new AutofacBusinessModule()); // Nədən istifadə edəcəyimizi göstəririk
});
//End.

//JWT's Configurations
var tokenOptions = builder.Configuration.GetSection("TokenOptions").Get<TokenOptions>(); // 2.

// Tokenlərin necə yaradılacağını və doğrulanacağını təyin edən konfiqurasiya parametrlərini
// (məsələn, issuer, audience, security key) əldə etmək üçün istifadə olunur.

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidIssuer = tokenOptions.Issuer,
            ValidAudience = tokenOptions.Audience,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = SecurityKeyHelper.CreateSecurityKey(tokenOptions.SecurityKey)
        };
    });
// Biz Asp.Net Api - ya deyirik ki burada JWT istifadə olunacaq

// Təsəvvür edin ki, bir məkanın girişində bir nəzarətçi var. Hər dəfə bir şəxs daxil olmaq istəyəndə,
// nəzarətçi bu şəxsin daxil olmaq hüququ olduğunu təsdiq edən bir kartı (token) yoxlayır. Bu kod parçası,
// tətbiqdə bu "kartların" (tokenlərin) necə yaradılacağını və necə doğrulanacağını müəyyən edir.
// Bu, nəzarətçinin düzgün və etibarlı kartları (tokenləri) qəbul etməsini təmin edir.

//End.



//Dependency Injection
builder.Services.AddDependencyResolvers(new ICoreModule[] // 3.
        {
            new CoreModule()
        });


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//CORS DI
builder.Services.AddCors();

var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//Middleware
//app.ConfigureCustomExceptionMiddleware();

//CORS Request!
app.UseCors(builder => builder.WithOrigins("http://localhost:4200").AllowAnyHeader());

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseStaticFiles();

app.Run();

