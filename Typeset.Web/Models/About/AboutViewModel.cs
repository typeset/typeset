using System.Collections.Generic;
using System.Text;
using Typeset.Domain.About;

namespace Typeset.Web.Models.About
{
    public class AboutViewModel
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Bio { get; private set; }
        public string Email { get; private set; }
        public IDictionary<string, string> Usernames { get; private set; }
        public IDictionary<string, string> Links { get; private set; }

        public AboutViewModel(IAbout entity)
        {
            FirstName = entity.FirstName;
            LastName = entity.LastName;
            Bio = entity.Bio;
            Email = entity.Email;
            Usernames = entity.Usernames;
            Links = entity.Links;
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