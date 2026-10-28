using System.Web;
using System.Web.Mvc;

namespace CLIWEB_CONUNI_RESTFULDOTNET_GR06
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
