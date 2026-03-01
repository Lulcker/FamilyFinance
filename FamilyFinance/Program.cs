using FamilyFinance.Configurators;
using FamilyFinance.Handlers;
using FamilyFinance.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder
    .ConfigureHash()
    .ConfigureAuth()
    .ConfigureCors()
    .ConfigureAutofac()
    .ConfigureDatabase()
    .ConfigureAesCrypto()
    .ConfigureDefaultUsers();

builder.Services.AddControllers();

var app = builder.Build();

app.UseCors("CorsPolicy");

app.UseExceptionHandler(error => error.Run(GlobalExceptionHandler.Handle));

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseUserInfo();

app.MapControllers();

app.MigrateDatabase();

await app.AddDefaultUsersAsync();

app.Run();