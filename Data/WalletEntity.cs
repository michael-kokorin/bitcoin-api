using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BitcoinApi.Data
{
    [Table("wallets")]
    public sealed class WalletEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(400)]
        public string Account { get; set; }

        public decimal Balance { get; set; }

        [Required]
        [MaxLength(400)]
        public string Address { get; set; }

        public DateTime Created { get; set; }

        public DateTime Updated { get; set; }

        public bool Removed { get; set; }
    }
}