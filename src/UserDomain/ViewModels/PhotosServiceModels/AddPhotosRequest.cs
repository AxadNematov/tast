using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http;

namespace UserDomain.ViewModels.PhotosServiceModels;

public class AddPhotosRequest
{
    [JsonIgnore]
    [Newtonsoft.Json.JsonIgnore]
    public int UserId { get; set; }
    
    [Required]
    public IFormFile File { get; set; }
}