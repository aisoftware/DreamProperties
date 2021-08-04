using System;
using System.Collections.Generic;
using System.Text;

namespace DreamProperties.Common.Models
{
    public enum SearchType
    {
        City,
        PropertyType
    }

    public class SearchQuery
    {
        public SearchType SearchType { get; set; }
        public string Term { get; set; }
    }
}
