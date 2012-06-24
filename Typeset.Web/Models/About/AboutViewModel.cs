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

        public bool HasUsername(string service)
        {
            return !string.IsNullOrWhiteSpace(service) && Usernames.ContainsKey(service) && !string.IsNullOrWhiteSpace(Usernames[service]);
        }

        public bool HasGithubUsername()
        {
            return HasUsername("github");
        }

        public string GithubUrl()
        {
            return HasUsername("github") ? string.Format("https://github.com/{0}", Usernames["github"]) : "https://github.com";
        }

        public bool HasTwitterUsername()
        {
            return HasUsername("twitter");
        }

        public string TwitterUrl()
        {
            return HasUsername("twitter") ? string.Format("https://twitter.com/{0}", Usernames["twitter"]) : "https://twitter.com";
        }
    }
}