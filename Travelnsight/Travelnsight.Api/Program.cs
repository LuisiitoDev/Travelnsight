using Microsoft.AspNetCore.Http.Features;
using Scalar.AspNetCore;
using Travelnsight.Api.Configuration;
using Travelnsight.Api.Extensions;
using Travelnsight.Application.Interfaces;
using Travelnsight.Application.UsesCases;
using Travelnsight.Infraestructure.Configuration;
using Travelnsight.Infraestructure.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi(options =>
{
    options.AddDocumentTransformer<OpenApiDocumentTransformer>();
});


builder.Services.Configure<AzureImageModeratorOptions>(options => builder.Configuration.GetSection("Azure:ImageModerator").Bind(options));
builder.Services.Configure<AzureAIInferenceOptions>(options => builder.Configuration.GetSection("Azure:Inference").Bind(options));
builder.Services.Configure<AzureCustomVisionOptions>(options => builder.Configuration.GetSection("Azure:CustomVision").Bind(options));

builder.Services.AddSingleton<IAIInferenceService, AIInferenceService>();
builder.Services.AddSingleton<IImageContentModerator, ImageContentModerator>();
builder.Services.AddSingleton<IVisionImageAnalysis, VisionImageAnalysis>();
builder.Services.AddTransient<IImageAnalysisUseCase, ImageAnalysisUseCase>();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyHeader()
               .AllowAnyMethod()
               .AllowAnyOrigin();
    });
});

builder.Services.Configure<FormOptions>(options =>
{
    options.MultipartBodyLengthLimit = 10 * 1024 * 1024; // 10 MB
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.MapScalarApiReference(options =>
    {
        options.WithTitle("LuisiitoDev - DevOps Assistant API")
               .WithTheme(ScalarTheme.Kepler)
               .WithDefaultHttpClient(ScalarTarget.CSharp, ScalarClient.HttpClient)
               .WithModels(true)
               .WithDarkMode(true);
    });
}

app.MapVision();

app.Run();
