using IslamicPlatform.Domain.Enums;

namespace IslamicPlatform.Api.Models.Requests
{
    public class AddBookmarkRequest
    {
        public BookmarkType Type { get; set; }
        public int EntityId { get; set; }
    }
}
