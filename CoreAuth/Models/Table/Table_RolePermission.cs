using System.Data;

namespace CoreAuth.Models.Table
{
    public class Table_RolePermission
    {
        public int Id { get; set; }
        public string?  RoleName { get; set; }
        public string? Method { get; set; }
        public string? Controller { get; set; }
        public char CanView { get; set; }
        public char CanCreate { get; set; }
        public char CanEdit { get; set; }
        public char CanDelete { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}
