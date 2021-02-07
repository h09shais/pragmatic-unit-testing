using System.Collections.Generic;

namespace Shopping.Core.Requests
{
    public class BuyRequest
    {
        public IEnumerable<int> ItemIds { get; set; }

        public string PromoCode { get; set; }
    }
}