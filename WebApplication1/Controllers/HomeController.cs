using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private static List<DisAModel> Names;

        [HttpGet]
        public ActionResult Index()
        {
            var model = new DisAModel();
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Index(DisAModel model)
        {
            await Task.Delay(300);

            if (ModelState.IsValid)
            {
                if (Names == null)
                {
                    Names = new List<DisAModel>();
                }

                if (Names.Any(n => model.Name == n.Name))
                {
                    TempData["Error"] = true;
                    TempData["Reason"] = "Duplicate Name";
                    return View(model);
                }

                TempData["Success"] = true;
                TempData["Name"] = model.Name;
                Names.Add(model);
                return RedirectToAction("Summary");
            }

            TempData["Error"] = true;
            TempData["Reason"] = "Model was invalid";

            return View(model);
        }

        [HttpPost]
        public ActionResult ShouldWeDoTheDo(DisAModel model)
        {
            return Json(new { doTheDo = true });
        } 

        [HttpGet]
        public ActionResult Summary()
        {
            if(Names == null)
            {
                return RedirectToAction("Index");
            }
            return View(Names);
        }
    }

    public class DisAModel
    {
        [Required]
        [DataType(DataType.Text)]
        [MaxLength(25)]
        public string Name { get; set; }
    }
}