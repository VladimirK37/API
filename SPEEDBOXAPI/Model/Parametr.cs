using System.ComponentModel.DataAnnotations;

namespace SPEEDBOXAPI.Model
{
    public class Parametr
    {
        //[Required]
        //[Display(Name = "Вес в граммах")]
        public int Weight { get; set; }

        public int Height { get; set; }

        public int Width { get; set; }

        public int Length { get; set; }

    }
}
