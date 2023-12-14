namespace EventBus.Messages.Events
{
    public class BookAddedEvent : IntegrationBaseEvent
    {
        public required string ISBN { get; set; }
        public required string Title { get; set; }
        public required string Author { get; set; }
        public required string Publisher { get; set; }
        public int PublicationYear { get; set; }
        public decimal Price { get; set; }
        public string? Genre { get; set; }
        public required string Language { get; set; }
        public string? Description { get; set; }
        public required string SellerId { get; set; }
        public int Quantity { get; set; }
    }
}
