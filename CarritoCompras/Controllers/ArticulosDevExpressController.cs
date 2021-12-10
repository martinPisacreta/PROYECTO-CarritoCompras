using CarritoCompras.Models;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace CarritoCompras.Controllers
{
    [Route("api/[controller]/[action]")]
    public class ArticulosDevExpressController : Controller
    {
        private CarritoComprasContext _context;
        FamiliumsController familiums_controller = new FamiliumsController();

        public ArticulosDevExpressController(CarritoComprasContext context)
        {
            _context = context;
        }


        [HttpGet("{utilidad}")]
        [Route("GetArticulos/{utilidad}")]
        public async Task<IActionResult> GetArticulos(DataSourceLoadOptions loadOptions, int utilidad)
        {

            decimal _utilidad = Convert.ToDecimal(utilidad);
            decimal _cien = Convert.ToDecimal(100);
            decimal _uno = Convert.ToDecimal(1);
            var articulos = _context.Articulos
                            .Where(a => a.FecBaja == null) //solamente los activos
                            .Select(a => new
                            {
                                id = a.IdArticulo,
                                codigoArticulo = a.CodigoArticulo,
                                precioLista_por_coeficiente_por_medioIva = a.PrecioLista * familiums_controller.coeficiente_articulo(a.IdTablaFamiliaNavigation) * 1.105M,
                                pathImagenArticulo = a.PathImg,
                                smPathImagenArticulo = a.PathImg,
                                descripcionArticulo = a.DescripcionArticulo,
                                marcaArticulo = //si la marca tiene imagen , muestro el nombre de la imagen , sino muestro el nombre de la marca
                                                (a.IdTablaFamiliaNavigation.IdTablaMarcaNavigation.PathImg != null && a.IdTablaFamiliaNavigation.IdTablaMarcaNavigation.PathImg != "")
                                                            ? a.IdTablaFamiliaNavigation.IdTablaMarcaNavigation.PathImg
                                                            : a.IdTablaFamiliaNavigation.IdTablaMarcaNavigation.TxtDescMarca,
                                familiaArticulo = a.IdTablaFamiliaNavigation.TxtDescFamilia,
                                stockArticulo = a.Stock == null ? 999999999 : a.Stock,
                                ofertaArticulo = a.SnOferta,
                                precioListaArticulo = a.PrecioLista,
                                coeficienteArticulo = familiums_controller.coeficiente_articulo(a.IdTablaFamiliaNavigation),
                                utilidadArticulo = (a.PrecioLista * familiums_controller.coeficiente_articulo(a.IdTablaFamiliaNavigation) * 1.105M) * ((_utilidad / _cien) + _uno),
                            });




            for (int i = (loadOptions.Filter?.Count ?? 0) - 1; i >= 0; i--)
            {
                //if (loadOptions.Filter[i].GetType().Name == "String")
                //{
                //    string columna = loadOptions.Filter[0].ToString();
                //    string valor = loadOptions.Filter[2].ToString();
                //    valor = valor.Replace(" ", "%");
                //    if (columna == "descripcionArticulo")
                //    {

                //        articulos = articulos.Where(x => EF.Functions.Like(x.descripcionArticulo, "% " + valor + "%"));
                //        loadOptions.Filter.Remove(loadOptions.Filter[i]);
                //        break;
                //    }

                //}
                if (loadOptions.Filter[i].GetType().Name == "JArray")
                {
                    JArray srcfilterarray = JArray.Parse(loadOptions.Filter[i].ToString());
                    string columna = srcfilterarray[0].ToString();
                    //var valor = srcfilterarray[2].ToString().Replace(" ", "%");
                    string[] valor = srcfilterarray[2].ToString().Split(" ");
                    if (columna == "descripcionArticulo")
                    {

                        foreach (var v in valor)
                        {

                            articulos = articulos.Where(x => EF.Functions.Like(x.descripcionArticulo, "%" + v + "%"));
                        }


                        articulos.Distinct();


                        loadOptions.Filter.Remove(loadOptions.Filter[i]);
                        if (i == 0)
                        {
                            loadOptions.Filter.Remove(loadOptions.Filter[i]);
                        }
                        break;



                    }
                }


            }




            return Json(await DataSourceLoader.LoadAsync(articulos, loadOptions));
        }

        [HttpPost]
        public async Task<IActionResult> Post(string values)
        {
            var model = new Articulo();
            var valuesDict = JsonConvert.DeserializeObject<IDictionary>(values);
            PopulateModel(model, valuesDict);

            if (!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            var result = _context.Articulos.Add(model);
            await _context.SaveChangesAsync();

            return Json(result.Entity.IdArticulo);
        }

        [HttpPut]
        public async Task<IActionResult> Put(long key, string values)
        {
            var model = await _context.Articulos.FirstOrDefaultAsync(item => item.IdArticulo == key);
            if (model == null)
                return StatusCode(409, "Object not found");

            var valuesDict = JsonConvert.DeserializeObject<IDictionary>(values);
            PopulateModel(model, valuesDict);

            if (!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete]
        public async Task Delete(long key)
        {
            var model = await _context.Articulos.FirstOrDefaultAsync(item => item.IdArticulo == key);

            _context.Articulos.Remove(model);
            await _context.SaveChangesAsync();
        }


        [HttpGet]
        public async Task<IActionResult> FamiliaLookup(DataSourceLoadOptions loadOptions)
        {
            var lookup = from i in _context.Familia
                         orderby i.TxtDescFamilia
                         select new
                         {
                             Value = i.IdTablaFamilia,
                             Text = i.TxtDescFamilia
                         };
            return Json(await DataSourceLoader.LoadAsync(lookup, loadOptions));
        }

        private void PopulateModel(Articulo model, IDictionary values)
        {
            string ID_ARTICULO = nameof(Articulo.IdArticulo);
            string CODIGO_ARTICULO_MARCA = nameof(Articulo.CodigoArticuloMarca);
            string CODIGO_ARTICULO = nameof(Articulo.CodigoArticulo);
            string DESCRIPCION_ARTICULO = nameof(Articulo.DescripcionArticulo);
            string PRECIO_LISTA = nameof(Articulo.PrecioLista);
            string ID_TABLA_FAMILIA = nameof(Articulo.IdTablaFamilia);
            string SN_OFERTA = nameof(Articulo.SnOferta);
            string PATH_IMG = nameof(Articulo.PathImg);
            string FECHA_ULT_MODIF = nameof(Articulo.FechaUltModif);
            string FEC_BAJA = nameof(Articulo.FecBaja);
            string ACCION = nameof(Articulo.Accion);
            string STOCK = nameof(Articulo.Stock);
            string ID_ORDEN = nameof(Articulo.IdOrden);

            if (values.Contains(ID_ARTICULO))
            {
                model.IdArticulo = Convert.ToInt64(values[ID_ARTICULO]);
            }

            if (values.Contains(CODIGO_ARTICULO_MARCA))
            {
                model.CodigoArticuloMarca = Convert.ToString(values[CODIGO_ARTICULO_MARCA]);
            }

            if (values.Contains(CODIGO_ARTICULO))
            {
                model.CodigoArticulo = Convert.ToString(values[CODIGO_ARTICULO]);
            }

            if (values.Contains(DESCRIPCION_ARTICULO))
            {
                model.DescripcionArticulo = Convert.ToString(values[DESCRIPCION_ARTICULO]);
            }

            if (values.Contains(PRECIO_LISTA))
            {
                model.PrecioLista = values[PRECIO_LISTA] != null ? Convert.ToDecimal(values[PRECIO_LISTA], CultureInfo.InvariantCulture) : (decimal?)null;
            }

            if (values.Contains(ID_TABLA_FAMILIA))
            {
                model.IdTablaFamilia = values[ID_TABLA_FAMILIA] != null ? Convert.ToInt32(values[ID_TABLA_FAMILIA]) : (int?)null;
            }

            if (values.Contains(SN_OFERTA))
            {
                model.SnOferta = values[SN_OFERTA] != null ? Convert.ToInt32(values[SN_OFERTA]) : (int?)null;
            }

            if (values.Contains(PATH_IMG))
            {
                model.PathImg = Convert.ToString(values[PATH_IMG]);
            }

            if (values.Contains(FECHA_ULT_MODIF))
            {
                model.FechaUltModif = values[FECHA_ULT_MODIF] != null ? Convert.ToDateTime(values[FECHA_ULT_MODIF]) : (DateTime?)null;
            }

            if (values.Contains(FEC_BAJA))
            {
                model.FecBaja = values[FEC_BAJA] != null ? Convert.ToDateTime(values[FEC_BAJA]) : (DateTime?)null;
            }

            if (values.Contains(ACCION))
            {
                model.Accion = Convert.ToString(values[ACCION]);
            }

            if (values.Contains(STOCK))
            {
                model.Stock = values[STOCK] != null ? Convert.ToInt32(values[STOCK]) : (int?)null;
            }

            if (values.Contains(ID_ORDEN))
            {
                model.IdOrden = values[ID_ORDEN] != null ? Convert.ToInt64(values[ID_ORDEN]) : (long?)null;
            }
        }

        private string GetFullErrorMessage(ModelStateDictionary modelState)
        {
            var messages = new List<string>();

            foreach (var entry in modelState)
            {
                foreach (var error in entry.Value.Errors)
                    messages.Add(error.ErrorMessage);
            }

            return String.Join(" ", messages);
        }
    }
}