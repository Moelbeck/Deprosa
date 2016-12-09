using deprosa.Common;
using deprosa.Model;
using System;

namespace deprosa.Service
{
    public class SubscriptionService
    {
        public DateTime CalculateExpirationDate(eSubscription subcategory)
        {
            DateTime expirationDate = DateTime.Now;
            switch (subcategory)
            {
                case eSubscription.None:
                    expirationDate = DateTime.Now;
                    break;
                case eSubscription.Basic:
                case eSubscription.Medium:
                case eSubscription.Premium:
                    expirationDate = DateTime.Now.AddDays(7);
                    break;  
            }
            return expirationDate;
        }
        /// <summary>
        /// Works only for sales listing for now
        /// </summary>
        /// <param name="subcategory"></param>
        /// <returns></returns>
        public Subscription CreateSubscription(eSubscription subcategory)
        {
            Subscription newsub = new Subscription
            {
                ExpirationDate = CalculateExpirationDate(subcategory),
                SubscriptionType = eSubscriptionType.SaleListing,
                SubscriptionCategory = subcategory
            };
            return newsub;
        }
    }
}
