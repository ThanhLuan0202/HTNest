using HTNest.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTNest.Service.Interfaces
{
    public interface IStyleService
    {
        Task<IEnumerable<Style>> GetAllAsync(string? styleName);
        Task<Style> Add(Style style);
        Task<Style> Delete(int  id);
        Task<Style> GetById(int id);
        Task<Style> Update(Style? style, int id);
    }
}
