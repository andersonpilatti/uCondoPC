namespace Core.DTOs.Request;

public class DataTableRequestDTO
{
    public int start { get; set; } = 0;
    public int length { get; set; } = 10;

    public string? search { get; set; }
}
