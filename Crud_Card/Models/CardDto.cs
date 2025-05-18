using System.ComponentModel.DataAnnotations;

namespace Crud_Card.Models
{
   

    public class CardDto
    {
        public int Id { get; set; }

          [Required(ErrorMessage = "El número de tarjeta es obligatorio.")]
       // [CreditCard(ErrorMessage = "El número de tarjeta no es válido.")]
        public string CardNumber { get; set; }

        [Required(ErrorMessage = "El nombre del titular es obligatorio.")]
       // [StringLength(100 , ErrorMessage = "El nombre del titular no debe exceder los 100 caracteres.")]
        public string CardHolder { get; set; }

        [Required(ErrorMessage = "La fecha de expiración es obligatoria.")]
       // [RegularExpression(@"^(0[1-9]|1[0-2])\/?([0-9]{2})$" , ErrorMessage = "La fecha de expiración debe tener el formato MM/YY.")]
        public string ExpiryDate { get; set; }

        [Required(ErrorMessage = "El CVV es obligatorio.")]
       // [Range(100 , 9999 , ErrorMessage = "El CVV debe tener entre 3 y 4 dígitos.")]
        public int Cvv { get; set; }
    }

}
