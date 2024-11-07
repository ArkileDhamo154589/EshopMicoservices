
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Products.CreateProduct;

public record CreateProductRequest(string Name, List<string> Category, string Description , string ImageFile , decimal Price);

public record CreateProductResponse(Guid Id);

public class CreateProudctEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        _ = app.MapPost("/products",
            async (CreateProductRequest request, ISender sender) =>
            {
                var command = request.Adapt<CreateProductCommand>();

                var result = await sender.Send(command);

                var response = result.Adapt<CreateProductResponse>();

                return Results.Created($"/products/{response.Id}", response);
            })
            .WithName("CreateProduct")
            .Produces<CreateProductResponse>(StatusCodes.Status201Created)
            .Produces<ProblemDetails>(StatusCodes.Status404NotFound)
            .WithSummary("Create Product")
            .WithDescription("Create Product");
    }
}

