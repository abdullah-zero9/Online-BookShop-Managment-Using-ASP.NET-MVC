using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace Ans_1.Models.ViewModels
{
    public class BookVM
    {
        public int Id { get; set; }
        [Required, StringLength(50), Display(Name = "Name")]
        public string BookName { get; set; }
        [Required, Column(TypeName = "money"), Display(Name = "Price"), DisplayFormat(DataFormatString = "{0:0.00}", ApplyFormatInEditMode = true)]
        public decimal Price { get; set; }
        [Column(TypeName = "date"), Display(Name = "Publish Date"), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime PublishDate { get; set; }
        public bool Available { get; set; }
        [Display(Name = "Picture")]
        public string PicturePath { get; set; }

        //fk
        [Display(Name = "Category")]
        [ForeignKey("Category")]
        public int CId { get; set; }
        public string CName { get; set; }
        public HttpPostedFileBase Picture { get; set; }
    }
}