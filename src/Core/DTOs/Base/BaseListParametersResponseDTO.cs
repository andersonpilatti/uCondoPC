namespace Core.DTOs.Base;

public class BaseListParametersResponseDTO<TListResult>
        : BaseListParametersDTO
{
    public BaseListParametersResponseDTO()
    {
        data = new List<TListResult>();
    }

    public IEnumerable<TListResult> data { get; set; } = null!;
}

