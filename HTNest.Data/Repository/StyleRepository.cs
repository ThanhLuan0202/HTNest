using HTNest.Data.Data;
using HTNest.Data.Entities;
using HTNest.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Net.WebSockets;
using System.Runtime.InteropServices.JavaScript;

namespace HTNest.Data.Repository
{
    public class StyleRepository : Repository<Style> ,IStyleRepository
    {
       readonly HTNestDbContext _dbContext;

        public StyleRepository(HTNestDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Style>> GetAllAsync(string? style)
        {
            var query = Entities.Where(x => x.Status.Equals("Active"));
            if (!string.IsNullOrEmpty(style))
            {
                query = Entities.Where(x => x.StyleName.ToLower().Contains(style) && x.Status.Equals("Active"));
            }
            return await query.ToListAsync();
        }

        public async Task<Style> Add(Style style)
        {
            if (style == null)
            {
                throw new ArgumentNullException(nameof(style));

            }
            var query = Entities.FirstOrDefault(x => x.StyleName.ToLower().Contains(style.StyleName));
            if (query != null)
            {
                throw new Exception($"Style {style.StyleName} is existed !!");

            }
            await Entities.AddAsync(style);
            await _dbContext.SaveChangesAsync();
            return style;

        }

        public async Task<Style> Delete(int id)
        {
            var style = Entities.FirstOrDefault(x => x.StyleId == id);
            if (style == null) 
            {
                throw new ArgumentNullException(nameof(style));
            }
            style.Status = "Inactive";
            await _dbContext.SaveChangesAsync();
            return style;
        }

        public async Task<Style> GetById(int id)
        {
            var style = Entities.FirstOrDefault(x => x.StyleId == id && x.Status.ToLower().Equals("Active"));
            if (style == null)
            {
                throw new ArgumentNullException(nameof(style));

            }
            return style;
        }

        public async Task<Style> Update(Style style, int id)
        {
            var existingStyle =  await GetById(id);
            if (existingStyle == null)
            {
                throw new ArgumentNullException(nameof(style));

            }

            existingStyle.StyleName = style.StyleName;
            await _dbContext.SaveChangesAsync();
            return existingStyle;
        }
    }
}
