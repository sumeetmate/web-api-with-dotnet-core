namespace USWalks.Models.DTO
{
    public class AddRegionRequest
    {
        public string Code { get; set; } = String.Empty;

        public string Name { get; set; } = String.Empty;

        public double Area { get; set; }

        public double Lat { get; set; }

        public double Long { get; set; }

        public long Populations { get; set; }

    }
}
