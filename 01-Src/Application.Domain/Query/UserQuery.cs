﻿using System.ComponentModel.DataAnnotations;

namespace Applications.Domains.Query
{
    public class UserQuery
    {
        public string Name { get; set; }
        public int PageIndex { get; set; } = 1;

        public int PageSize { get; set; } = 20;
        public int? Age { get; set; }
    }
}