using BookStore.Repository;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Areas.Staff.Components
{
    public class DateListFilterViewComponent : ViewComponent
    {
        private readonly IOrderInfoRepository _OrderRepos;
        public DateListFilterViewComponent(IOrderInfoRepository OrderRepos)
        {
            _OrderRepos = OrderRepos;
        }
        public IViewComponentResult Invoke()
        {
            var orderinfo = _OrderRepos.GetAllOrder().OrderBy(x => x.OrderDate);
            return View(orderinfo);
        }
    }
}
