using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CityInfo.Entities
{
    public class Cidade
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        [Required]
        [MaxLength(50)]
        public string Nome { get; set; }  
        public string Descricao { get; set; }  

        public ICollection<PontoTuristico> PontosTuristicos { get; set; } = new List<PontoTuristico>();
        
    }
}
