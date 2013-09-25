using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        string ruta = "";
        string NombreArchivo = ""; //ruta+nombre.extension
        string directorios = "";

        Dictionary<string, string> hola = new Dictionary<string, string>();
        public MainWindow()
        {
            InitializeComponent();
        }

        internal string getRichText() {
            string richText = new TextRange(textBox.Document.ContentStart, textBox.Document.ContentEnd).Text;
            return richText;
        }

        
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog save = new SaveFileDialog();
            save.DefaultExt = ".java";
            save.Filter = "Java File (.java)|*.java";
            Nullable<bool> result = save.ShowDialog();
            if (result == true)
            {
                ruta = "";
                ruta = save.FileName;
                directorios = System.IO.Path.GetDirectoryName(ruta);
                NombreArchivo = save.SafeFileName;
                if (!File.Exists(ruta))
                {
                    FileInfo newDoc = new FileInfo(ruta);
                    FileStream fs = newDoc.Create();
                    fs.Close();
                    string richText = getRichText();
                    File.WriteAllText(ruta, richText);
                }
                else if (File.Exists(ruta))
                {
                    string richText = getRichText();
                    File.WriteAllText(ruta, richText);
                    
                }
            }

        }

      

        private void OpenButton_Click(object sender, RoutedEventArgs e)
        {
            
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.DefaultExt = ".java";
            ofd.Filter = "Java File (.java)|*.java";
            Nullable<bool> res = ofd.ShowDialog();
            if (res == true)
            {
                TextRange txt = new TextRange(textBox.Document.ContentStart, textBox.Document.ContentEnd);
                txt.Text = "";
                ruta = "";
                ruta = ofd.FileName;
                directorios = System.IO.Path.GetDirectoryName(ruta);
                NombreArchivo = ofd.SafeFileName;
                textBox.AppendText(File.ReadAllText(ruta));

            }
        }
        internal void CreateOutput() {
            string richText = getRichText();
            File.WriteAllText(ruta, richText);

            string commandLine = "javac " + @ruta + " 2> " + @directorios + @"\output.out";
            System.Diagnostics.ProcessStartInfo PSI = new System.Diagnostics.ProcessStartInfo("cmd.exe");
            PSI.RedirectStandardInput = true;
            PSI.RedirectStandardOutput = true;
            PSI.RedirectStandardError = true;
            PSI.UseShellExecute = false;
            System.Diagnostics.Process p = System.Diagnostics.Process.Start(PSI);
            System.IO.StreamWriter SW = p.StandardInput;
            System.IO.StreamReader SR = p.StandardOutput;
            SW.WriteLine(commandLine);
            SW.Close();
        }
        internal void ShowErrorMessages() {
            string result = File.ReadAllText(directorios + @"\output.out");

            if (result != string.Empty)
            {
                var lines = result.Split('\n');

                foreach (var line in lines)
                {
                    Match match = Regex.Match(line, @"error: .*", RegexOptions.IgnoreCase);//ignora casos de caracteres especiales
                    if (match.Success)
                    {
                        string n = line.Replace(match.Groups[0].Value, "");
                        ErrorResultScrollViewer.Content += n + "\n" + match.Groups[0].Value;
                    }
                    else
                    {
                        ErrorResultScrollViewer.Content += line;
                    }
                }
            }
            else
            {
                ErrorResultScrollViewer.Content = "Compilation Succeeded";
            }
        }
        private void CompileButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("soy feo");
            ErrorResultScrollViewer.Content = string.Empty;
            try
            {
                if (@ruta != string.Empty)
                {
                    if (File.Exists(@ruta + @"/output.out"))
                    {
                        FileInfo fi = new FileInfo(@ruta + @"/output.out");
                        fi.Delete();
                        CreateOutput();
                        ShowErrorMessages();
                    }
                    else {
                        CreateOutput();
                        ShowErrorMessages();
                    }
                    
                        ///////////////////////////////////Mensajes de error en pantalla///////////////////////////////////////
                    
                }
                else
                {
                       MessageBox.Show("File not saved or opened", "Alert");
                }
            }
                
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
    }
}
