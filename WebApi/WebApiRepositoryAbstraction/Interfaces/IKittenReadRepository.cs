﻿using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApiRepositoryAbstraction.Interfaces
{
    public interface IKittenReadRepository<T> where T : class
    {
        Task<IReadOnlyList<T>> ReadByParameterAsync(
            string name, 
            int page, 
            int size);
    }
}