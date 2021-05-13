namespace Projection.Domain.Model
{
    public class SearchPersonFilter
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string SortBy { get; set; }


        public string FirstName { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public string Phone { get; set; }

    }
}