using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database_Test.Models
{
    public class Child
    {
        //[KeyAttribute()]
        //[DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public string Text { get; set; }
        public int? GrandChildId { get; set; }
        [ForeignKey("GrandChildId")]
        public GrandChild GrandChild { get; set; }
    }
}
