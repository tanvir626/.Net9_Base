using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;

namespace CoreAuth.Repository.Interface
{
    public interface Icrud
    {
      public bool Delete(string TableName, string ColName, string Id);
      public List<dynamic> ShowIndivisualRow(string TableName, string ColName, string Id);
    }
}
