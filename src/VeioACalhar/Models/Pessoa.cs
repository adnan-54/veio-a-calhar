using System.ComponentModel.DataAnnotations;

namespace VeioACalhar.Models;

public abstract record Pessoa : Entidade
{
    public Pessoa()
    {
        Nome = string.Empty;
        Observacoes = string.Empty;
        Pix = string.Empty;
        Email = string.Empty;
        Telefone = string.Empty;
        Endereco = string.Empty;
    }

    [StringLength(64, MinimumLength = 3, ErrorMessage = "O nome precisa ter entre 3 e 64 caracteres")]
    [Display(Name = "Nome")]
    public string Nome { get; init; }

    [DataType(DataType.MultilineText)]
    [Display(Name = "Observações")]
    public string? Observacoes { get; init; }

    [StringLength(64, ErrorMessage = "O PIX pode ter no maximo 64 caracteres")]
    [Display(Name = "PIX")]
    public string? Pix { get; init; }

    [DataType(DataType.EmailAddress)]
    [StringLength(64, ErrorMessage = "O email pode ter no maximo 64 caracteres")]
    [Display(Name = "Email")]
    public string? Email { get; init; }

    [DataType(DataType.PhoneNumber)]
    [StringLength(64, ErrorMessage = "O telefone pode ter no maximo 64 caracteres")]
    [Display(Name = "Telefone")]
    public string? Telefone { get; init; }

    [StringLength(128, ErrorMessage = "O endereço pode ter no maximo 128 caracteres")]
    [Display(Name = "Endereço")]
    public string? Endereco { get; init; }
}
