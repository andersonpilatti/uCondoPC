namespace Core.DTOs.Response;

public class NotificacaoResponseDTO
{
    public string Tipo { get; set; } = "E"; // (E)rro
    public int? Codigo { get; set; }
    public string Mensagem { get; set; } = null!;
}
