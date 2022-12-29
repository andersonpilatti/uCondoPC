using Core.Base;
using System.ComponentModel.DataAnnotations;

namespace Domain.Model;

public class PlanoContaEntity
    : BaseEntity
{
    public PlanoContaEntity()
    {
        ContasFilhas = new HashSet<PlanoContaEntity>();
    }

    [MaxLength(16)]
    public string Codigo { get; set; } = null!;
    [MaxLength(64)]
    public string Nome { get; set; } = null!;
    [MaxLength(1)]
    public string Tipo { get; set; } = null!; // (R)eceita ou (D)espesa
    public bool InAceitaLancamento { get; set; } = false;

    public int? IdPai { get; set; } = null;

    public virtual PlanoContaEntity? ContaPai { get; set; }
    public virtual ICollection<PlanoContaEntity>? ContasFilhas { get; set; }
}