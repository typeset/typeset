using System.Collections.Generic;
using System.Text;
using Typeset.Domain.About;

namespace Typeset.Web.Models.About
{
    public class AboutViewModel
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }

        public AboutViewModel(IAbout entity)
        {
            FirstName = entity.FirstName;
            LastName = entity.LastName;
        }

        public string FullName()
        {
            var stringBuilder = new StringBuilder();
            if (!string.IsNullOrWhiteSpace(FirstName))
            {
                stringBuilder.Append(FirstName);
            }
            if (!string.IsNullOrWhiteSpace(FirstName) && !string.IsNullOrWhiteSpace(LastName))
            {
                stringBuilder.Append(" ");
            }
            if (!string.IsNullOrWhiteSpace(LastName))
            {
                stringBuilder.Append(LastName);
            }
            return stringBuilder.ToString();
        }
    }
}