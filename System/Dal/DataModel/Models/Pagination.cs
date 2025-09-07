namespace DataModel.Models
{
    public class Pagination
    {
        public bool UsePagination { get; init; } = false;
        public int PageSize { get; init; } = 20;
        public int PageNumber { get; init; } = 1;

        public int Skip => (PageNumber - 1) * PageSize;
    }
}