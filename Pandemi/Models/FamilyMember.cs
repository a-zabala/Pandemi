using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Pandemi.Models
{
    public class FamilyMember
    {
        public int ID { get; set; }
        [Display(Name = "Family Member")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        public int Age { get; set; }
        public string UserId { get; set; }
        public AppUser User { get; set; }

        public ICollection<JournalEntry> JournalEntries { get; set; }
       public ICollection<Book> Books { get; set; }
    }
}
