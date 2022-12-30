using API.Base;
using Core.DTOs.Base;
using Core.DTOs.Request;
using Core.DTOs.Response;
using Core.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Services.Interfaces;
using System.Net;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlanoContaController
        : CustomControllerBase
    {
        private readonly IPlanoContaService _planoContaService;

        public PlanoContaController(IPlanoContaService planoContaService)
        {
            _planoContaService = planoContaService;
        }

        [HttpPost]
        [Route("/api/v1/[controller]/Inclusao")]
        [ProducesResponseType(typeof(NotificacaoResponseDTO), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(PlanoContaResponseDTO), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Inclusao([FromBody] PlanoContaAddRequestDTO param)
        {
            try
            {
                await _planoContaService.AddAsync(param);
            }
            catch (PlanoContaExeception ex)
            {
                return BadRequest(new NotificacaoResponseDTO
                {
                    Codigo = ex.ErrorCode,
                    Mensagem = ex.Message
                });
            }
            catch (Exception)
            {
                return BadRequest(new NotificacaoResponseDTO
                {
                    Mensagem = _msgErroGenerico
                });
            }

            return Ok();
        }

        [HttpDelete]
        [Route("/api/v1/[controller]/Exclusao")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(NotificacaoResponseDTO), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Exclusao(string ContaCodigo)
        {
            try
            {
                await _planoContaService.DeleteAsync(ContaCodigo);
                return Ok();
            }
            catch (PlanoContaExeception ex)
            {
                return BadRequest(new NotificacaoResponseDTO
                {
                    Codigo = ex.ErrorCode,
                    Mensagem = ex.Message
                });
            }
            catch (Exception ex)
            {                
                return BadRequest(new NotificacaoResponseDTO
                {
                    Mensagem = _msgErroGenerico
                });
            }
        }

        [HttpGet]
        [Route("/api/v1/[controller]/ListarContasPaiElegiveis")]
        [ProducesResponseType(typeof(IEnumerable<PlanoContaResponseDTO>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(NotificacaoResponseDTO), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> ListarContasPaiElegiveis()
        {
            try
            {
                var result = await _planoContaService.ListEligibleParentAccountsAsync();
                return Ok(result);
            }
            catch (Exception)
            {
                return BadRequest(new NotificacaoResponseDTO
                {
                    Mensagem = _msgErroGenerico
                });
            }
        }

        [HttpPost]
        [Route("/api/v1/[controller]/ListarContas")]
        [ProducesResponseType(typeof(BaseListParametersResponseDTO<PlanoContaResponseDTO>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(NotificacaoResponseDTO), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> ListarContas([FromBody] DataTableRequestDTO param)
        {
            try
            {
                return Ok(await _planoContaService.ListGridAsync(param));
            }
            catch (Exception)
            {
                return BadRequest(new NotificacaoResponseDTO
                {
                    Mensagem = _msgErroGenerico
                });
            }
        }

        [HttpGet]
        [Route("/api/v1/[controller]/SugestaoCodigoConta")]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> SugestaoCodigoConta(string? CodigoContaPai)
        {
            try
            {
                var result = await _planoContaService.SugestNewAccountCodeAsync(CodigoContaPai);
                return Ok(result);
            }
            catch (Exception)
            {
                return BadRequest(new NotificacaoResponseDTO
                {
                    Mensagem = _msgErroGenerico
                });
            }
        }


    }
}
