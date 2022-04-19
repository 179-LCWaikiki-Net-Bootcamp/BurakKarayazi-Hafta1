using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Week1.Models
{
    public class Item
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        //[RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$",
        //ErrorMessage = "Characters are not allowed.")]
        public string Name { get; set; }
        public int VendorValue { get; set; }

    }
}
