namespace BookStoreAPI.ViewModels.Helpers
{
    public class ListOfOrderItemsIds : ListOfIds
    {
        public int Quantity { get; set; }
        public decimal BruttoPrice { get; set; }
    }
}
