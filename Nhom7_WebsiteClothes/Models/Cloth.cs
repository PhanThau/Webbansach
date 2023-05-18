namespace Nhom7_WebsiteClothes.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Cloth
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Cloth()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(150)]
        public string Title { get; set; }

        public string Description { get; set; }

        public int? CategoryId { get; set; }

        public int? SizeId { get; set; }

        public decimal? Price { get; set; }

        [StringLength(100)]
        public string Image { get; set; }

        public virtual Category Category { get; set; }

        public virtual Size Size { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
