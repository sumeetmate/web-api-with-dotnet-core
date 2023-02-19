namespace USWalks.Models.Domain
{
    public class Region
    {
        public Guid ID { get; set; }

        public string Code { get; set; } = String.Empty;

        public string Name { get; set; } = String.Empty;

        public double Area { get; set; }

        public double Lat { get; set; }

        public double Long { get; set; }

        public long Populations { get; set; }

        public IEnumerable<Walk> Walks { get; set; }


    }
}
