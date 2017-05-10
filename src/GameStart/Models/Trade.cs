using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GameStart.Models
{
    [Table("Trades")]
    public class Trade
    {
        [Key]
        public int TradeId { get; set; }
        public int ToUserId { get; set; }
        public int FromUserId { get; set; }
        public string Message { get; set; }
        public bool Accepted { get; set; }
    }
}
