using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NotesApp.Models
{
    public class NotesInputModel
    {
        public Guid Id { get; set; }

        [Display(Name ="Title")]
        [Required(ErrorMessage ="Please provide a Title")]
        [StringLength(100, ErrorMessage ="Title cannot be more than 100" )]
        public string Title { get; set; }

        [Display(Name ="Description")]
        [Required(ErrorMessage ="Please write a description for your Note")]
        public string Description { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
