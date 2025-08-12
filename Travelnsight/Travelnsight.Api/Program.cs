using Travelnsight.Api.Extensions;
using Travelnsight.Application.Interfaces;
using Travelnsight.Application.UsesCases;
using Travelnsight.Infraestructure.Configuration;
using Travelnsight.Infraestructure.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.Configure<AzureImageModeratorOptions>(options => builder.Configuration.GetSection("Azure:ImageModerator").Bind(options));
builder.Services.Configure<AzureAIInferenceOptions>(options => builder.Configuration.GetSection("Azure:Inference").Bind(options));
builder.Services.Configure<AzureCustomVisionOptions>(options => builder.Configuration.GetSection("Azure:CustomVision").Bind(options));

builder.Services.AddSingleton<IAIInferenceService, AIInferenceService>();
builder.Services.AddSingleton<IImageContentModerator, ImageContentModerator>();
builder.Services.AddSingleton<IVisionImageAnalysis, VisionImageAnalysis>();
builder.Services.AddTransient<IImageAnalysisUseCase, ImageAnalysisUseCase>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

EndpointExtensions.MapVision(app);

app.Run();
