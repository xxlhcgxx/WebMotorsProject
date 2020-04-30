using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebMotorsProject.Models.DAL.Anuncio;

namespace WebMotorsProject.Models.BLL
{
    public class BoAnuncio
    {
        /// <summary>
        /// Inclui um novo Anuncio
        /// </summary>
        /// <param name="Anuncio">Objeto de Anuncio</param>
        public long Incluir(DML.Anuncio Anuncio)
        {
            DaoAnuncio usu = new DaoAnuncio();
            return usu.Incluir(Anuncio);
        }

        /// <summary>
        /// Altera um Anuncio
        /// </summary>
        /// <param name="Anuncio">Objeto de Anuncio</param>
        public void Alterar(DML.Anuncio Anuncio)
        {
            DaoAnuncio usu = new DaoAnuncio();
            usu.Alterar(Anuncio);
        }

        /// <summary>
        /// Consulta o Anuncio pelo id
        /// </summary>
        /// <param name="id">id da Anuncio</param>
        /// <returns></returns>
        public DML.Anuncio Consultar(int id)
        {
            DaoAnuncio usu = new DaoAnuncio();
            return usu.Consultar(id);
        }

        /// <summary>
        /// Excluir o Anuncio pelo id
        /// </summary>
        /// <param name="id">id do Anuncio</param>
        /// <returns></returns>
        public void Excluir(long id)
        {
            DaoAnuncio usu = new DaoAnuncio();
            usu.Excluir(id);
        }

        /// <summary>
        /// Lista os Anuncio
        /// </summary>
        public List<DML.Anuncio> Pesquisa(string marca, string modelo, string versao, int ano)
        {
            DaoAnuncio usu = new DaoAnuncio();
            var _marca = marca == null ? "" : marca.ToString();
            var _modelo = modelo == null ? "" : modelo.ToString();
            var _versao = versao  == null ? "" : versao.ToString();
            return usu.Pesquisa(_marca, _modelo, _versao, ano);
        }
    }
}
