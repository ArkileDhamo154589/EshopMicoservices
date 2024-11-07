
namespace Catalog.API.Products.CreateProduct;
//command and result classes 
public record CreateProductCommand(string Name , List<string> Category, string Description , string ImageFile, decimal Price)
    : ICommand<CreateProductResult>;
public record CreateProductResult(Guid Id);
internal class CreateProductCommandHandler(IDocumentSession session)
    : ICommandHandler<CreateProductCommand, CreateProductResult>
{
    public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {

       
        //return CreateProductResult result
        //create Product entity from command object
        //from catalog api create new variation product that it takes the data
        var product = new Product
        {
            Name = command.Name,
            Category = command.Category,
            Description = command.Description,
            ImageFile = command.ImageFile,
            Price = command.Price,
        };
        //save to database
        session.Store(product);
        await session.SaveChangesAsync(cancellationToken);
        //return result
        return new CreateProductResult(product.Id);

    }
}
