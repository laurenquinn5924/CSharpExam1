using System;
using System.ComponentModel.DataAnnotations;

namespace CSharpExam1.Models
{
    public class RSVP : BaseEntity
    {
        [Key]
        public int RSVPSId { get; set; }
        public int ParticipantActivityId { get; set; } //Foreign Key
        public int UserId { get; set; } //Foreign Key
        public User User { get; set; }
        public Activity ParticipantActivity { get; set; }
    }
}