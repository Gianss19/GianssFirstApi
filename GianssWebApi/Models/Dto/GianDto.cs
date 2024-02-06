using System.ComponentModel.DataAnnotations;

namespace GianssWebApi.Models.Dto
{
    public class GianDto
    {
        [Required]
        public int Id { get; set; }
        
        [Required]
        [MaxLength(20)]
        public  string Name { get; set; }
        public string Detalle { get; set; }

        [Required]
        public double Tarifa { get; set; }

        public int Ocupantes { get; set; }

        public int MetrosCuadrados { get; set; }

        public string ImagenUrl { get; set; }

        public string Amenidad { get; set; }


    }
}
