namespace gdrequests_api.Data.Entities;

public class Request
{
    public int Id { get; set; }
    public int LevelId { get; set; }
    public int AddedAt { get; set; }
    public LevelMetadata Metadata { get; set; }
}