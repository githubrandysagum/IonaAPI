using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Web.Http.Controllers;

namespace IonaAPI.API.Filters
{
    public class PageLimitValidationFilter : Attribute, IActionFilter
    {
        public bool AllowMultiple => throw new NotImplementedException();

       
        public void OnActionExecuted(ActionExecutedContext context)
        {
           

        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var query = context.HttpContext.Request.Query;
            var page = query["page"];
            var limit = query["limit"];
            
            var pageError = new BadRequestObjectResult("Error:Page value is out of range. Accepted value is 0 and Higher");
            if (int.TryParse(page, out var pageValue))
            {
                if (pageValue<0)
                {
                    context.Result = pageError;
                }
            }
            else
            {
                context.Result = pageError;
            }

            var limitError = new BadRequestObjectResult("Error:limit value is out of range. Accepted value is 1 to 100");
            if (int.TryParse(limit, out var limitValue))
            {
                if (limitValue<1 || limitValue>100)
                {
                    context.Result = limitError;
                }
            }
            else
            {
                context.Result = limitError;
            }
        }
    }
}
