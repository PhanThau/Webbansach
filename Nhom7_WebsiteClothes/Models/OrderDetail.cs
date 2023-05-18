namespace Nhom7_WebsiteClothes.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("OrderDetail")]
    public partial class OrderDetail
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int OrderNo { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ClothesId { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public virtual Cloth Cloth { get; set; }

        public virtual Order Order { get; set; }
    }
}
