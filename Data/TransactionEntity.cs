using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BitcoinApi.Data
{
    [Table("transactions")]
    public class TransactionEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(400)]
        public string Account { get; set; }

        [Required]
        [MaxLength(400)]
        public string Address { get; set; }

        [Required]
        [MaxLength(400)]
        public string Category { get; set; }

        public decimal Amount { get; set; }

        [MaxLength(400)]
        public string Label { get; set; }

        [Required]
        [MaxLength(400)]
        public string TransactionId { get; set; }

        public int Confirmations { get; set; }

        [Required]
        [MaxLength(400)]
        public string BlockHash { get; set; }

        public int BlockIndex { get; set; }

        public int BlockTime { get; set; }

        public int Time { get; set; }

        public int TimeReceived { get; set; }
    }
}