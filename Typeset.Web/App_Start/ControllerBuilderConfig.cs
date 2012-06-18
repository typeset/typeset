using System.Web.Mvc;

namespace Typeset.Web
{
    public class ControllerBuilderConfig
    {
        public static void RegisterControllerFactory(IControllerFactory controllerFactory)
        {
            ControllerBuilder.Current.SetControllerFactory(controllerFactory);
        }
    }
}