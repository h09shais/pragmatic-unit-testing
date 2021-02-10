using System;
using System.Collections.Generic;
using Shopping.Core.Models;

namespace Shopping.Core.Repositories
{
    public class BlockListRepository
    {
        public Func<IEnumerable<User>> Users { get; set; }
        public Func<IEnumerable<Receiver>> Receivers { get; set; }
        public Func<IEnumerable<string>> Words { get; set; }
    }
}
