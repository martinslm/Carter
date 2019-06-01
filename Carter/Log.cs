using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Carter
{
    class Log
    {
        private static string caminhoExe = string.Empty;
        private static string nomeArquivo = string.Format("Log{0}", DateTime.Now.Date.ToString());
        public static bool Add(string strMensagem)
        {
            try
            {
                caminhoExe = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                string caminhoArquivo = Path.Combine(caminhoExe, nomeArquivo);
                if (!File.Exists(caminhoArquivo))
                {
                    FileStream arquivo = File.Create(caminhoArquivo);
                    arquivo.Close();
                }
                using (StreamWriter w = File.AppendText(caminhoArquivo))
                {
                    AppendLog(strMensagem, w);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        private static void AppendLog(string logMensagem, TextWriter txtWriter)
        {
            try
            {
                txtWriter.Write("\r\nLog Entrada : ");
                txtWriter.WriteLine($"{DateTime.Now.ToLongTimeString()} {DateTime.Now.ToLongDateString()}");
                txtWriter.WriteLine("  :");
                txtWriter.WriteLine($"  :{logMensagem}");
                txtWriter.WriteLine("------------------------------------");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
