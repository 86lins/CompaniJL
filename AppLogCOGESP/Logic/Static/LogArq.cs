namespace AppLogCOGESP.Logic.Static
{
    using System;
    using static AppLogCOGESP.Logic.LogArq;

    public static class LogArq
    {
        private static Logic.LogArq _logArq;

        private static bool start = false;

        public static void GravaLog(object log)
        {
            if (!start)
            {
                start = true;
                _logArq = new Logic.LogArq();
            }

            _logArq.GravaLog(log);
        }

        public static void LogException(Exception ex)
        => Logic.LogArq.LogException(ex, GetCabecalhoLocal());


    }
}
