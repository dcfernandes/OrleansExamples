namespace OrleansSimpleExample.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Orleans;
    using System.Threading.Tasks;

    [Route("[controller]")]
    [ApiController]
    public class CalculadoraController : ControllerBase
    {
        private readonly IClusterClient _clusterClient;

        public CalculadoraController(IClusterClient clusterClient)
        {
            _clusterClient = clusterClient;
        }

        [HttpGet("{grainId}/soma/{numero}")]
        public async Task<IActionResult> Soma(string grainId, int numero)
        {
            var result = await _clusterClient.GetGrain<ICalculadoraGrain>(grainId).Soma(numero);

            return Ok(result);
        }

        [HttpGet("{grainId}/subtracao/{numero}")]
        public async Task<IActionResult> Subtracao(string grainId, int numero)
        {
            var result = await _clusterClient.GetGrain<ICalculadoraGrain>(grainId).Subtracao(numero);

            return Ok(result);
        }

        [HttpGet("{grainId}/atual")]
        public async Task<IActionResult> Atual(string grainId)
        {
            var result = await _clusterClient.GetGrain<ICalculadoraGrain>(grainId).ValorAtual();

            return Ok(result);
        }

        [HttpGet("{grainId}/zerar")]
        public async Task<IActionResult> Zerar(string grainId)
        {
            await _clusterClient.GetGrain<ICalculadoraGrain>(grainId).Zerar();

            return Ok();
        }


    }
}
