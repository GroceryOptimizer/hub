﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Core.Entities;

namespace Core.Repositories
{
    public interface IProductRepository
    {
        Task<List<Product>> GetProductsByNamesAsync(List<string> names);
    }
}
