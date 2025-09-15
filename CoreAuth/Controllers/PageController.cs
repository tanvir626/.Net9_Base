using CoreAuth.Models.VM_Model;
using CoreAuth.Repository.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoreAuth.Controllers
{
    public class PageController(IPageController page) : Controller
    {
        [Route("/page/")]
        public IActionResult PageCrud()
        {
            GetDropDownList();
            return View();
        }

        private void GetDropDownList()
        {
            ViewBag.RoleId = page.Get_DDPage_Role();
            ViewBag.Method = page.Get_DDPage_Action();
            ViewBag.Controller = page.Get_DDPage_Controller();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(VM_PageCrud model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    bool status=page.Save_to_RolePermission(model);
                    return Json(new
                    {
                        info = status
                    });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return RedirectToAction("Error500");
        }

        [Route("/500/")]
        public IActionResult Error500()=>View();
    }
}
