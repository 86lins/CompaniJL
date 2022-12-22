namespace AppLogJL.Logic
{
    using AppArqTXTJL.Logic;
    using System;
    using System.IO;
    using AppUtilJL.Logic;
    
    public class LogJL
    {
        internal static ArqTXT arqTxt;
        private readonly PeriodosCriacaoArquivo _periodoCriacaoArquivo;

        public string NomeArqLog { get; private set; }
        public static string NomeSistema { get; private set; }

        public string EnderecoArq { get { return arqTxt.EnderecoArq; } }
        public static string EnderecoCompletoArq { get { return arqTxt.EnderecoCompletoArq; } }

        public LogJL(PeriodosCriacaoArquivo periodoCriacaoArquivo, string nomeArqLog = "LogGeral")
        {
            this._periodoCriacaoArquivo = periodoCriacaoArquivo;
            NomeSistema = InformacoesLocais.GetNomeSistema;
            this.NomeArqLog = $"{nomeArqLog}{GetDataNoArquivo(periodoCriacaoArquivo)}";

            arqTxt = new ArqTXT($"C:\\Temp\\{NomeSistema}", this.NomeArqLog, "log");
            arqTxt.EscreverLinha(GetCabecalho());
        }

        protected internal LogJL(PeriodosCriacaoArquivo periodoCriacaoArquivo = PeriodosCriacaoArquivo.Nunca)
        {
            this._periodoCriacaoArquivo = periodoCriacaoArquivo;
            NomeSistema = InformacoesLocais.GetNomeSistema;
        }

        public enum PeriodosCriacaoArquivo { Nunca, Diariamente, Mensalmente, Anualmente, Sempre }

        protected internal void Iniciar(string nomeArqLog, Func<string> func)
        {
            this.NomeArqLog = $"{nomeArqLog}{GetDataNoArquivo(_periodoCriacaoArquivo)}";
            arqTxt = new ArqTXT($"C:\\Temp\\{NomeSistema}", this.NomeArqLog, "log");
            arqTxt.EscreverLinha(func());
        }

        private static string GetDataNoArquivo(PeriodosCriacaoArquivo periodoCriacaoArquivo)
        {
            switch (periodoCriacaoArquivo)
            {
                case PeriodosCriacaoArquivo.Nunca:
                    return string.Empty;
                case PeriodosCriacaoArquivo.Diariamente:
                    return "_" + DateTime.Now.ToString("dd");
                case PeriodosCriacaoArquivo.Mensalmente:
                    return "_" + DateTime.Now.ToString("ddMM");
                case PeriodosCriacaoArquivo.Anualmente:
                    return "_" + DateTime.Now.ToString("ddMMyyyy");
                case PeriodosCriacaoArquivo.Sempre:
                    return "_" + DateTime.Now.ToString("ddMMyyyyhhmmss");
                default:
                    return string.Empty;
            }
        }

        public static void LogException(Exception ex, string cabecalho)
        {
            string nomeSistema = string.Empty;

            try
            {
                nomeSistema = InformacoesLocais.GetNomeSistema;
            }
            catch { }

            string caminho = $"C:\\Temp{(string.IsNullOrEmpty(nomeSistema) ? "" : "\\" + nomeSistema)}";

            Util.CriarPasta(caminho);

            caminho += $"\\LogErrorFatal_{(string.IsNullOrEmpty(nomeSistema) ? string.Empty : $"{nomeSistema}_") }{DateTime.Now:ddMMyyyyHHmmssfff}.log";

            if (!File.Exists(caminho))
            {
                using (FileStream fileStream = new FileStream(caminho, FileMode.CreateNew, FileAccess.ReadWrite, FileShare.ReadWrite))
                {
                    fileStream.Close();
                    fileStream.Dispose();
                }
            }

            using (TextWriter textWriter = File.AppendText(caminho))
            {
                if (!string.IsNullOrEmpty(cabecalho))
                    textWriter.WriteLine(cabecalho);

                textWriter.WriteLine(GetLinhaLogFormat($@"
--- ERRO FATAL NÃO TRATADO ---

Exception:
    1.--------Message: '{ex.Message}'.
    2.---------Source: '{ex.Source}'.
    3.-----TargetSite: '{ex.TargetSite}'.
    4.-InnerException: '{ex.InnerException}'.
    5.----StackTracer: '{(ex.StackTrace??"").TrimStart()}'.

'{ex}'."));
                textWriter.Close();
                textWriter.Dispose();
            }
        }

        public void GravaLog(object texto)
            => arqTxt.EscreverLinha(GetLinhaLogFormat(texto));

        private static string GetLinhaLogFormat(object texto)
            => $"[{DateTime.Now:dd/MM/yyyy HH:mm:ss-fff}] {texto}";

        public static string GetCabecalho()
        {
            int maxLength = 0;

            try
            {
                maxLength = arqTxt.EnderecoCompletoArq.Length;

                if (NomeSistema.Length > maxLength)
                    maxLength = NomeSistema.Length;

                if (InformacoesLocais.MaquinaLocalIPV4_Host.Length > maxLength)
                    maxLength = InformacoesLocais.MaquinaLocalIPV4_Host.Length;

                if (InformacoesLocais.MaquinaLocalDominioUsuarioLogado.Length > maxLength)
                    maxLength = InformacoesLocais.MaquinaLocalDominioUsuarioLogado.Length;
            }
            catch { }

            if (maxLength < 60)
                maxLength = 60;

            return $"" +
$@"{"".PadRight(maxLength + 27, '░')}
░█{"".PadRight(maxLength + 23, '▀')}█░
░█░░{"".PadRight(maxLength + 19, '═')}░░█░
░█░░{"".PadRight(20, '═')} Dev.Jhonny Lins 86lins@gmail.com {"".PadRight(maxLength - 35, '═')}░░█░
░█░░{"".PadRight(maxLength + 19, '═')}░░█░
░█░░══════════════════ ▄▀─▄▀  ══════██════██████══█████═{"".PadRight(maxLength - 33, '═')}░░█░
░█░░══════════════════ ──▀──▀ ══════██════██══██══██════{"".PadRight(maxLength - 33, '═')}░░█░
░█░░══════════════════ █▀▀▀▀▀█▄ ════██════██══██══██═██═{"".PadRight(maxLength - 33, '═')}░░█░
░█░░══════════════════ █░░░░░█─█ ═══██════██══██══██══█═{"".PadRight(maxLength - 33, '═')}░░█░
░█░░══════════════════ ▀▄▄▄▄▄▀▀ ════████══██████══█████═{"".PadRight(maxLength - 33, '═')}░░█░
░█░░{"".PadRight(maxLength + 19, '═')}░░█░
░█░░═══════════════════ APP: {NomeSistema} {"".PadRight(maxLength - NomeSistema.Length - 7, '═')}░░█░
░█░░═══════════════ USUARIO: {InformacoesLocais.MaquinaLocalDominioUsuarioLogado} {"".PadRight(maxLength - InformacoesLocais.MaquinaLocalDominioUsuarioLogado.Length - 7, '═')}░░█░
░█░░════════════════════ PC: {InformacoesLocais.MaquinaLocalIPV4_Host} {"".PadRight(maxLength - InformacoesLocais.MaquinaLocalIPV4_Host.Length - 7, '═')}░░█░
░█░░════════════ DtExecução: {DateTime.Now:dd/MM/yyyy HH:mm:ss-fff} {"".PadRight(maxLength - 30, '═')}░░█░
░█░░{"".PadRight(maxLength + 19, '═')}░░█░
░█{"".PadRight(maxLength + 23, '▄')}█░      
{"".PadRight(maxLength + 27, '░')}";

        }
    }
}
