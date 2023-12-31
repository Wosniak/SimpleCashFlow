﻿using Carter;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using SimpleCashFlow.Application.Summary.Queries;

namespace SimpleCashFlow.Presentation
{
    public class SummaryModule : CarterModule
    {

        public SummaryModule() : base("/summary")
        {
            this.RequireAuthorization();
        }

        public override void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/{date}", async (DateOnly date, ISender sender) =>
            {

                var result = await sender.Send(new GetDailySummaryByDateQuery(date));

                if (result.Value is null)
                {
                    return Results.NotFound();
                }

                return Results.Ok(result.Value);

            }).Produces(StatusCodes.Status404NotFound)
            .Produces<GetDailySummaryByDateQuery>(StatusCodes.Status200OK, "Application/Json")
            .WithName("GetByDate")
            .WithTags("Summary")
            .WithOpenApi(o => new(o)
            {
                Description = "Return a Cash Flow summary for informed date",
                Summary = "Get a Daily Summary by it´s date",
            });
        }
    }
}
