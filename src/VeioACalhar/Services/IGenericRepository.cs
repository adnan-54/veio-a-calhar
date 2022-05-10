using VeioACalhar.Models;

namespace VeioACalhar.Services;

public interface IGenericRepository<TModel> where TModel : Entidade, new()
{
    TModel Create(TModel model);

    TModel Get(int id);

    IEnumerable<TModel> Get();

    void Update(TModel model);

    void Delete(TModel model);
}