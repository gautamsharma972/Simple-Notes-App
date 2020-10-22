﻿using System;

namespace NotesApp.Models
{
    public class NotesViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}