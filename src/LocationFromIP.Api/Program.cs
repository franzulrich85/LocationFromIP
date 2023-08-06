using LocationFromIP.Api.Filters;
using LocationFromIP.Api.Middlewares;
using LocationFromIP.Api.Options;
using LocationFromIP.Application;
using LocationFromIP.Infrastructure;
using LocationFromIP.Persistence;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddCors();
builder.Services.AddControllers()
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.Converters.Add(new StringEnumConverter());
                    options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                    options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
                    {
                        c.SchemaFilter<EnumSchemaFilter>();
                    });

builder.Services.AddApiVersioning(x =>
                    {
                        x.DefaultApiVersion = new ApiVersion(1, 0);
                        x.AssumeDefaultVersionWhenUnspecified = true;
                        x.ReportApiVersions = true;
                    });

builder.Services.AddVersionedApiExplorer(setup =>
                    {
                        setup.GroupNameFormat = "'v'VVV";
                        setup.SubstituteApiVersionInUrl = true;
                    });

builder.Services.ConfigureOptions<ConfigureSwaggerOptions>();

//configure project dependencies
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddPersistenceServices(builder.Configuration);


//adding healthchecks
builder.Services.AddHealthChecks();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapHealthChecks("/health");

app.UseMiddleware<ExceptionMiddleware>();

app.UseCors(x => x
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());

app.Run();
