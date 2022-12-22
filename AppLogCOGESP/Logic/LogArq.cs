namespace AppLogCOGESP.Logic
{
    using AppArqTXTJL.Logic;
    using AppUtilJL.Logic;
    using System;
    using System.IO;

    public class LogArq : global::AppLogJL.Logic.LogJL
    {
        public LogArq()
            : base(PeriodosCriacaoArquivo.Sempre)
        {
            base.Iniciar("LogCogesp", () => GetCabecalhoLocal());
        }

        public static void LogException(Exception ex)
        {
            LogException(ex, GetCabecalhoLocal());
        }

        public static string GetCabecalhoLocal()
        {
            int maxLength = 0;

            try
            {
                maxLength = EnderecoCompletoArq.Length;

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
░█░░══════════ ▄▀─▄▀ ═══════████══██████══█████══████══████══████══{"".PadRight(maxLength - 44, '═')}░░█░
░█░░══════════ ──▀──▀ ══════██════██══██══██═════██════██════██══█═{"".PadRight(maxLength - 44, '═')}░░█░
░█░░══════════ █▀▀▀▀▀█▄ ════██════██══██══██═██══███═══████══████══{"".PadRight(maxLength - 44, '═')}░░█░
░█░░══════════ █░░░░░█─█ ═══██════██══██══██══█══██══════██══██════{"".PadRight(maxLength - 44, '═')}░░█░
░█░░══════════ ▀▄▄▄▄▄▀▀ ════████══██████══█████══████══████══██════{"".PadRight(maxLength - 44, '═')}░░█░
░█░░{"".PadRight(maxLength + 19, '═')}░░█░
░█░░═══════════════ CONTATO: cogesp@sad.ms.gov.br {"".PadRight(maxLength - NomeSistema.Length - 12, '═')}░░█░
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
