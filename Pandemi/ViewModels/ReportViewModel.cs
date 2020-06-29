using Pandemi.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Pandemi.ViewModels
{
    public class ReportViewModel
    {
        [Required]
        public string Title { get; set; }


        public string Notes { get; set; }
        public int ID { get; set; }
        public string Author { get; set; }
        [Display(Name = "Family Member")]
        public int FamilyMemberID { get; set; }
        public string FirstName { get; set; }
        public string UserId { get; set; }
        public List<Book> Books { get; set; }
        public ReportViewModel()
        {
        }
        //public ReportViewModel(IEnumerable<Book> books)
        //{

          //  Books = new List<Book>();
           // foreach (Book book in books)
            //{

              //  Books.Add(new Book
                //{
                  //  Title = book.Title,
                    //Author = book.Author,
                    //Notes = book.Notes,
                    //FamilyMember = book.FamilyMember
                //});
            //}

        //}

    }
}
