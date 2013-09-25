using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication1
{
    public class Tokenizer
    {
        public List<string> separadorLineas(string cadena)
        {
            List<string> tokenizer = new List<string>();
            string[] n = cadena.Split(new string[] { Environment.NewLine}, StringSplitOptions.None );
            foreach (string item in n)
            {
                tokenizer.Add(item);
            }
            return tokenizer;
        }

        public List<string> separadorEspacios(string cadena) 
        {
            List<string> tokenizer = new List<string>();
            string [] n = cadena.Split(' ');
            foreach (string item in n)
            {
                tokenizer.Add(item);
            }
            return tokenizer;
        }
    }
}
