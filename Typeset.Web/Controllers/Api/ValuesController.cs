﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Typeset.Domain.Post;

namespace Typeset.Web.Controllers.Api
{
    public class ValuesController : BaseApiController
    {
        private IPostRepository PostRepository { get; set; }

        public ValuesController(IPostRepository postRepository)
        {
            if (postRepository == null)
            {
                throw new ArgumentNullException("postRepository");
            }

            PostRepository = postRepository;
        }

        public IEnumerable<IPost> Get()
        {
            return new List<IPost>();
        }
    }
}