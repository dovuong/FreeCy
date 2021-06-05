namespace Models.Framework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("User")]
    public partial class User
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public User()
        {
            Bids = new HashSet<Bid>();
            Conversations = new HashSet<Conversation>();
            Messages = new HashSet<Message>();
            Products = new HashSet<Product>();
        }

        [Key]
        public int ID_User { get; set; }

        [Required]
        [StringLength(20)]
        public string UserName { get; set; }

        [Required]
        [StringLength(3000)]
        public string Password { get; set; }

        [StringLength(20)]
        public string GroupID { get; set; }

        [Required]
        [StringLength(100)]
        public string Email { get; set; }

        public int? Phone { get; set; }

        public DateTime? Birthday { get; set; }

        public bool? Status { get; set; }

        public bool? isFreeLancer { get; set; }

        public bool? isEmployer { get; set; }

        [StringLength(500)]
        public string ResetPasswordCode { get; set; }

        public bool? isEmailVerify { get; set; }

        [Column(TypeName = "text")]
        public string Address { get; set; }

        [Column(TypeName = "text")]
        public string WorkTime { get; set; }

        public DateTime? CreateAt { get; set; }

        public Guid? ActivationCode { get; set; }

        [StringLength(3000)]
        public string ConfirmPassword { get; set; }

        [StringLength(3000)]
        public string Name { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Bid> Bids { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Conversation> Conversations { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Message> Messages { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Product> Products { get; set; }
    }
}
