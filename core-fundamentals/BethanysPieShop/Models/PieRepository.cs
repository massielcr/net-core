
using Microsoft.EntityFrameworkCore;

namespace BethanysPieShop.Models
{
    public class PieRepository(AppDbContext appDbContext) : IPieRepository
    {
        private readonly AppDbContext _appDbContext = appDbContext;        

        public IEnumerable<Pie> GetAll()
        {
            return _appDbContext.Pies.Include(p => p.Category);
        }

        public IEnumerable<Pie> GetPiesOfTheWeek()
        {
            return _appDbContext.Pies.Include(c => c.Category).Where(p => p.IsPieOfTheWeek);
        }

        public IEnumerable<Pie> GetByCategory(int categoryId)
        {
            return _appDbContext.Pies.Include(p => p.Category).Where(p => p.CategoryId == categoryId);
        }

        public IEnumerable<Pie> SearchPies(string searchQuery)
        {
            return _appDbContext.Pies.Include(p => p.Category).Where(p => p.Name.Contains(searchQuery));
        }
        

        public Pie? GetById(int id)
        {
            return _appDbContext.Pies.Include(p => p.Category).FirstOrDefault(p => p.PieId == id);
        }

        public void CreatePie(Pie pie)
        {
            _appDbContext.Pies.Add(pie);
            _appDbContext.SaveChanges();
        }

        public void UpdatePie(Pie pie)
        {
            _appDbContext.Pies.Update(pie);
            _appDbContext.SaveChanges();
        }

        public void DeletePie(int id)
        {
            var pie = GetById(id);
            if (pie == null)
            {
                _appDbContext.Pies.Remove(pie);
            }
        }
    }
}
