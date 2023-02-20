namespace USWalks.Models.DTO
{
    public class AddWalkRequest
    {
        public string Name { get; set; } = String.Empty;

        public double Length { get; set; }
        public Guid RegionId { get; set; }

        public Guid WalkDifficultyId { get; set; }
        
    }
}
