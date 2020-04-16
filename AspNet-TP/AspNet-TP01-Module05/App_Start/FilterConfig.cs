using System.Web;
using System.Web.Mvc;

namespace AspNet_TP01_Module05
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
