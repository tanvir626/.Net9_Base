using CoreAuth.Models.DropDown;
using CoreAuth.Models.Table;
using CoreAuth.Models.VM_Model;
using CoreAuth.Repository.Interface;
using Dapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace CoreAuth.Repository.Implementation
{
    public class Im_PageController(IConfiguration con): IPageController
    {
        readonly private string con = con.GetConnectionString("DefaultConnection");

        public List<SelectListItem> Get_DDPage_Role()
        {
            using (var connection = new SqlConnection(con))
            {
                string sql = @"SELECT Id, Name FROM AspNetRoles";

                var roles = connection.Query<DD_Page_Role>(sql).ToList();

                // Map to SelectListItem inside the method
                var selectList = roles.Select(r => new SelectListItem
                {
                    Value = r.Id.ToString(),
                    Text = r.Name
                }).ToList();

                return selectList;
            }
        }

        public List<SelectListItem> Get_DDPage_Action()
        {
            using (var connection = new SqlConnection(con))
            {
                string sql = @"SELECT Id, SubCateName FROM SubCategories";

                var Methods = connection.Query<DD_Page_Method>(sql).ToList();

                // Map to SelectListItem inside the method
                var selectList = Methods.Select(r => new SelectListItem
                {
                    Value = r.Id.ToString(),
                    Text = r.SubCateName
                }).ToList();

                return selectList;
            }
        }

        public List<SelectListItem> Get_DDPage_Controller()
        {
            using (var connection = new SqlConnection(con))
            {
                string sql = @"SELECT Id, CateName FROM Categories";

                var Methods = connection.Query<DD_Page_Controller>(sql).ToList();

                // Map to SelectListItem inside the method
                var selectList = Methods.Select(r => new SelectListItem
                {
                    Value = r.Id.ToString(),
                    Text = r.CateName
                }).ToList();

                return selectList;
            }
        }

        public bool Save_to_RolePermission(VM_PageCrud model)
        {
            try
            {
                    using (var connection = new SqlConnection(con))
                    {
                    string sql = @"
                                INSERT INTO [CoreDB].[dbo].[RolePermissions]
                                (
                                    RoleId,
                                    SubCategoryId,
                                    CanView,
                                    CanCreate,
                                    CanEdit,
                                    CanDelete,
                                    CreateDate,
                                    UpdateDate,
                                    CategoriesID
                                )
                                SELECT 
                                    @RoleId,
                                    @SubCategoryId,
                                    @CanView,
                                    @CanCreate,
                                    @CanEdit,
                                    @CanDelete,
                                    @CreateDate,
                                    @UpdateDate,
                                    @CategoriesID
                                WHERE NOT EXISTS (
                                    SELECT 1
                                    FROM [CoreDB].[dbo].[RolePermissions]
                                    WHERE RoleId = @RoleId
                                      AND SubCategoryId = @SubCategoryId
                                      AND CategoriesID = @CategoriesID
                                );
                            ";

                        // Dapper parameter binding
                        var rowsAffected = connection.Execute(sql, new
                        {
                            model.RoleId,
                            model.SubCategoryId,
                            model.CanView,
                            model.CanCreate,
                            model.CanEdit,
                            model.CanDelete,
                            CreateDate = DateTime.Now,
                            UpdateDate = DateTime.Now,
                            model.CategoriesID
                        });
                        if (rowsAffected > 0)
                            return true;
                        return false;
                    }
            }
            catch (Exception ex) 
            { 
                Console.WriteLine(ex.Message);
                return false;
            }
            
        }

        public List<Table_RolePermission> Get_RolePermissions()
        {
            try
            {
                using (var connection = new SqlConnection(con))
                {
                    string sql = @"
                SELECT 
                    rp.Id AS Id,
                    r.Name AS RoleName,
                    sc.SubCateName AS Method,
                    c.CateName AS Controller,
                    CASE WHEN rp.CanView = 1 THEN 'Y' ELSE 'N' END AS CanView,
                    CASE WHEN rp.CanCreate = 1 THEN 'Y' ELSE 'N' END AS CanCreate,
                    CASE WHEN rp.CanEdit = 1 THEN 'Y' ELSE 'N' END AS CanEdit,
                    CASE WHEN rp.CanDelete = 1 THEN 'Y' ELSE 'N' END AS CanDelete,
                    rp.CreateDate,
                    rp.UpdateDate
                FROM RolePermissions rp
                INNER JOIN SubCategories sc ON sc.Id = rp.SubCategoryId
                INNER JOIN Categories c ON c.Id = rp.CategoriesID
                INNER JOIN AspNetRoles r ON r.Id = rp.RoleId";

                    var data = connection.Query<Table_RolePermission>(sql).ToList();

                    return data;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new List<Table_RolePermission>();
            }
        }

    }
}
