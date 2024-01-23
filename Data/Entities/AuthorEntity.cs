using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    [Table("AuthorEntity")]
    public class AuthorEntity
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [MaxLength(120)]
        public string? Description { get; set; }
        public AddressEntity? Address { get; set; }
        public int AddressId { get; set; }
        public ISet<PhotoEntity>? Photos { get; set; }
    }
}
