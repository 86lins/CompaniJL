namespace AppArqTXTJL.Logic
{
    using System;
    using System.IO;
    using static AppUtilJL.Logic.Util;
    public class ArqTXT
    {
        public string EnderecoArq { get; private set; }
        public string NomeArq { get; private set; }
        public string Extensao { get; private set; }
        public string EnderecoCompletoArq { get { return $"{this.EnderecoArq}\\{this.NomeArq}.{this.Extensao}"; } }

        public ArqTXT(string endereco = "C:\\Temp", string nome = "Teste", string extensao = "txt")
        {
            this.EnderecoArq = endereco;
            this.NomeArq = nome;
            this.Extensao = extensao;

            this.ValidarNome();

            CriarPasta(this.EnderecoArq);

            CriarArquivo(this.EnderecoCompletoArq);
        }

        private void ValidarNome()
        {
            string aux = EnderecoCompletoArq.Replace("\\", "").Replace(":", "").Replace(".", "").Replace("_", "");

            for (int i = 0; i < aux.Length; i++)
            {
                string item = aux.Substring(i, 1);

                if (ComAcentos.Contains(item) || CaracteresEspeciais.Contains(item))
                    throw new Exception("Caracteres Invalidos para endereço do arquivo.");
            }
        }

        public void EscreverLinha(object texto)
        {
            lock (this)
            {
                try
                {
                    using (TextWriter textWriter = File.AppendText(this.EnderecoCompletoArq))
                    {
                        textWriter.WriteLine(texto);
                        textWriter.Close();
                        textWriter.Dispose();
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Erro ao escrever no arquivo. Erro: " + ex.Message, ex);
                }
            }
        }
    }
}
