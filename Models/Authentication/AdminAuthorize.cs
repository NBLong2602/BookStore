using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BookStore.Models.Authentication
{
    public class AdminAuthorize : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var SessionRole = context.HttpContext.Session.GetString("AdRole");
            // Check Session
            if (SessionRole != null)
            {
                #region Check CustomeType ~ Role            
                if (SessionRole == "Admin")
                {
                    return;
                }
                else
                {
                    context.Result = new RedirectToRouteResult(
                        new RouteValueDictionary
                        {
                            {"Controller", "Home"},
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
                        {"Controller", "Account"},
                        {"Action", "Login"}
                    });
            }


        }
    }
}
