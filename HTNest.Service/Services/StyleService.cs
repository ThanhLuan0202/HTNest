using HTNest.Data.Entities;
using HTNest.Data.Interfaces;
using HTNest.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTNest.Service.Services
{
    public class StyleService : IStyleService
    {
        public readonly IStyleRepository _styleRepository;
        public StyleService(IStyleRepository styleRepository)
        {
            _styleRepository = styleRepository;
        }

        public Task<Style> Add(Style style)
        {
            return _styleRepository.Add(style);
        }

        public Task<Style> Delete(int id)
        {
            return _styleRepository.Delete(id);
        }

        public Task<IEnumerable<Style>> GetAllAsync(string? style)
        {
            return _styleRepository.GetAllAsync(style);
        }

        public Task<Style> GetById(int id)
        {
            return (_styleRepository.GetById(id));
        }

        public Task<Style> Update(Style style, int id)
        {
            return _styleRepository.Update(style, id);
        }
    }
}
