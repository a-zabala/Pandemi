﻿using Pandemi.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Pandemi.ViewModels
{
    public class ReportViewModel
    {
        public List<Book> Books { get; set; }
        public List<FamilyMember> FamilyMembers { get; set; }
        public List<Accomplishment> Accomplishments { get; set; }
        public List<Food> Foods { get; set; }
        public List<JournalEntry> JournalEntries { get; set; }
        public FamilyMember FamilyMember { get; set; }
       


    }
}
