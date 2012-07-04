using Typeset.Web.Models.Configuration;

namespace Typeset.Web.Models.Common
{
    public class PageOfFrontMatterContentViewModel
    {
        public ConfigurationViewModel Configuration { get; set; }
        public LayoutViewModel Layout { get; set; }
        public FrontMatterContentViewModel FrontMatterContent { get; set; }

        public PageOfFrontMatterContentViewModel(ConfigurationViewModel configViewModel, LayoutViewModel layoutViewModel, FrontMatterContentViewModel frontMatterContentViewModel)
        {
            Configuration = configViewModel;
            Layout = layoutViewModel;
            FrontMatterContent = frontMatterContentViewModel;
        }
    }
}