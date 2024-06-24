namespace ShardingPetatpoco.Services.Implementations
{
    public class RequestContextBuilder
    {
        public RequestContext BuildRequestContext()
        {
            // This is a dummy implementation. In a real-world scenario, you would fetch the tenantId from the database.
            return new RequestContext
            {
                TenantId = new Random().Next(1, 4)
            };
        }
    }

    public class RequestContext
    {
        public int TenantId { get; set; }
    }
}
