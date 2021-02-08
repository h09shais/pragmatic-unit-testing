using System;
using System.Collections.Generic;
using Shopping.Core.Models;

namespace Shopping.Core.Repositories
{
    public class BlockListRepository
    {
        public Func<List<User>> Users { get; set; }
        public Func<List<Receiver>> Receivers { get; set; }
        public Func<List<string>> Words { get; set; }
    }
}
