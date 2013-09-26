using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApplication1;


namespace Pruebas
{
    class Program
    {
        static void Main(string[] args)
        {
            ControlStructuresAnalizer csa = new ControlStructuresAnalizer();
            List<string> forloop = new List<string>();
            forloop = csa.ForStatementAnalizer("for(int i = 0; i < n; i++)");
            Console.WriteLine(forloop[0].ToString());
            Console.ReadKey();

        }
    }
}
