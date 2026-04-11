namespace AnrukiShop_API.Requests
{
    public class CreateInventoryRequest
    {
        public required int ProductId { get; set; }
        public required int Quantity { get; set; }
        public required string Location { get; set; }
    }

    public class StockChangeRequest
    {
        public required int Amount { get; set; }
    }

    public class ChangeLocationRequest
    {
        public required string Location { get; set; }
    }

    public class SetQuantityRequest
    {
        public required int Quantity { get; set; }
    }
}
