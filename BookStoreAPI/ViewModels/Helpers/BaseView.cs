namespace BookStoreAPI.ViewModels.Helpers
{
    public class BaseView
    {
        public int Id { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreationDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
