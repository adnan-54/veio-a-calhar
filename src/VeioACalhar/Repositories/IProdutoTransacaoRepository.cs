using VeioACalhar.Models;

namespace VeioACalhar.Repositories;

public interface IProdutoTransacaoRepository
{
    IReadOnlyCollection<ProdutoTransacao> CreateFrom(Transacao transacao);

    IReadOnlyCollection<ProdutoTransacao> GetFrom(Transacao transacao);

    IReadOnlyCollection<ProdutoTransacao> UpdateFrom(Transacao transacao);

    void DeleteFrom(Transacao transacao);
}