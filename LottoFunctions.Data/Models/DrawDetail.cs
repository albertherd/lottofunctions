using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LottoFunctions.Data.Models
{
    public class DrawDetail
    {
        [Key]
        public int Id { get; set; }
        public int Number { get; set; }
        public int DrawOrder { get; set; }
    }
}
