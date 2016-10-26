using deprosa.Common;
using deprosa.Model.Base;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace deprosa.Model
{
    public class Subscription : Entity
    {
        public double Price { get; set; }

        public eSubscription SubscriptionCategory { get; set; }

        public eSubscriptionType SubscriptionType { get; set; }


        [Column(TypeName = "DateTime2")]
        public DateTime ExpirationDate
        {
            get; set;
        }
    }
}
