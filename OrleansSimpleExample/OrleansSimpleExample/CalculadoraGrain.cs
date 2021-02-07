namespace OrleansSimpleExample
{
    using Orleans;
    using System;
    using System.Threading.Tasks;

    public interface ICalculadoraGrain : IGrainWithStringKey
    {
        Task<string> Soma(int numero);
        Task<string> Subtracao(int numero);
        Task<string> ValorAtual();
        Task Zerar();
    }

    public class CalculadoraGrain : Grain, ICalculadoraGrain
    {
        private const char sinalSoma = '+';
        private const char sinalSubtracao = '-';

        private int _valorAtual = 0;
        private string _extenso = "";
        public Task<string> Soma(int numero)
        {
            _valorAtual += numero;
            EscreverExtenso(sinalSoma, numero);

            return Extenso();
        }

        public Task<string> Subtracao(int numero)
        {
            _valorAtual -= numero;
            EscreverExtenso(sinalSubtracao, numero);

            return Extenso();
        }

        public Task<string> ValorAtual() => Extenso();

        public Task Zerar()
        {
            _valorAtual = 0;
            _extenso = "";

            return Task.CompletedTask;
        }

        private Task EscreverExtenso(char sinal, int numero)
        { 
            if(string.IsNullOrEmpty(_extenso))
                _extenso += $"{numero}";
            else
                _extenso += $" {sinal} {numero}";

            return Task.CompletedTask;
        }

        private Task<string> Extenso()
        {
            if(!string.IsNullOrEmpty(_extenso))
                return Task.FromResult($"Memória: {this.GetPrimaryKeyString()}{Environment.NewLine}{_extenso} = {_valorAtual}");

            return Task.FromResult($"Memória: {this.GetPrimaryKeyString()}");
        }
    }
}
