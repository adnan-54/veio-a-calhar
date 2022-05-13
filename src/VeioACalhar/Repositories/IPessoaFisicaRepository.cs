using VeioACalhar.Models;

namespace VeioACalhar.Repositories;

public interface IPessoaFisicaRepository
{
    PessoaFisica Create(PessoaFisica pessoaFisica);

    PessoaFisica Get(int id);

    IEnumerable<PessoaFisica> Get();

    void Update(PessoaFisica pessoaFisica);

    void Delete(PessoaFisica pessoaFisica);
}