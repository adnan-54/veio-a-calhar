using VeioACalhar.Models;

namespace VeioACalhar.Repositories;

public interface IPessoaFisicaRepository
{
    PessoaFisica Create(PessoaFisica pessoaFisica);

    PessoaFisica Get(int id);

    IEnumerable<PessoaFisica> Get();

    PessoaFisica Update(PessoaFisica pessoaFisica);

    void Delete(PessoaFisica pessoaFisica);
}