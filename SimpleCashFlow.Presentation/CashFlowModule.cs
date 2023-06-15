

using Carter;
using Carter.OpenApi;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Routing;

namespace SimpleCashFlow.Presentation
{
    public class CashFlowModule: CarterModule
    {

        public CashFlowModule(): base("/cashflow")
        {
                
        }

        public override void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/", () =>
            {
                return Results.Ok();
            })
            .IncludeInOpenApi();

            app.MapGet("/summary", () =>
            {
                return Results.Ok();
            })
                .IncludeInOpenApi();
        }
        
    }
}