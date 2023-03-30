using DemoWebshop.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoWebshop.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Column(TypeName ="datetime")]
        public DateTime DateTime{ get; set; }

        [Required]
        [Column(TypeName = "decimal(9,2)")]
        public decimal Subtotal { get; set; }

        [Required]
        [Column(TypeName = "decimal(9,2)")]
        public decimal Tax { get; set; }

        [Required]
        [Column(TypeName = "decimal(9,2)")]
        public decimal Total { get; set; }

        //Application User klase je klasa korinsika povezana s Identitiy paketom (za prijavljene kupce)
        [ForeignKey(nameof(UserId))]
        public ApplicationUser User { get; set; }


        [Column(TypeName = "nvarchar(450)")]
        public string UserId { get; set; }

        //TODO: Billing i shiping klase sa svojstivma o kupcu (Za neprivaljenje kupce)
            //Svojstva: id, firstname, lastname, email , pphone, cisty, country, postalcode, message

        //TODO: Customers klasa koja je poveza sa ApplicationUser klasom(labava veza!)

    }
}
