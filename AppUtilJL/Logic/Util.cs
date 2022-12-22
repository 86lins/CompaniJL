namespace AppUtilJL.Logic
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Text;

    public static class Util
    {

        public static string ISO_8859_1 { get; } = "iso-8859-1";

        public static String GetNomeSistema
        {
            get
            {
                try
                {
                    var assembly = Assembly.GetEntryAssembly();

                    if (assembly != null)
                        return Path.GetFileName(assembly.Location).Replace(".exe", "");

                    return "SystemLog";
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        /// <summary>
        /// "C:\\Temp\\{UtilGeral.GetNomeSistema}\\
        /// </summary>
        public static String GetEnderecoLog { get { return $"C:\\Temp\\{Util.GetNomeSistema}"; } }

        public static void SaveToExcel(this StringBuilder stringBuilder)
            => File.WriteAllBytes($"{GetEnderecoLog}\\{GetNomeSistema}_{DateTime.Now:yyyyMMddssmm}.xls", Encoding.GetEncoding(Util.ISO_8859_1).GetBytes(stringBuilder.ToString()));

        public static void SaveTableHtmlToExcel(StringBuilder arquivo)
            => File.WriteAllBytes($"{GetEnderecoLog}\\{GetNomeSistema}_{DateTime.Now:yyyyMMddssmm}.xls", Encoding.GetEncoding(Util.ISO_8859_1).GetBytes(arquivo.ToString()));

        /// <summary>
        /// separe as pastas com '\\'
        /// </summary>
        /// <param name="caminho"></param>
        public static void CriarPasta(string caminho = "C:\\Temp")
        {
            var vetorPasta = caminho.Replace("\\\\", "\\").Split('\\');

            string pasta = string.Empty;

            for (int i = 0; i < vetorPasta.Length; i++)
            {
                if (i != 0)
                    pasta += "\\";
                pasta += vetorPasta[i];
                if (!Directory.Exists(pasta))
                    Directory.CreateDirectory(pasta);
            }
        }

        public static string RemoverCaracteresEspeciais(string texto, string[] naoRemover = null)
        {
            for (int i = 0; i < CaracteresEspeciais.Length; i++)
            {
                if (naoRemover == null || naoRemover != null && !naoRemover.Contains(CaracteresEspeciais[i].ToString()))
                    texto = texto.Replace(CaracteresEspeciais[i].ToString(), "");
            }
            return texto;
        }

        public static string RemoverAcentos(string texto, string[] naoRemover = null)
        {
            for (int i = 0; i < ComAcentos.Length; i++)
            {
                if (naoRemover == null || naoRemover != null && !naoRemover.Contains(ComAcentos[i].ToString()))
                    texto = texto.Replace(ComAcentos[i].ToString(), SemAcentos[i].ToString());
            }
            return texto;
        }

        public static string ComAcentos { get; } = "ÄÅÁÂÀÃäáâàãÉÊËÈéêëèÍÎÏÌíîïìÖÓÔÒÕöóôòõÜÚÛüúûùÇç";
        public static string SemAcentos { get; } = "AAAAAAaaaaaEEEEeeeeIIIIiiiiOOOOOoooooUUUuuuuCc";
        public static string CaracteresEspeciais { get; } = ",<.>;:/?°~^]}º´`[{ª!¹@²$£%¢¨¬&*()_-=+§\'\"|\\";

        public static bool IsNull(this string txt) => string.IsNullOrEmpty(txt);

        public static string DtToStr(this DateTime data) => data.ToString("dd/MM/yyyy");

        public static string DtToStr(this DateTime? data) => !data.HasValue ? String.Empty : DtToStr(data.Value);

        public static DateTime StrToDt(this string data) => DateTime.ParseExact(data, "ddMMyyyy", System.Globalization.CultureInfo.InvariantCulture);

        public static int StrToInt32(this string numero)
        {
            if (!Int32.TryParse(numero, out int valor))
                throw new Exception($"Erro Conversão: {numero} to int");
            return valor;
        }

        public static string PadronizaEsq(this object txt, int tamanho) => $"{txt}".PadLeft(tamanho, ' ');

        public static string PadronizaDire(this object txt, int tamanho) => $"{txt}".PadRight(tamanho, ' ');
        public static int MesStrToNumeral(this string mes)
        {
            switch (mes.ToUpper())
            {
                case "JANEIRO": return 01;
                case "FEVEREIRO": return 02;
                case "MARÇO": case "MARCO": return 03;
                case "ABRIL": return 04;
                case "MAIO": return 05;
                case "JUNHO": return 06;
                case "JULHO": return 07;
                case "AGOSTO": return 08;
                case "SETEMBRO": return 09;
                case "OUTUBRO": return 10;
                case "NOVEMBRO": return 11;
                case "DEZEMBRO": return 12;
                default: throw new Exception($"Erro Conversão de mês: {mes}");
            }
        }

        public static void CriarArquivo(string caminhoArq = "C:\\Temp", string nomeArqComExt = "Teste.txt")
            => CriarArquivo($"{caminhoArq}\\{nomeArqComExt}");

        public static void CriarArquivo(string caminhoCompleto = "C:\\Temp\\Teste.txt")
        {
            if (File.Exists(caminhoCompleto))
                return;

            using (FileStream fileStream = new FileStream(caminhoCompleto, FileMode.CreateNew, FileAccess.ReadWrite, FileShare.ReadWrite))
            {
                fileStream.Close();
                fileStream.Dispose();
            }
        }
    }
}
