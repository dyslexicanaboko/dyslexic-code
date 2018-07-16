using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MvcApplication1.Models
{
    public class AddExample
    {
        [Required]
        [DisplayName("X Value")]
        public int X { get; set; }

        [Required]
        [DisplayName("Y Value")]
        public int Y { get; set; }

        [Required]
        public int Result { get; set; }

        public void Calculate()
        {
            Result = X + Y;
        }
    }
}