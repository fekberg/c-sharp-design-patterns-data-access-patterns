using MyShop.Business.Services;
using MyShop.Business.ValueHolders;
using System;

namespace MyShop.Business.Models
{
    public class Customer
    {
        public Guid CustomerId { get; set; }

        public virtual string Name { get; set; }
        public virtual string ShippingAddress { get; set; }
        public virtual string City { get; set; }
        public virtual string PostalCode { get; set; }
        public virtual string Country { get; set; }

        // Lazy Loading: Virtual Proxy
        public virtual byte[] ProfilePicture { get; set; }

        // Lazy Loading: Lazy Initialization
        private Lazy<byte[]> profilePicture;
        public byte[] ProfilePictureLazy
        {
            get
            {
                return profilePicture.Value;
            }
        }

        // Lazy Loading: Value Holder
        private IValueHolder<byte[]> profilePictureValueHolder;
        public byte[] ProfilePictureValueHolder
        {
            get
            {
                return profilePictureValueHolder.GetValue(Name);
            }
        }

        public Customer()
        {
            CustomerId = Guid.NewGuid();

            // Lazy Loading: Value Holder
            profilePictureValueHolder = new ProfilePictureValueHolder(new ProfilePictureService());

            // Lazy Loading: Lazy Initialization
            profilePicture = new Lazy<byte[]>(() =>
            {
                // Could be injected as an interface
                return new ProfilePictureService().GetFor(Name);
            });
        }
    }
}
