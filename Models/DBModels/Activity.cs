using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CSharpExam1.Models
{
    public class Activity : BaseEntity
    {
        [Key]
        public int ActivityId { get; set; }
        public string ActivityTitle { get; set; }
        public DateTime Date { get; set; }
        public DateTime Time { get; set; }
        public string Duration { get; set; }
        public string Description { get; set; }

        public int UserId { get; set; }
        public List<RSVP> RSVPS { get; set; }

        public Activity()
        {
            RSVPS = new List<RSVP>();
        }

    }

}