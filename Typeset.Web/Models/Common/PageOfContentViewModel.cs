using Typeset.Web.Models.Configuration;

namespace Typeset.Web.Models.Common
{
    public class PageOfContentViewModel
    {
        public ConfigurationViewModel Configuration { get; set; }
        public LayoutViewModel Layout { get; set; }
        public ContentViewModel FrontMatterContent { get; set; }

        public PageOfContentViewModel(ConfigurationViewModel configViewModel, LayoutViewModel layoutViewModel, ContentViewModel frontMatterContentViewModel)
        {
            Configuration = configViewModel;
            Layout = layoutViewModel;
            FrontMatterContent = frontMatterContentViewModel;
        }
    }
}