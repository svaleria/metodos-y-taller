using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication1
{
    public class controlLLaves
    {
        private List<int> entrada = new List<int>();
        private List<int> salida = new List<int>();

        public string verificarLLaves(List<string> text)
        {
            entrada.Clear();
            salida.Clear();
            //buscamos en el texto las { y } y guardamos sus posiciones
            for (int i = 0; i < text.Count; i++)
            {
                if (text[i].Contains("{"))
                {
                    addEntada(i);
                }
                if (text[i].Contains("}"))
                {
                   addSalida(i);
                }
            }

            if (ControlLLaves() == -1) // si el numero de { y } son iguales
            {
                return controlParentesis(text);//llamamos a control de ( ) 
            }
            else
            {
                return "se esperaba } de la linea: " + ControlLLaves().ToString();
            }

        }

        private string controlParentesis(List<string> text)
        {
            int control = 0, ind = 0;
            for (int i = 0; i < text.Count; i++)
            {
                if (text[i].Contains("(")) //si encuentra un ( el control se suma en 1
                {
                    control++;
                    ind = i; // guardamos la linea en caso de no tener )
                } 
                if (text[i].Contains(")")) // si encuentra  ) control se resta 
                    control--;
            }
            if (control != 0) // si ( y ) no son iguales
            {
                return "se esperaba ) en la linea: " + (ind+1).ToString();
            }
            else
            {
                return "";
            }
        }

        public void addEntada(int Linea)
        {
            entrada.Add(Linea);
        }

        public void addSalida(int Linea)
        {
            salida.Add(Linea);
        }

        public int ControlLLaves()
        { 
            int num = entrada.Count - salida.Count;
            if (num == 0)
                return -1;
            return entrada[num-1] + 1;
        }
    }
}
