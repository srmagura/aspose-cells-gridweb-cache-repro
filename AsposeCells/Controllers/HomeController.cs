using Aspose.Cells.GridWeb;
using Aspose.Cells.GridWeb.Data;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Reflection;

namespace AsposeCells.Controllers
{
    public class HomeController : Controller
    {
        private GridWeb GetGridWeb()
        {
            var gridWeb = new GridWeb
            {
                ID = "gid",
            };
            gridWeb.SetSession(HttpContext.Session);
            gridWeb.ResourceFilePath = "/acw_client/";
            gridWeb.EditMode = false;

            var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("AsposeCells.image.xlsx");
            gridWeb.ImportExcelFile(stream);

            gridWeb.Width = Unit.Percentage(100);
            gridWeb.Height = Unit.Pixel(800);

            return gridWeb;
        }

        public IActionResult Index()
        {
            var gridWeb = GetGridWeb();
            return View("Index", gridWeb);
        }
    }
}
