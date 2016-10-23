using depross.Common;
using depross.Model.Base;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace depross.Model
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
