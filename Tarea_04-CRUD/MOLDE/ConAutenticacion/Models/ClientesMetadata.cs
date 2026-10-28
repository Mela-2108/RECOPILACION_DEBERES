using System;
using System.ComponentModel.DataAnnotations;

namespace ConAutenticacion.Models
{
    // 1. Vinculamos el MetadataType con tu clase Clientes generada por el .edmx
    [MetadataType(typeof(ClientesMetadata))]
    public partial class Clientes
    {
        // Esta clase parcial permite extender tu modelo sin alterar el .edmx
    }

    // 2. Aquí defines todas tus reglas y mensajes de validación
    public class ClientesMetadata
    {
        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(50, ErrorMessage = "El nombre no puede tener más de 50 caracteres.")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "La dirección es obligatoria.")]
        public string Direccion { get; set; }

        [Required(ErrorMessage = "La ciudad es obligatoria.")]
        public string Ciudad { get; set; }

        [Required(ErrorMessage = "La provincia es obligatoria.")]
        public string Provincia { get; set; }

        [Display(Name = "Código Postal")]
        [RegularExpression(@"^\d+$", ErrorMessage = "El código postal solo debe contener números.")]
        public string CodigoPostal { get; set; }

        [Required(ErrorMessage = "La cédula es obligatoria.")]
        [StringLength(15, MinimumLength = 10, ErrorMessage = "La cédula debe tener entre 10 y 15 dígitos.")]
        [RegularExpression(@"^\d+$", ErrorMessage = "La cédula solo debe contener números.")]
        public string Cedula { get; set; }

        [Required(ErrorMessage = "El país es obligatorio.")]
        public string Pais { get; set; }

        [Display(Name = "Teléfono")]
        [RegularExpression(@"^\d+$", ErrorMessage = "El teléfono solo debe contener números.")]
        public string Telefonos { get; set; }

        [Required(ErrorMessage = "Debe especificar el nombre de la foto.")]
        public string Foto { get; set; }
    }
}