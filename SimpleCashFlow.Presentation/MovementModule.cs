

using Carter;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using SimpleCashFlow.Application.Movements.Commands.CreateMovement;
using SimpleCashFlow.Application.Movements.Commands.UpdateMovement;
using SimpleCashFlow.Application.Movements.Queries;
using SimpleCashFlow.Domain.Entities;
using SimpleCashFlow.Domain.Results;
using SimpleCashFlow.Presentation.Requests.Movement;
using SimpleCashFlow.Presentation.Responses;

namespace SimpleCashFlow.Presentation
{
    public class MovementModule : CarterModule
    {

        public MovementModule() : base("/movement")
        {
            this.RequireAuthorization();
        }

        public override void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/", async (CreateMovementRequest request, ISender sender) =>
            {
                var result = await sender.Send(new CreateMovementCommand(request.Date, request.Amount, request.Classificaion));

                if (!result.IsSuccess)
                {
                    return Results.BadRequest(result);
                }

                var responseMovement = CreateMovementResponse(result);

                return Results.CreatedAtRoute("GetById", routeValues: new { id = responseMovement.Id }, value: responseMovement);
            })
            .Produces<MovementResponse>(StatusCodes.Status201Created)
            .Produces<Result>(StatusCodes.Status400BadRequest, "Application/Json")
            .Accepts<CreateMovementRequest>("Application/Json")
            .WithName(nameof(CreateMovementRequest))
            .WithTags("Movements")
            .RequireAuthorization()
            .WithOpenApi(o => new(o)
            {
                Description = "Creates a new financial movement on cash flow.\nA valid and diferent from zero decimal value must be informed as Amount.\nA movement classification (origin of a credit or purpose of a payment) also is mandatory.",
                Summary = "Create a new cash flow movement",
            });

            app.MapGet("{id}", async (Guid id, ISender sender) =>
            {

                var result = await sender.Send(new GetMovementByIdQuery(id));

                if (result.Value is null)
                {
                    return Results.NotFound();
                }

                var movementResponse = CreateMovementResponse(result);

                return Results.Ok(movementResponse);
            })
            .Produces(StatusCodes.Status404NotFound)
            .Produces<MovementResponse>(StatusCodes.Status200OK, "Application/Json")
            .WithName("GetById")
            .WithTags("Movements")
            .RequireAuthorization()
            .WithOpenApi(o => new(o)
            {
                Description = "Return a already registered movement from database,quering bya it´s id.",
                Summary = "Get a Movement by it´s id",
            });

            app.MapPut("{id}", async (Guid id, UpdateMovementRequest request, ISender sender) =>
            {
                var result = await sender.Send(new UpdateMovementCommand(id, request.Date, request.Amount, request.Classificaion));

                if (!result.IsSuccess)
                {
                    if (result.HasValidationError)
                    {
                        return Results.BadRequest(result);
                    }
                    else
                    {
                        return Results.NotFound(result);
                    }
                }

                return Results.AcceptedAtRoute("GetById", routeValues: new { id = id });
            })
            .Produces(StatusCodes.Status200OK)
            .Produces<Result>(StatusCodes.Status400BadRequest, "Application/Json")
            .Accepts<UpdateMovementRequest>("Application/Json")
            .WithName(nameof(UpdateMovementRequest))
            .WithTags("Movements")
            .RequireAuthorization()
            .WithOpenApi(o => new(o)
            {
                Description = "Update financial movement on cash flow with provided id.\nA valid and diferent from zero decimal value must be informed as Amount.\nA movement classification (origin of a credit or purpose of a payment) also is mandatory.",
                Summary = "Update cash flow movement",
            });

        }

        private MovementResponse CreateMovementResponse(Result<Movement> result)
        {
            return new MovementResponse(result.Value.Id.Value, result.Value.Date, result.Value.Amount, result.Value.MovementType, result.Value.Classification);
        }

    }
}