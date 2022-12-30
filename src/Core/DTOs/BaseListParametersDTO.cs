namespace Core.DTOs;

public class BaseListParametersDTO
{
    public int length { get; set; }
    public int start { get; set; }
    public string? search { get; set; }
    public int recordsTotal { get; set; }
    public int recordsFiltered { get; set; }
}
