namespace BookStoreAPI.ViewModels.Helpers
{
    public class BasePostView : BaseView
    {
        public new int? Id { get; set; }
        public bool IsActive { get; set; } = true;

        public DateTime CreationDate { get; set; } = DateTime.Now;
        public DateTime? ModifiedDate { get; set; }
    }
}
