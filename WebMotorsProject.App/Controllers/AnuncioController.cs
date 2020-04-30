using AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Web.Configuration;
using System.Web.Mvc;
using WebMotorsProject.App.Models;
using WebMotorsProject.Models.BLL;
using WebMotorsProject.Models.DML;
using static WebMotorsProject.App.Util.UtilRest;

namespace WebMotorsProject.App.Controllers
{
    public class AnuncioController : Controller
    {
        private static string wsMarca = WebConfigurationManager.AppSettings["ApiMarcas"];
        private static string wsModelo = WebConfigurationManager.AppSettings["ApiModelos"];
        private static string wsVersao = WebConfigurationManager.AppSettings["ApiVersoes"];
        private static JsonRequest jsonRequest = new JsonRequest("");
        private static BindingList<KeyValuePair<string, string>> keyValuePairs = new BindingList<KeyValuePair<string, string>>();

        private readonly BoAnuncio _boAnuncio = new BoAnuncio();

        public ActionResult Index()
        {
            try
            {
                ObterListas(0,0);
                var listaAnuncio =
                    Mapper.Map<IEnumerable<Anuncio>, IEnumerable<AnuncioViewModel>>(_boAnuncio.Pesquisa("", "", "", 0));
                return View(listaAnuncio);
            }
            catch (Exception)
            {
                return View(new List<Anuncio>());
            }
        }

        public ActionResult Details(int id)
        {
            try
            {
                if (id == 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var result = Mapper.Map<Anuncio, AnuncioViewModel>(_boAnuncio.Consultar(id));
                if (result == null)
                {
                    return HttpNotFound();
                }
                return View(result);
            }
            catch (Exception)
            {
                return View(new AnuncioViewModel());
            }
        }

        public ActionResult Create()
        {
            ObterListas(0,0);
            return View();
        }

        public ActionResult Edit(int id)
        {
            try
            {
                if (id == 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var result = Mapper.Map<Anuncio, AnuncioViewModel>(_boAnuncio.Consultar(id));
                if (result == null)
                {
                    return HttpNotFound();
                }

                //Marcas
                jsonRequest = new JsonRequest(wsMarca);
                var listaMarca = jsonRequest.GET<List<ListaMarca>>(null, null, keyValuePairs);
                var _marca = listaMarca.Where(c => c.Name == result.Marca).FirstOrDefault();
                result.Marca = _marca.ID.ToString() == "" ? result.Marca : _marca.ID.ToString();

                //Modelos
                if (_marca != null)
                {
                    jsonRequest = new JsonRequest(wsModelo + "?MakeID=" + _marca.ID.ToString());
                    List<ListaModelo> listaModelos = jsonRequest.GET<List<ListaModelo>>(null, null, keyValuePairs);
                    var _modelo = listaModelos.Where(c => c.Name == result.Modelo).FirstOrDefault();
                    result.Modelo = _modelo.ID.ToString() == "" ? result.Modelo : _modelo.ID.ToString();

                    //Versao
                    if (_modelo != null)
                    {
                        jsonRequest = new JsonRequest(wsVersao + "?ModelID=" + _modelo.ID.ToString());
                        List<ListaVersao> listaVersoes = jsonRequest.GET<List<ListaVersao>>(null, null, keyValuePairs);
                        var _versao = listaVersoes.Where(c => c.Name == result.Versao).FirstOrDefault();
                        result.Versao = _versao.ID.ToString() == "" ? result.Versao : _versao.ID.ToString();
                    }
                }

                ObterListas(int.Parse(result.Marca), int.Parse(result.Modelo));
                return View(result);
            }
            catch (Exception)
            {
                return View(new AnuncioViewModel());
            }
        }
        public ActionResult Delete(int id)
        {
            try
            {
                if (id == 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var result = Mapper.Map<Anuncio, AnuncioViewModel>(_boAnuncio.Consultar(id));
                if (result == null)
                {
                    return HttpNotFound();
                }
                return View(result);
            }
            catch (Exception)
            {
                return View(new AnuncioViewModel());
                throw;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AnuncioViewModel anuncio, FormCollection fobj)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var anuncioDomain = Mapper.Map<AnuncioViewModel, Anuncio>(anuncio);

                    anuncioDomain.Marca = fobj["hiddenMarca"].ToString();
                    anuncioDomain.Modelo = fobj["hiddenModelo"].ToString();
                    anuncioDomain.Versao = fobj["hiddenVersao"].ToString();

                    _boAnuncio.Incluir(anuncioDomain);

                    ViewBag.Incluir = 1;

                    return RedirectToAction("Index");
                }

                return View(anuncio);
            }
            catch (Exception)
            {
                return View(anuncio);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AnuncioViewModel anuncio, FormCollection fobj)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var anuncioDomain = Mapper.Map<AnuncioViewModel, Anuncio>(anuncio);

                    anuncioDomain.Marca = fobj["hiddenMarca"].ToString();
                    anuncioDomain.Modelo = fobj["hiddenModelo"].ToString();
                    anuncioDomain.Versao = fobj["hiddenVersao"].ToString();

                    if (anuncioDomain.Marca == "" || anuncioDomain.Modelo == "" || anuncioDomain.Versao == "")
                    {
                        var result = Mapper.Map<Anuncio, AnuncioViewModel>(_boAnuncio.Consultar(anuncioDomain.Id));
                        if (anuncioDomain.Marca == "") { anuncioDomain.Marca = result.Marca; }
                        if (anuncioDomain.Modelo == "") { anuncioDomain.Modelo = result.Modelo; }
                        if (anuncioDomain.Versao == "") { anuncioDomain.Versao = result.Versao; }
                    }

                    _boAnuncio.Alterar(anuncioDomain);

                    ViewBag.Alterar = 1;
                    return RedirectToAction("Index");
                }
                return View(anuncio);
            }
            catch (Exception)
            {
                return View(anuncio);
            }
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                if (id == 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var result = Mapper.Map<Anuncio, AnuncioViewModel>(_boAnuncio.Consultar(id));
                if (result == null)
                {
                    return HttpNotFound();
                }
                else
                {
                    _boAnuncio.Excluir(id);
                }
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return RedirectToAction("Index");
            }
        }

