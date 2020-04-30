namespace WebMotorsProject.Models.DML
{
    public class Anuncio
    {
        public int Id { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public string Versao { get; set; }
        public int Ano { get; set; }
        public int Quilometragem { get; set; }
        public string Observacao { get; set; }

    }

    //public class ListaMarca
    //{
    //    public int ID { get; set; }
    //    public string Name { get; set; }
    //}

    //public class ListaModelo
    //{
    //    public int ID { get; set; }
    //    public string Name { get; set; }
    //    public int MakeID { get; set; }
    //}

    //public class ListaVersao
    //{
    //    public int ID { get; set; }
    //    public string Name { get; set; }
    //    public int ModelID { get; set; }
    //}
}
