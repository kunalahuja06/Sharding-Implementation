namespace ShardingPetapoco.Data.Entities
{
    public class TenantMapping
    {
        public int Id { get; set; }
        public string TenantName { get; set; }
        public int TenantId { get; set; }
        public int ServerId { get; set; }
    }
}
