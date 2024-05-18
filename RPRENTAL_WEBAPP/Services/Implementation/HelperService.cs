
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using RPRENTAL_WEBAPP.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPRENTAL_WEBAPP.Services.Implementation
{
    public class HelperService : IHelperService
    {
        public string ViewToString(ControllerContext controllerContext, PartialViewResult pvr, ICompositeViewEngine _viewEngine)
        {
            using (var writer = new StringWriter())
            {
                ViewEngineResult viewResult = _viewEngine.FindView(controllerContext, pvr.ViewName!, false);

                ViewContext viewContext = new ViewContext(
                    controllerContext,
                    viewResult.View!,
                    pvr.ViewData,
                    pvr.TempData,
                    writer,
                    new HtmlHelperOptions()
                );

                viewResult.View!.RenderAsync(viewContext);

                return writer.GetStringBuilder().ToString();
            }
        }
    }
}
