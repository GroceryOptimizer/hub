﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Core.Entities;

namespace Core.Repositories
{
    public interface IStoreRepository
    {
        Task<List<Store>> GetAllStoresAsync();
        Task<Store?> GetStoreByIdAsync(int id);
    }
}
