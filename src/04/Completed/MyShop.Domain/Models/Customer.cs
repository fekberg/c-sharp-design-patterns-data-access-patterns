using MyShop.Domain.Services;
using MyShop.Domain.ValueHolders;
using System;

namespace MyShop.Domain.Models
{
    public class Customer
    {
        public Guid CustomerId { get; set; }

        // Virtual because a proxy can now override its behaviour
        public virtual string Name { get; set; }
        public virtual string ShippingAddress { get; set; }
        public virtual string City { get; set; }
        public virtual string PostalCode { get; set; }
        public virtual string Country { get; set; }

        // Lazy Loading: Virtual Proxy
        public virtual byte[] ProfilePicture { get; set; }

        // Lazy Loading: Lazy Initialization
        private byte[] profilePicture;
        public virtual byte[] ProfilePicture2
        {
            get
            {
                if (profilePicture == null)
                {
                    profilePicture = ProfilePictureService.GetFor(Name);
                }

                return profilePicture;
            }
            set
            {
                profilePicture = value;
            }
        }

        // Lazy Loading: Value Holder
        public IValueHolder<byte[]> ProfilePictureValueHolder { get; set; }
        public Lazy<byte[]> ProfilePictureValueHolder2 { get; set; }
        public byte[] ProfilePicture3
        {
            get
            {
                return ProfilePictureValueHolder2.Value;
            }
        }

        public byte[] ProfilePicture4
        {
            get
            {
                return ProfilePictureValueHolder.GetValue(Name);
            }
        }

        public Customer()
        {
            CustomerId = Guid.NewGuid();
        }
    }
}
