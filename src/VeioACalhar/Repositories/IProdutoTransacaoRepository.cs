using VeioACalhar.Models;

namespace VeioACalhar.Repositories;

public interface IProdutoTransacaoRepository
{
    IReadOnlyCollection<ProdutoTransacao> CreateFor(Transacao transacao);

    IReadOnlyCollection<ProdutoTransacao> GetFor(Transacao transacao);

    IReadOnlyCollection<ProdutoTransacao> UpdateFor(Transacao transacao);

    void DeleteFor(Transacao transacao);
}