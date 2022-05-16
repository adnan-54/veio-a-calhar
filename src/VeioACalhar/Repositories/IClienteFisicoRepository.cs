﻿using VeioACalhar.Models;

namespace VeioACalhar.Repositories;

public interface IClienteFisicoRepository
{
    ClienteFisico Create(ClienteFisico cliente);

    ClienteFisico Get(int id);

    IEnumerable<ClienteFisico> GetAll();

    ClienteFisico Update(ClienteFisico cliente);

    void Delete(ClienteFisico cliente);
}