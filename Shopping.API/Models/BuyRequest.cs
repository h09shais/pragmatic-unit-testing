using System.Collections.Generic;

namespace Shopping.API.Models
{
    public class BuyRequest
    {
        public IEnumerable<int> ItemIDs { get; set; }

        public string PromoCode { get; set; }
    }
}