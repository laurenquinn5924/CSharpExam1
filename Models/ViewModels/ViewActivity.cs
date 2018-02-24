using System;
using System.ComponentModel.DataAnnotations;

namespace CSharpExam1.Models
{
    public class AddActivity : BaseEntity
    {
        [Display(Name = "Activity Title")]
        [Required]
        [MinLength(2)]
        public string ActivityTitle { get; set; }

        [Display(Name = "Time")]
        [Required]
        [MyDate(ErrorMessage = "Time must be in Future")]
        public DateTime Time { get; set; }


        [Required]
        [Display(Name = "Date of Activity")]
        [MyDate(ErrorMessage = "Date must be in Future")]
        public DateTime Date { get; set; }

        [Display(Name = "Duration")]
        [Required]
        [MinLength(2)]
        public string Duration { get; set; }

        [Display(Name = "Description")]
        [Required]
        public string Description { get; set;}
    }

    public class MyDateAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            DateTime d = Convert.ToDateTime(value);
            return d >= DateTime.Now;
        }
    }
}