using System.Collections.Generic;
using Shopping.Core.Models;

namespace Shopping.Core.Repositories
{
    public interface IItemRepository
    {
        IEnumerable<Item> FindByIDs(IEnumerable<int> itemIDs);
    }
}
