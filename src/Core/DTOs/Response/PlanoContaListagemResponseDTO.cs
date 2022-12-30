using Core.DTOs.Request;

namespace Core.DTOs.Response;

public class PlanoContaListagemResponseDTO
{

    public IEnumerable<PlanoContaAddRequestDTO> PlanoConta { get; set; }
}
