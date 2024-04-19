using CanneTVReplay.Helpers;
using CanneTVReplay.Repositories;
using Google.Protobuf.WellKnownTypes;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.FileProviders;
using MySql.Data.MySqlClient;
using System.Data;

var builder = WebApplication.CreateBuilder(args);

const string DEV_CORS_POLICY = "dev_cors_policy";

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

if (builder.Environment.IsDevelopment())
{
    builder.Services.AddTransient(_ => new CanneCounterConnectionProvider { Connection = new MySqlConnection("Server=localhost;Database=cannetvjnmmain;Uid=root;Pwd=;") });
    builder.Services.AddTransient(_ => new CanneReplayConnectionProvider { Connection = new MySqlConnection("Server=localhost;Database=cannetvreplay;Uid=root;Pwd=;") });


    builder.Services.AddCors(options =>
    {
        options.AddPolicy(name: DEV_CORS_POLICY,
                          policy =>
                          {
                              policy.WithOrigins("http://localhost:5173");
                          });
    });

}
else
{
    var mysqlUser = Environment.GetEnvironmentVariable("MYSQL_USER");
    var mysqlPassword = Environment.GetEnvironmentVariable("MYSQL_PASSWORD");
    builder.Services.AddTransient(_ => new CanneCounterConnectionProvider { Connection = new MySqlConnection($"Server=localhost;Database=cannetvjnmmain;Uid={mysqlUser};Pwd={mysqlPassword};") });
    builder.Services.AddTransient(_ => new CanneReplayConnectionProvider { Connection = new MySqlConnection($"Server=localhost;Database=cannetvreplay;Uid={mysqlUser};Pwd={mysqlPassword};") });
}

builder.Services.AddTransient<CompetitionRepository>();
builder.Services.AddTransient<EncounterRepository>();

//  dotnet publish -c Release --no-self-contained --runtime linux-arm64


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseHttpsRedirection();
    app.UseCors(DEV_CORS_POLICY);
}
else
{
    app.UseStaticFiles(new StaticFileOptions
    {
        FileProvider = new PhysicalFileProvider(Path.Combine(builder.Environment.ContentRootPath, "videos")),
        RequestPath = "/videos"
    });

    app.UseForwardedHeaders(new ForwardedHeadersOptions
    {
        ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
    });
}



app.UseAuthorization();

app.MapControllers();

app.Run();
