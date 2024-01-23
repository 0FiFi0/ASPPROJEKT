using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Data.Entities
{
    public enum PhotoFormat
    {
        JPEG,
        PNG,
        GIF,
    }

    [Table("photos")]
    public class PhotoEntity
    {
        [Column("id")]
        [Key]
        public int PhotoId { get; set; }
        [MaxLength(50)]
        public string? Camera { get; set; }
        [MaxLength(120)]
        public string? Description { get; set; }
        [MaxLength(50)]
        public string? Resolution { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; }
        public PhotoFormat Format { get; set; }
        public AuthorEntity? Author { get; set; }
        [Required]
        public int AuthorId { get; set; }
    }
}