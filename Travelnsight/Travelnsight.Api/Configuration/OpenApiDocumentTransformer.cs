using Microsoft.AspNetCore.OpenApi;
using Microsoft.OpenApi.Models;

namespace Travelnsight.Api.Configuration;

public class OpenApiDocumentTransformer : IOpenApiDocumentTransformer
{
    public Task TransformAsync(OpenApiDocument document, OpenApiDocumentTransformerContext context, CancellationToken cancellationToken)
    {
        document.Info = new OpenApiInfo
        {
            Title = "DevOps Assistant API",
            Version = "v1",
            Description = $"""
                    API for LuisiitoDev DevOps Assistant
                    
                    Current Date and Time (UTC - YYYY-MM-DD HH:MM:SS formatted): 2025-07-06 14:14:34
                    Current User's Login: LuisiitoDev
                    
                    This API provides DevOps automation capabilities including:
                    • Git branch management
                    • Repository operations  
                    • Deployment automation
                    • AI-powered DevOps assistance powered by Semantic Kernel
                    
                    Authorization: Only LuisiitoDev can use this API
                    
                    All operations are logged with timestamps for audit purposes.
                    You can test all endpoints using the playground interface.
                    """,
            Contact = new OpenApiContact
            {
                Name = "LuisiitoDev",
                Email = "luisiitodev@example.com",
                Url = new Uri("https://github.com/LuisiitoDev")
            },
            License = new OpenApiLicense
            {
                Name = "MIT License",
                Url = new Uri("https://opensource.org/licenses/MIT")
            }
        };

        document.Servers = new List<OpenApiServer>
            {
                new()
                {
                    Url = "https://localhost:7062",
                    Description = "Development Server (LuisiitoDev)"
                },
                new()
                {
                    Url = "http://localhost:5001",
                    Description = "Development Server HTTP (LuisiitoDev)"
                }
            };

        // Add security scheme
        document.Components ??= new OpenApiComponents();
        document.Components.SecuritySchemes.Add("LuisiitoDev-Auth", new OpenApiSecurityScheme
        {
            Type = SecuritySchemeType.ApiKey,
            In = ParameterLocation.Header,
            Name = "X-User-Id",
            Description = "User identifier - must be 'LuisiitoDev'"
        });

        // Add example responses
        document.Components.Examples.Add("ChatExample", new OpenApiExample
        {
            Summary = "Example chat request",
            Description = "Example of how to send a chat message to the DevOps assistant",
            Value = new Microsoft.OpenApi.Any.OpenApiObject
            {
                ["message"] = new Microsoft.OpenApi.Any.OpenApiString("Create a branch called feature-auth"),
                ["userId"] = new Microsoft.OpenApi.Any.OpenApiString("LuisiitoDev"),
                ["timestamp"] = new Microsoft.OpenApi.Any.OpenApiString("2025-07-06 14:14:34")
            }
        });

        document.Components.Examples.Add("BranchExample", new OpenApiExample
        {
            Summary = "Example branch creation request",
            Description = "Example of how to create a new Git branch",
            Value = new Microsoft.OpenApi.Any.OpenApiObject
            {
                ["repository"] = new Microsoft.OpenApi.Any.OpenApiString("LuisiitoDev/DevOpsAssistant"),
                ["branchName"] = new Microsoft.OpenApi.Any.OpenApiString("feature-new-feature"),
                ["baseBranch"] = new Microsoft.OpenApi.Any.OpenApiString("main"),
                ["userId"] = new Microsoft.OpenApi.Any.OpenApiString("LuisiitoDev")
            }
        });

        // Add tags
        document.Tags = new List<OpenApiTag>
            {
                new()
                {
                    Name = "Chat",
                    Description = "AI-powered chat assistant using Semantic Kernel"
                }
            };

        // Add custom extension data
        document.Extensions.Add("x-owner", new Microsoft.OpenApi.Any.OpenApiString("LuisiitoDev"));
        document.Extensions.Add("x-current-time", new Microsoft.OpenApi.Any.OpenApiString("2025-07-06 14:14:34"));
        document.Extensions.Add("x-timezone", new Microsoft.OpenApi.Any.OpenApiString("UTC"));
        document.Extensions.Add("x-api-type", new Microsoft.OpenApi.Any.OpenApiString("DevOps Assistant"));

        return Task.CompletedTask;
    }
}