﻿using System;
using System.Collections.Generic;

namespace ITERS_Tutorio_v1.Models
{
    public class TbSubscriptions
    {
        public Guid UniqueId { get; set; }
        public string Email { get; set; }
        public string Id { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public string Salutation { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? Birthdate { get; set; }
        public string WeChatId { get; set; }
        public string PhoneNumber { get; set; }
        public string MobileNumber { get; set; }
        public string FaxNumber { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostCode { get; set; }
        public string CountryCode { get; set; }
        public string GroupId { get; set; }
        public int SubscriptionStatusId { get; set; }
        public bool WantsNewsletter { get; set; }
        public DateTime JoinDate { get; set; }
        public string IpAddress { get; set; }
        public string ActivationKey { get; set; }
        public string PasswordResetKey { get; set; }
        public DateTime? UpdatedTime { get; set; }
        public string Notes { get; set; }

        public virtual TbSubscriptionStatuses SubscriptionStatus { get; set; }
    }
}
