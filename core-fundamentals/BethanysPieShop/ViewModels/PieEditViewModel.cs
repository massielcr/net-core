using BethanysPieShop.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BethanysPieShop.ViewModels
{
    public class PieEditViewModel
    {
        public required Pie Pie { get; set; }
        public required List<SelectListItem> Categories { get; set; }
        public int CategoryId { get; set; }
    }
}
