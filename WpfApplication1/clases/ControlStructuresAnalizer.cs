using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WpfApplication1
{
    public class ControlStructuresAnalizer
    {
        #region Analize
        public string StatementAnalize(string statement) 
        {
            string res = string.Empty;
            if (statement.Contains('('))
            {
                if (statement.Contains(')'))
                {
                    statement = statement.Replace("(", "/openBrac");
                    statement = statement.Replace(")", "/closeBrac");
                    Match match = Regex.Match(statement, @"( )?[/openBrac]( )?.*( )?/closeBrac$", RegexOptions.IgnoreCase);
                    if (match.Success)
                    {
                        res = match.Groups[0].Value.ToString().Replace("/openBrac", "");
                        res = res.Replace("/closeBrac", "");
                    }
                }
                else
                {
                    res = "Se esperaba ')'";
                }
            }
            else {
                res = "Se esperaba '('"; 
            }
            return res;
        }
        public string statementFilter(string statement, string type)
        {
            string aux = "";
            Match ifst = Regex.Match(statement, @type+"( )?.*", RegexOptions.IgnoreCase);
            if (ifst.Success)
            {
                aux = ifst.Groups[0].Value.ToString().Replace(type, "");
                aux = StatementAnalize(aux);
                if (aux.Contains("Se esperaba")) {
                    aux = "Error en " + type + " statement:\n" + statement + "\n" + aux;
                }
            }
            return aux;
        }
#endregion Analize
        public string IfStatementAnalizer(string ifStatement)
        {
            return statementFilter(ifStatement, "if");
        }
        public string WhileStatementAnalizer(string whileStatement)
        {
            return statementFilter(whileStatement, "while");
        }
        public List<string> ForStatementAnalizer(string forStatement)
        {
            string aux = statementFilter(forStatement, "for");
            var forValues = new List<string>();
            if (!aux.Contains("Error")) 
            {
                var iteratRestrict = aux.Split(';');
                foreach (string rest in iteratRestrict)
                {
                    forValues.Add(rest);
                }
                if (forValues.Count != 3) { 
                    /////verificar asignacion de variable como primera entrada(int i = 0), condicion(i<n) y expresion final(i++)////////////
                }
            }
            return forValues;
        }
    }
}
