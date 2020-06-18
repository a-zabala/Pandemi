using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Pandemi.Models
{
    public class JournalEntry
    {

        public int ID { get; set; }
        public string Entry { get; set; }
        public string EntryFile { get; set; }
        public string Name { get; set; }

        
        [DataType(DataType.Date)]
        public DateTime EntryDate { get; set; }
        public int FamilyMemberID { get; set; }

        public FamilyMember FamilyMember { get; set; }
        public string UserId { get; set; }
        public AppUser User { get; set; }
    }
}
