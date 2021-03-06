using Nitro.Channel.Api.StartupExtensions;

bool? isRunningInContainer = null;

var IsRunningInContainer = isRunningInContainer ??=
    bool.TryParse(Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER"),
        out var inContainer) && inContainer;

var builder = WebApplication.CreateBuilder(args);

var IsDevelopment = builder.Environment.IsDevelopment();

builder.Services
    .AddLogging()
    .AddTransient<IConfiguration>(p => builder.Configuration)
    .AddMicroservices(IsRunningInContainer)
    .AddGraphQLEnvironment(IsDevelopment, IsDevelopment);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();
//app.UseAuthorization();
app.MapControllers();

app.UseWebSockets();
app.UseGraphQLEnvironment();

app.Run();