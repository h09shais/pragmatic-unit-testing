using System;
using System.Collections.Generic;
using Shopping.Core.Models;

namespace Shopping.Core.Repositories
{
    public class BlockListRepository
    {
        public static Func<IEnumerable<User>> Users { get; set; }
        public static Func<IEnumerable<Receiver>> Receivers { get; set; }
        public static Func<IEnumerable<string>> Words { get; set; }
    }
}
