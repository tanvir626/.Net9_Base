using CoreAuth.Models.DropDown;
using CoreAuth.Models.VM_Model;
using CoreAuth.Models.Table;
using Microsoft.AspNetCore.Mvc.Rendering;
namespace CoreAuth.Repository.Interface
{
    public interface IPageController
    {
        public List<SelectListItem> Get_DDPage_Role();
        public List<SelectListItem> Get_DDPage_Action();
        public List<SelectListItem> Get_DDPage_Controller();
        public bool Save_to_RolePermission(VM_PageCrud model);
        public bool Update_to_RolePermission(VM_PageCrud model);
        public List<Table_RolePermission> Get_RolePermissions();

        public string RoleNameByID(string ID);
        public string PageLinkById(string ID);  
    }
}
