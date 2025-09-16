using CoreAuth.Models.VM_Model;
using CoreAuth.Repository.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoreAuth.Controllers
{
    public class PageController(IPageController page, Icrud cd) : Controller
    {
        [Route("/page/")]
        public IActionResult PageCrud()
        {
            GetDropDownList();
            GetDataForView();
            return View();
        }

        private void GetDataForView()
        {
            ViewBag.Data = page.Get_RolePermissions();
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


        [HttpPost]
        public JsonResult Delete(string TableName,string ColName,string id)
        {
            try
            {
                bool status =cd.Delete(TableName, ColName,id);

                return Json(new { info = status });
            }
            catch (Exception ex)
            {
                return Json(new { info = false, message = ex.Message });
            }
        }

    }
}
