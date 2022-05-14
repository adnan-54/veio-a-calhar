﻿using VeioACalhar.Models;

namespace VeioACalhar.Repositories;

public interface IFuncionarioRepository
{
    Funcionario Create(Funcionario funcionario);

    Funcionario? Get(int id);

    IEnumerable<Funcionario> Get();

    Funcionario Update(Funcionario funcionario);

    void Delete(Funcionario funcionario);
}