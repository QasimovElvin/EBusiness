using System.ComponentModel.DataAnnotations.Schema;

namespace EBusiness.Models;

public class Blog
{
    public int Id { get; set; }
    public string? Image { get; set; }
    [NotMapped]
    public IFormFile Imagefile { get; set; } = null!;
    public string Title { get; set; } = null!;
    public string Description { get; set; }=null!;
}
