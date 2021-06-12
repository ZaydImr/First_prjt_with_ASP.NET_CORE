﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using webCore.Models;

namespace webCore.ViewModels
{
    public class BookAuthorViewModel
    {
        public int BookId { get; set; }

        [Required]
        [MaxLength(20)]
        [MinLength(5)]
        public string Title { get; set; }

        [Required]
        [MinLength(30)]
        public string Description { get; set; }
        public int AuthorId { get; set; }
        public List<Author> Authors { get; set; }
    }
}
