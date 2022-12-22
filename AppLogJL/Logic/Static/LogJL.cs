namespace AppLogJL.Logic.Static
{
    using System;    
    using static AppLogJL.Logic.LogJL;

    public static class LogJL
    {
        private static Logic.LogJL _logJL;

        private static bool start = false;

        public static void GravaLog(object log)
        {
            if (!start)
            {
                start = true;
                _logJL = new Logic.LogJL( PeriodosCriacaoArquivo.Sempre, nomeArqLog: "LogGeral");
            }

            _logJL.GravaLog(log);
        }

        public static void LogException(Exception ex)
        => Logic.LogJL.LogException(ex, GetCabecalho());

    }
}
