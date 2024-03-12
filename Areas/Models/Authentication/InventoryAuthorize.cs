using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BookStore.Areas.Models.Authentication
{
    public class InventoryAuthorize : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var SessionRole = context.HttpContext.Session.GetString("Role");
            // Check Session
            if (SessionRole != null)
            {
                #region Check CustomeType ~ Role            
                if (SessionRole == "Inventory")
                {
                    return;
                }
                else
                {
                    context.Result = new RedirectToRouteResult(
                        new RouteValueDictionary
                        {
                            {"Areas","System"},
                            {"Controller", "Login"},
                            {"Action", "Index"}
                        });
                }
                #endregion
            }
            else
            {
                context.Result = new RedirectToRouteResult(
                    new RouteValueDictionary
                    {
                        {"Areas","System"},
                            {"Controller", "Login"},
                            {"Action", "Index"}
                    });
            }


        }
    }
}
