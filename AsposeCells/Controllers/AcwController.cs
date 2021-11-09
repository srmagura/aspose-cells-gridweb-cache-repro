using Microsoft.AspNetCore.Mvc;

namespace Website.Controllers
{
    public class AcwController : Controller
    {
        public IActionResult Operation(string type, string id)
        {
            return Aspose.Cells.GridWeb.AcwController.DoAcwAction(this, type, id);
        }
    }
}
