using BookStore.Repository;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Areas.Admin.Components
{
    public class CategoryListViewComponent : ViewComponent
    {
        private readonly ICategoryRepository _category;
        public CategoryListViewComponent(ICategoryRepository categoryRepository)
        {
            _category = categoryRepository;
        }
        public IViewComponentResult Invoke()
        {
            var category = _category.GetAllCategory().OrderBy(x => x.Id);
            return View(category);
        }
    }
}
