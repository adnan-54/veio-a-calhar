using VeioACalhar.Models;

namespace VeioACalhar.Repositories;

public interface IStatusTransacaoRepository
{
    StatusTransacao Create(StatusTransacao statusTransacao);

    StatusTransacao Get(int id);

    IReadOnlyCollection<StatusTransacao> GetAll();

    StatusTransacao Update(StatusTransacao statusTransacao);

    void Delete(StatusTransacao statusTransacao);
}