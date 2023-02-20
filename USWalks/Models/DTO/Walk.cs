using USWalks.Models.Domain;

namespace USWalks.Models.DTO
{
    public class Walk
    {
        public Guid ID { get; set; }

        public string Name { get; set; } = String.Empty;

        public double Length { get; set; }
        public Guid RegionId { get; set; }

        public Guid WalkDifficultyId { get; set; }

        public Region Region { get; set; }

        public WalkDifficulty WalkDifficulty { get; set; }
    }
}
