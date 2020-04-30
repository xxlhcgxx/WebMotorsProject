using System.Collections.Generic;
using System.Data;
using System.Linq;
using WebMotorsProject.Models.DAL.Padrao;

namespace WebMotorsProject.Models.DAL.Anuncio
{
    /// <summary>
    /// Classe de acesso a dados do Anuncio
    /// </summary>
    internal class DaoAnuncio : AcessoDados
    {
        /// <summary>
        /// Inclui um novo Anuncio
        /// </summary>
        /// <param name="Anuncio">Objeto de Anuncio</param>
        internal long Incluir(DML.Anuncio Anuncio)
        {
            try
            {
                List<System.Data.SqlClient.SqlParameter> parametros = new List<System.Data.SqlClient.SqlParameter>();

                parametros.Add(new System.Data.SqlClient.SqlParameter("Marca", Anuncio.Marca));
                parametros.Add(new System.Data.SqlClient.SqlParameter("Modelo", Anuncio.Modelo));
                parametros.Add(new System.Data.SqlClient.SqlParameter("Versao", Anuncio.Versao));
                parametros.Add(new System.Data.SqlClient.SqlParameter("Ano", Anuncio.Ano));
                parametros.Add(new System.Data.SqlClient.SqlParameter("Quilometragem", Anuncio.Quilometragem));
                parametros.Add(new System.Data.SqlClient.SqlParameter("Observacao", Anuncio.Observacao));

                DataSet ds = base.Consultar("SP_IncAnuncio", parametros);
                long ret = 0;
                if (ds.Tables[0].Rows.Count > 0)
                    long.TryParse(ds.Tables[0].Rows[0][0].ToString(), out ret);
                return ret;
            }
            catch (System.Exception ex)
            {
                return 0;
            }
            
        }

        /// <summary>
        /// Consultar um Anuncio
        /// </summary>
        /// <param name="Anuncio">Objeto de Anuncio</param>
        internal DML.Anuncio Consultar(long Id)
        {
            List<System.Data.SqlClient.SqlParameter> parametros = new List<System.Data.SqlClient.SqlParameter>();

            parametros.Add(new System.Data.SqlClient.SqlParameter("Id", Id));

            DataSet ds = base.Consultar("SP_ConsAnuncio", parametros);
            List<DML.Anuncio> usu = Converter(ds);

            return usu.FirstOrDefault();
        }

        internal List<DML.Anuncio> Pesquisa(string marca, string modelo, string versao, int ano)
        {
            List<System.Data.SqlClient.SqlParameter> parametros = new List<System.Data.SqlClient.SqlParameter>();

            parametros.Add(new System.Data.SqlClient.SqlParameter("Marca", marca));
            parametros.Add(new System.Data.SqlClient.SqlParameter("Modelo", modelo));
            parametros.Add(new System.Data.SqlClient.SqlParameter("Versao", versao));
            parametros.Add(new System.Data.SqlClient.SqlParameter("Ano", ano));

            DataSet ds = base.Consultar("SP_PesqAnuncio", parametros);
            List<DML.Anuncio> usu = Converter(ds);

            return usu;
        }

        /// <summary>
        /// Alterar um novo Anuncio
        /// </summary>
        /// <param name="Anuncio">Objeto de Anuncio</param>
        internal void Alterar(DML.Anuncio Anuncio)
        {
            List<System.Data.SqlClient.SqlParameter> parametros = new List<System.Data.SqlClient.SqlParameter>();

            parametros.Add(new System.Data.SqlClient.SqlParameter("Marca", Anuncio.Marca));
            parametros.Add(new System.Data.SqlClient.SqlParameter("Modelo", Anuncio.Modelo));
            parametros.Add(new System.Data.SqlClient.SqlParameter("Versao", Anuncio.Versao));
            parametros.Add(new System.Data.SqlClient.SqlParameter("Ano", Anuncio.Ano));
            parametros.Add(new System.Data.SqlClient.SqlParameter("Quilometragem", Anuncio.Quilometragem));
            parametros.Add(new System.Data.SqlClient.SqlParameter("Observacao", Anuncio.Observacao));
            parametros.Add(new System.Data.SqlClient.SqlParameter("Id", Anuncio.Id));

            base.Executar("SP_AltAnuncio", parametros);
        }

        /// <summary>
        /// Excluir Anuncio
        /// </summary>
        /// <param name="Anuncio">Objeto de Anuncio</param>
        internal void Excluir(long Id)
        {
            List<System.Data.SqlClient.SqlParameter> parametros = new List<System.Data.SqlClient.SqlParameter>();

            parametros.Add(new System.Data.SqlClient.SqlParameter("Id", Id));

            base.Executar("SP_DelAnuncio", parametros);
        }

        private List<DML.Anuncio> Converter(DataSet ds)
        {
            List<DML.Anuncio> lista = new List<DML.Anuncio>();
            if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    DML.Anuncio usu = new DML.Anuncio();
                    usu.Id = row.Field<int>("Id");
                    usu.Marca = row.Field<string>("Marca");
                    usu.Modelo = row.Field<string>("Modelo");
                    usu.Versao = row.Field<string>("Versao");
                    usu.Ano = row.Field<int>("Ano");
                    usu.Quilometragem = row.Field<int>("Quilometragem");
                    usu.Observacao = row.Field<string>("Observacao");
                    lista.Add(usu);
                }
            }

            return lista;
        }
    }
}
