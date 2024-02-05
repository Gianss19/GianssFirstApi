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

       
        public int MetrosCuadrados { get; set; }
        public int Ocupantes { get; set; }
    }
}
