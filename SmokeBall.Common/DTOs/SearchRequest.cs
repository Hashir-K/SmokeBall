namespace SmokeBall.Common.DTOs
{
    public class SearchRequest
    {
        public SearchRequest() { }

        public string URI { get; set; }

        public string Keywords { get; set; }

        public int NumberOfResults { get; set; }

    }
}
