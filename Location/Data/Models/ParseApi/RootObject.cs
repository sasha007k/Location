using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Models.ParseApi
{
    public class RootObject
    {
        public Result[] results { get; set; }
        public string status { get; set; }
    }
}
