using AccountOwnerServer.Extensions;
using Microsoft.AspNetCore.HttpOverrides;
using NLog;

var builder = WebApplication.CreateBuilder(args);
LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));
// Add services to the container.

builder.Services.ConfigureCors();
builder.Services.ConfigureIISIntegration();
builder.Services.ConfigureLoggerService();
builder.Services.ConfigureRepositoryWrapper();

builder.Services.AddControllers(config =>
{
    config.RespectBrowserAcceptHeader = true;
    config.ReturnHttpNotAcceptable = true;
}).AddXmlDataContractSerializerFormatters()
.AddNewtonsoftJson();

builder.Services.ConfigureSqlContext(builder.Configuration);
builder.Services.AddAutoMapper(typeof(Program));

var app = builder.Build();

// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseHsts();
}

//void Configure(IApplicationBuilder app)
//{
//    app.UseCors(options => options.WithOrigins("http://localhost:4200").WithHeaders("X-Pagination"));
//    app.UseRouting();
//    app.UseEndpoints(endpoints =>
//    {
//        endpoints.MapControllers();
//    });
//}

app.UseCors("CorsPolicy");

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.All
});

// app.UseCors() it is called above the app.UseAuthorization() as Microsoft suggest to be a best practice

app.UseAuthorization();

app.MapControllers();

app.Run();
