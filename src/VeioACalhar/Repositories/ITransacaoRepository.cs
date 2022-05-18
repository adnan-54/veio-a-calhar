using VeioACalhar.Models;

namespace VeioACalhar.Repositories;

public interface ITransacaoRepository<TTransacao> where TTransacao : Transacao, new()
{
    TTransacao Create(TTransacao transacao);

    TTransacao Get(int id);

    IReadOnlyCollection<TTransacao> GetAll();

    TTransacao Update(TTransacao transacao);

    void Delete(TTransacao transacao);
}