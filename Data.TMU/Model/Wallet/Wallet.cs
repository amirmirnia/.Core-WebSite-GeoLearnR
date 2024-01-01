using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.TMU.Model.Wallet
{
    public class Wallet
    {
        [Key]
        public int walletid { get; set; }

        [ForeignKey("walletType")]
        public int TypeId { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
        [DisplayName("مبلغ")]
        [Required(ErrorMessage = "{0}را وارد کنید")]
        [MaxLength(100, ErrorMessage = "{0}نمی تواند بیشتر از {1}باشد")]
        public int Amount { get; set; }
        [DisplayName("توضیح")]
        [Required(ErrorMessage = "{0}را وارد کنید")]
        [MaxLength(100, ErrorMessage = "{0}نمی تواند بیشتر از {1}باشد")]
        public string Description { get; set; }

        public bool IsPay { get; set; }
        public DateTime DetaTime { get; set; }

        public virtual User User { get; set; }
        public virtual walletType walletType { get; set; }
    }
    public class ChargeWalletViewMode
    {
        [DisplayName("مبلغ")]
        [Required(ErrorMessage = "{0}را وارد کنید")]
        public int Amount { get; set; }
    }

}
