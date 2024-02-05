using GianssWebApi.Models.Dto;

namespace GianssWebApi.Data
{
    public static class GianStore
    {

        public static List<GianDto> GianList = new List<GianDto>
       {
           new GianDto { Id = 1, Name = "Dato 1", MetrosCuadrados = 40, Ocupantes = 3},
           new GianDto { Id = 2, Name = "Dato 2", MetrosCuadrados = 80, Ocupantes = 5}

       };

    }
}