        //public PartialViewResult _ConfirmacaoPartial(string marca, string modelo, string versao, int ano)
        //{
        //    try
        //    {
        //        var listaAnuncio = _boAnuncio.Pesquisa(marca, modelo, versao, ano);
        //        return PartialView("_ListaAnuncioPartial", listaAnuncio);
        //    }
        //    catch (Exception)
        //    {
        //        return PartialView("_ListaAnuncioPartial", new List<Anuncio>());
        //    }
        //}

        private void ObterListas(int marcar, int modelo)
        {
            //Marcas
            jsonRequest = new JsonRequest(wsMarca);
            var lista = jsonRequest.GET<List<ListaMarca>>(null, null, keyValuePairs);

            List<ListaMarca> marcas = new List<ListaMarca>();
            marcas.Add(new ListaMarca { ID = 0, Name = "Selecionar" });
            marcas.AddRange(lista);
            ViewBag.Marcas = new SelectList(marcas, "ID", "Name");

            //Modelos
            jsonRequest = new JsonRequest(wsModelo + "?MakeID=" + marcar.ToString());
            List<ListaModelo> modelos = jsonRequest.GET<List<ListaModelo>>(null, null, keyValuePairs);
            ViewBag.Modelos = new SelectList(modelos, "ID", "Name");

            //Versoes
            jsonRequest = new JsonRequest(wsVersao + "?ModelID=" + modelo.ToString());
            List<ListaVersao> versoes = jsonRequest.GET<List<ListaVersao>>(null, null, keyValuePairs);
            ViewBag.Versoes = new SelectList(versoes, "ID", "Name");
        }

        public JsonResult GetModelos(int marca)
        {
            jsonRequest = new JsonRequest(wsModelo + "?MakeID=" + marca.ToString());
            List<ListaModelo> modelos = jsonRequest.GET<List<ListaModelo>>(null, null, keyValuePairs);
            ViewBag.Modelos = new SelectList(modelos, "ID", "Name");

            return Json(ViewBag.Modelos, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetVersoes(int modelo)
        {
            jsonRequest = new JsonRequest(wsVersao + "?ModelID=" + modelo.ToString());
            List<ListaVersao> versoes = jsonRequest.GET<List<ListaVersao>>(null, null, keyValuePairs);
            ViewBag.Versoes = new SelectList(versoes, "ID", "Name");

            return Json(ViewBag.Versoes, JsonRequestBehavior.AllowGet);
        }
    }
}