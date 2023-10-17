using MediatR;


namespace JwtStore.Api.Extensions;

public static class AccountContextExtension
{
    public static void AddAccountContext(this WebApplicationBuilder builder)
    {
        #region Create ***************************************************

        builder.Services.AddTransient<
            JwtStore.Core.Contexts.AccountContext.UseCases.Create.Interfaces.IRepository,
            JwtStore.Infra.Contexts.AccountContext.UseCases.Create.Implementations.Repository
        >();
        builder.Services.AddTransient<
            JwtStore.Core.Contexts.AccountContext.UseCases.Create.Interfaces.IService,
            JwtStore.Infra.Contexts.AccountContext.UseCases.Create.Implementations.Service
        >();
        #endregion

        #region Authenticate *********************************************

        builder.Services.AddTransient<
            JwtStore.Core.Contexts.AccountContext.UseCases.Authenticate.Interfaces.IRepository,
            JwtStore.Infra.Contexts.AccountContext.UseCases.Authenticate.Implementations.Repository
        >();
        #endregion
    }

    public static void AddAccountEndPoints(this WebApplication app)
    {
        #region Create ***************************************************

        app.MapPost("api/v1/users", handler: async(
            JwtStore.Core.Contexts.AccountContext.UseCases.Create.Request request,
            IRequestHandler<
                JwtStore.Core.Contexts.AccountContext.UseCases.Create.Request,
                JwtStore.Core.Contexts.AccountContext.UseCases.Create.Response> handler) => 
        {
            var result = await handler.Handle(request, new CancellationToken());
            return result.IsSuccess
                ? Results.Created($"api/v1/users/{result.Data?.Id}", result)
                : Results.Json(result, statusCode: result.Status);
        });
        #endregion
        
        #region Authenticate *********************************************
        
        app.MapPost("api/v1/auth", handler: async(
            JwtStore.Core.Contexts.AccountContext.UseCases.Authenticate.Request request,
            IRequestHandler<
                JwtStore.Core.Contexts.AccountContext.UseCases.Authenticate.Request,
                JwtStore.Core.Contexts.AccountContext.UseCases.Authenticate.Response> handler) => 
        {
            var result = await handler.Handle(request, new CancellationToken());
            if (!result.IsSuccess)
                return Results.Json(result, statusCode: result.Status);

            if (result.Data is null)
                return Results.Json(result, statusCode: 500);

            result.Data.Token = JwtExtension.Generate(result.Data);
            return Results.Ok(result);
        }).RequireAuthorization("Admin");

        #endregion
    }
}