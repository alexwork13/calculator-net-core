using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Calculator.Controllers
{
    public class CalculatorController : Controller
    {

        public IActionResult Index()
        {
            ViewData["Title"] = "Calculator";
            ViewData["Hasil"] = 0;
            return View();
        }

        [HttpPost]
        public IActionResult Index(SimpleEquation simpleEquation)
        {
            float hasil = 0.0f;
            switch (simpleEquation.op)
            {
                case "+":
                    hasil = simpleEquation.p1 + simpleEquation.p2;
                    break;

                case "-":
                    hasil = simpleEquation.p1 - simpleEquation.p2;
                    break;

                case "*":
                    hasil = simpleEquation.p1 * simpleEquation.p2;
                    break;

                case "/":
                    hasil = simpleEquation.p1 / simpleEquation.p2;
                    break;

                case "%":
                    hasil = simpleEquation.p1 % simpleEquation.p2;
                    break;

                case "^":
                    hasil = (float) Math.Pow(Convert.ToDouble(simpleEquation.p1), Convert.ToDouble(simpleEquation.p2));
                    break;

                default:
                    hasil = 0.0f;
                    break;
            }

            ViewData["Hasil"] = hasil; 
            return View();
        }


        public IActionResult Equation()
        {
            ViewData["Title"] = "Equation";
            return View();
        }

        public IActionResult EquationData()
        {
            ViewData["Title"] = "Equation";
            return View();
        }
    }

    public class SimpleEquation
    {
       public float p1 { get; set; }
       public String op { get; set; }
       public float p2 { get; set; } 
    }
}
