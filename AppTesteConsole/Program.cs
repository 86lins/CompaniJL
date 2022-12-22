namespace AppTesteConsole
{
    using AppLogCOGESP.Logic.Static;    
    using System;

    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                for (int i = 0; i < (100 * 100); i++)
                {
                    Teste(i);

                    //log.GravaLog($"Teste - {i}");

                    //if (i == 1000)
                    //{
                    //    throw new Exception("Erro teste");
                    //}
                }
            }
            catch (Exception ex)
            {
                LogArq.LogException(ex);
            }
            Environment.Exit(1);
        }

        private static void Teste(int i)
        {
            LogArq.GravaLog($"Teste - {i}");

            if (i == 1000)
            {
                throw new Exception("Erro teste");
            }
        }
    }
}
