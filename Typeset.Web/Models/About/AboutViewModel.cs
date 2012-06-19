using System.Collections.Generic;
using Typeset.Domain.About;

namespace Typeset.Web.Models.About
{
    public class AboutViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Bio { get; set; }
        public string Email { get; set; }
        public string TwitterUsername { get; set; }
        public string GithubUsername { get; set; }
        public IDictionary<string, string> Links { get; set; }

        public AboutViewModel(IAbout entity)
        {
            FirstName = entity.FirstName;
            LastName = entity.LastName;
            Bio = entity.Bio;
            Email = entity.Email;
            TwitterUsername = entity.TwitterUsername;
            GithubUsername = entity.GithubUsername;
            Links = entity.Links;
        }
    }
}