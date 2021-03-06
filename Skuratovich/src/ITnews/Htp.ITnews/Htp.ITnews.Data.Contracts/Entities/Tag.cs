﻿using System;
using System.Collections.Generic;

namespace Htp.ITnews.Data.Contracts.Entities
{
    public class Tag : IEntity
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public ICollection<NewsTag> NewsTags { get; set; }
    }
}
