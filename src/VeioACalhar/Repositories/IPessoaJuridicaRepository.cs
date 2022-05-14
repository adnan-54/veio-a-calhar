using VeioACalhar.Models;

namespace VeioACalhar.Repositories;

public interface IPessoaJuridicaRepository
{
    PessoaJuridica Create(PessoaJuridica pessoaJuridica);

    PessoaJuridica Get(int id);

    IEnumerable<PessoaJuridica> Get();

    PessoaJuridica Update(PessoaJuridica pessoaJuridica);

    void Delete(PessoaJuridica pessoaJuridica);
}