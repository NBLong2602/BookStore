using BookStore.Repository;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.ViewComponents
{
    public class CategoryMenuViewComponent : ViewComponent
    {
        private readonly ICategoryRepository _category;
        public CategoryMenuViewComponent(ICategoryRepository categoryRepository)
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
