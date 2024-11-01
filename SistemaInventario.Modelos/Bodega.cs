using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaInventario.Modelos
{
    public class Bodega
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage ="El nombre es requerido")]
        [MaxLength(60, ErrorMessage ="El nombre debe tener un máximo de 60 caracteres")]
        public string Name { get; set; }
        [Required(ErrorMessage = "La descripcion es requerida")]
        [MaxLength(100, ErrorMessage = "La descripcion debe tener un máximo de 100 caracteres")]
        public string Description { get; set; }
        [Required(ErrorMessage = "El estado es requerido")]
        public bool Status { get; set; }
    }
}
