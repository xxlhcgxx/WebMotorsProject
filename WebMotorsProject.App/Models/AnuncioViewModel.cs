using System.ComponentModel.DataAnnotations;

namespace WebMotorsProject.App.Models
{
    public class AnuncioViewModel
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Seleciona uma Marca...")]
        [Display(Name = "Marca")]
        public string Marca { get; set; }

        [Required(ErrorMessage = "Seleciona um Modelo...")]
        [Display(Name = "Modelo")]
        public string Modelo { get; set; }

        [Required(ErrorMessage = "Seleciona uma Versão...")]
        [Display(Name = "Versão")]
        public string Versao { get; set; }

        [Required(ErrorMessage = "Preencha o Ano...")]
        //[StringLength(4, MinimumLength = 4)]
        [Display(Name = "Ano")]
        public int Ano { get; set; }

        [Required(ErrorMessage = "Preencha a Quilometragem...")]
        //[StringLength(7, MinimumLength = 1)]
        [Display(Name = "Quilometragem")]
        public int Quilometragem { get; set; }

        [Required(ErrorMessage = "Preencha a(s) Observação(ões)...")]
        [Display(Name = "Observações")]
        public string Observacao { get; set; }
    }

    public class ListaMarca
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }

    public class ListaModelo
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int MakeID { get; set; }
    }

    public class ListaVersao
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int ModelID { get; set; }
    }
}