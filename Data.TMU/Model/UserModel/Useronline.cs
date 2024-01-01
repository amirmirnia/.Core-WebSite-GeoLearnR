using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.TMU.User
{
    public class Useronline
    {
        [Key]
        public int Id { get; set; }
        public string Idcode { get; set; }

        public string name { get; set; }
        public string connectionId { get; set; }
        public DateTime DetaNews { get; set; }
        public bool IsOnline { get; set; }
    }
}
