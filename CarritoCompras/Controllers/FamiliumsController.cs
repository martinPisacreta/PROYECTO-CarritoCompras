using CarritoCompras.Models;
using Microsoft.AspNetCore.Mvc;

namespace CarritoCompras.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FamiliumsController : ControllerBase
    {

        public decimal coeficiente_articulo(Familium f)
        {


            decimal a_1 = f.Algoritmo1 == 0.00M ? 1 : f.Algoritmo1;
            decimal a_2 = f.Algoritmo2 == 0.00M ? 1 : f.Algoritmo2;
            decimal a_3 = f.Algoritmo3 == 0.00M ? 1 : f.Algoritmo3;
            decimal a_4 = f.Algoritmo4 == 0.00M ? 1 : f.Algoritmo4;
            decimal a_5 = f.Algoritmo5 == 0.00M ? 1 : f.Algoritmo5;
            decimal a_6 = f.Algoritmo6 == 0.00M ? 1 : f.Algoritmo6;
            decimal a_7 = f.Algoritmo7 == 0.00M ? 1 : f.Algoritmo7;
            decimal a_8 = f.Algoritmo8 == 0.00M ? 1 : f.Algoritmo8;
            decimal a_9 = f.Algoritmo9 == 0.00M ? 1 : f.Algoritmo9;

            decimal _coeficiente_articulo = a_1 * a_2 * a_3 * a_4 * a_5 * a_6 * a_7 * a_8 * a_9;

            return _coeficiente_articulo;

        }
    }
}
