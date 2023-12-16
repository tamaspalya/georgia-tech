using MediatR;

namespace Warehouse.Application.Features.Products.Commands.AddProduct
{
    public class AddProductCommand : IRequest<int>
    {
        public string ISBN { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Publisher { get; set; }
        public int PublicationYear { get; set; }
        public decimal Price { get; set; }
        public string? Genre { get; set; }
        public string Language { get; set; }
        public string? Description { get; set; }
        public string SellerId { get; set; }
        public int Quantity { get; set; }
    }
}
