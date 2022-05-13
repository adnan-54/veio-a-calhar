using VeioACalhar.Models;

namespace VeioACalhar.Repositories;

public interface IPessoaJuridicaRepository
{
    PessoaJuridica Create(PessoaJuridica pessoaJuridica);

    PessoaJuridica? Get(int id);

    IEnumerable<PessoaJuridica> Get();

    void Update(PessoaJuridica pessoaJuridica);

    void Delete(PessoaJuridica pessoaJuridica);
}