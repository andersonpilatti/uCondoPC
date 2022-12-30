namespace Core.DTOs.Request;

public class PlanoContaAddRequestDTO
{
    public string? CodigoContaPai { get; set; }
    public string CodigoConta { get; set; } = null!;
    public string Nome { get; set; } = null!;
    public string Tipo { get; set; } = null!;
    public bool InAceitaLancamento { get; set; }
}
