using Microsoft.AspNetCore.Http;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;

namespace Pandemi.Models
{
    public class Image
    {
        [Key]
        public int ImageId { get; set; }

        public string Title { get; set; }

        [DisplayName("Image Name")]
        public string ImageName { get; set; }
        [DisplayName("Upload File")]
        [NotMapped]
        public IFormFile ImageFile { get; set; }
        //public FileInfo[] FileImage { get; set; }

    }

}

