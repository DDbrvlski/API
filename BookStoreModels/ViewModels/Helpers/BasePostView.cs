using System.Text.Json.Serialization;

namespace BookStoreViewModels.ViewModels.Helpers
{
    public class BasePostView : BaseView
    {
        public new int? Id { get; set; }

        [JsonIgnore]
        public bool IsActive { get; set; } = true;

        [JsonIgnore]
        public DateTime CreationDate { get; set; } = DateTime.Now;
        [JsonIgnore]
        public DateTime? ModifiedDate { get; set; }
    }
}
