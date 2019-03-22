using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LottoFunctions.Data.Models
{
    public class Draw
    {
        [Key]
        public int Id { get; set; }
        public int DrawType { get; set; }
        public int DrawNo { get; set; }
        public DateTime DrawDate { get; set; }
        public List<DrawDetail> DrawDetails { get; set; }
    }
}
