namespace BethanysPieShop.Models
{
    public interface IPieRepository
    {
        IEnumerable<Pie> GetAll();
        IEnumerable<Pie> GetPiesOfTheWeek();
        IEnumerable<Pie> GetByCategory(int categoryId);
        IEnumerable<Pie> SearchPies(string searchQuery);

        Pie? GetById(int id);

        void CreatePie(Pie pie);

        void UpdatePie(Pie pie);
    }
}
