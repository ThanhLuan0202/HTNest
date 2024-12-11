using HTNest.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTNest.Data.Interfaces
{
    public interface IStyleRepository
    {
        Task<IEnumerable<Style>> GetAllAsync(string? style);
        Task<Style> GetById(int id);
        Task<Style> Add(Style style);
        Task<Style> Delete(int id);
        Task<Style> Update(Style? style, int id);
    }
}
