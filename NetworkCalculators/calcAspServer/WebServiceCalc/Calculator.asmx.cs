using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace WebServiceCalc
{
    /// <summary>
    /// Сводное описание для Calculator
    /// </summary>
    [WebService(Namespace = "localhost:8888/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Чтобы разрешить вызывать веб-службу из скрипта с помощью ASP.NET AJAX, раскомментируйте следующую строку. 
    [System.Web.Script.Services.ScriptService]
    public class Calculator : System.Web.Services.WebService
    {

        [WebMethod]
        public string Calc(double num1, double num2, string opr)
        {
            double res = 0;
            switch (opr)
            {
                case "+":
                case "plus":
                    res = num1 + num2;
                    break;
                case "-":
                    res = num1 - num2;
                    break;
                case "/":
                    if (num2 != 0)
                    {
                        res = num1 / num2;
                    }
                    break;
                case "*":
                    res = num1 * num2;
                    break;
            }
            return $"{res}";
        }
    }
}
