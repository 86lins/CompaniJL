namespace AppUtilJL.Logic
{
    using System.IO;
    using System.Net;
    using System.Reflection;

    public static class InformacoesLocais
    {
        public static string MaquinaLocalDominioUsuarioLogado { get; } = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
        public static string MaquinaLocalIPV4 { get; } = Dns.GetHostAddresses(Dns.GetHostName())[1].ToString();
        public static string MaquinaLocalHost { get; } = Dns.GetHostName().ToString();
        public static string MaquinaLocalIPV4_Host { get; } = $"{MaquinaLocalIPV4}/{MaquinaLocalHost}";

        public static string MaquinaLocalDominio { get; } = MaquinaLocalDominioUsuarioLogado.Split('\\')[0];
        public static string MaquinaLocalUsuarioLogado { get; } = MaquinaLocalDominioUsuarioLogado.Split('\\')[1];

        public static string GetNomeSistema
        {
            get
            {
                var assembly = Assembly.GetEntryAssembly();
                return assembly != null ? Path.GetFileName(assembly.Location).Replace(".exe", "") : "AppSystemLog";
            }
        }
    }
}
