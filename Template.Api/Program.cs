using Template.Api.Extensions;
using NLog;
using Microsoft.AspNetCore.HttpOverrides;

var builder = WebApplication.CreateBuilder(args);

LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));

// Add services to the container.
builder.Services.ConfigureCors();
builder.Services.ConfigureIISIntegration();
builder.Services.ConfigureLoggerService();

// this method registers only the controllers in IServiceCollection and not
// Views or Pages because they are not required in the Web API project
// which we are building. 
builder.Services.AddControllers();

// With the Build method, we are creating the app variable of the type
// WebApplication. This class (WebApplication) is very important since it
// implements multiple interfaces like IHost that we can use to start and
// stop the host, IApplicationBuilder that we use to build the
// middleware pipeline (as you could’ve seen from our previous custom code), 
// and IEndpointRouteBuilder used to add endpoints in our app.
var app = builder.Build();

if (app.Environment.IsDevelopment()) app.UseDeveloperExceptionPage();
else
    // will add middleware for using HSTS, which adds the
    // Strict-Transport-Security header.
    app.UseHsts();

// The UseHttpRedirection method is used to add the middleware for the
// redirection from HTTP to HTTPS. Also, we can see the UseAuthorization
// method that adds the authorization middleware to the specified
// IApplicationBuilder to enable authorization capabilities.
app.UseHttpsRedirection();

// enables using static files for the request. If
// we don’t set a path to the static files directory, it will use a wwwroot
// folder in our project by default.
app.UseStaticFiles();

// will forward proxy headers to the
// current request. This will help us during application deployment. Pay
// attention that we require Microsoft.AspNetCore.HttpOverrides
// using directive to introduce the ForwardedHeaders enumeration
app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.All
});

app.UseCors("CorsPolicy");

app.UseAuthorization();

// we can see the MapControllers method that adds the endpoints
// from controller actions to the IEndpointRouteBuilder and the Run
// method that runs the application and block the calling thread until the
// host shutdown.
app.MapControllers();

app.Run();
