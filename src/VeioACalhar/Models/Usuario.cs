using System.ComponentModel.DataAnnotations;

namespace VeioACalhar.Models;

public record Usuario : Entidade
{
    public Usuario()
    {
        Login = string.Empty;
        Password = string.Empty;
    }

    [StringLength(50, MinimumLength = 3, ErrorMessage = "O usuário deve ter entre 3 e 50 caracteres")]
    [Display(Name = "Usuário")]
    public string Login { get; init; }

    [DataType(DataType.Password)]
    [StringLength(32, MinimumLength = 3, ErrorMessage = "A senha deve ter entre 3 e 32 caracteres")]
    [Display(Name = "Senha")]
    public string Password { get; init; }

    [DataType(DataType.Date)]
    [Display(Name = "Data de Cadastro")]
    public DateOnly DataCadastro { get; init; }

    [Display(Name = "Ativo")]
    public bool Ativo { get; init; }
}
