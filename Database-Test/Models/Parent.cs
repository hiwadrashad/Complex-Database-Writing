using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database_Test.Models
{
    public class Parent
    {
        //[KeyAttribute()]
        //[DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public string Text { get; set; }
        public int? ChildId { get; set; }
        public Child Child { get; set; }
        public List<GrandChild> GrandChildren { get; set; }
        public string GrandChildrenId { get; set; }
    }
}
