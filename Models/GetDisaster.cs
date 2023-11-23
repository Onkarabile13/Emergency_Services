namespace Emergency_Services.Models
{
    public class GetDisaster
    {
        public string Id { get; set; }
        public string disasterStartDate { get; set; }
        public string disasterEndDate { get; set; }
        public string disasterLocation { get; set; }
        public string disasterDescription { get; set; }
        public string goodsSelection { get; set; }

        public string status { get; set; }
    }
}
