using Pandemi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pandemi.ViewModels
{
    public class JournalEntriesViewModel
    {
        public List<FamilyMember> FamilyMembers { get; set; }
        public List<JournalEntry> JournalEntries { get; set; }
        public FamilyMember FamilyMember { get; set; }
    }
}
