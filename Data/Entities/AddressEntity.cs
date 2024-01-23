using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class AddressEntity
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public required string City { get; set; }
        [Required]
        public required string Street { get; set; }
        [Required]
        public required string PostalCode { get; set; }
        public ISet<AuthorEntity>? Authors { get; set; }
    }
}
