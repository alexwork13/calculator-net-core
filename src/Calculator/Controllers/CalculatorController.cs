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

        [HttpPost]
        public IActionResult EquationData(String equation, int count = 3)
        {
            ViewData["Title"] = "Equation Data";
            ViewData["Equation"] = equation;
            ViewData["Count"] = count;

            List<char> variables = new List<char>();

            for(char i='A'; i<='Z'; i++)
            {
                if (equation.Contains(i))
                {
                    variables.Add(i);
                }
            }

            ViewData["Variables"] = variables;

            return View();
        }

        [HttpPost]
        public IActionResult EquationResult(String equation, int count = 3)
        {
            
            List<char> variables = new List<char>();

            for (char i = 'A'; i <= 'Z'; i++)
            {
                if (equation.Contains(i))
                {
                    variables.Add(i);
                }
            }

            Dictionary<char, List<double>> datas = new Dictionary<char, List<double>>();
             
            foreach (char c in variables)
            {
                //Get All Data from Form Based on Variable
                String all = HttpContext.Request.Form[c+"[]"];
                String[] items = all.Split(',');
                List<double> doubles = new List<double>();

                foreach(String item in items)
                {
                    doubles.Add(Convert.ToDouble(item));
                }

                datas.Add(c,doubles);
            }
            List<String> expressions = new List<String>();
            int size = datas.ElementAt(0).Value.Count;

            for (int i = 0; i < size; i++)
            {
                String expression = equation;
                foreach(char c in variables)
                {
                    expression = expression.Replace(c+"",datas[c].ElementAt(i)+"");
                }

                expressions.Add(expression+" , ");
            }

            ViewData["Expressions"] = expressions;
            ViewData["Equation"] = equation;
            ViewData["Datas"] = datas;
            ViewData["Variables"] = variables;
            ViewData["Count"] = count;
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
