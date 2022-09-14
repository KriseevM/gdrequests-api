using System.ComponentModel.DataAnnotations.Schema;

namespace gdrequests_api.Data.Entities;

public class LevelMetadata
{
    public int Id { get; set; }
    
    /// <summary>
    /// Level difficulty:
    /// 0 - N/A,
    /// 1 - easy,
    /// 2 - normal,
    /// 3 - hard,
    /// 4 - harder,
    /// 5 - insane.
    /// Stored in the 9 key of level data as a number multiplied by 10
    /// </summary>
    public int Difficulty { get; set; }
    
    public int Rate { get; set; }
    public bool IsEpic { get; set; }
    public bool IsFeatured { get; set; }
    public string LevelName { get; set; }
    public string Author { get; set; }
    public int AuthorId { get; set; }
    public Request Request { get; set; }
    
    public int RequestId { get; set; }
    public bool IsDemon { get; set; }
    public bool IsAuto { get; set; }
}