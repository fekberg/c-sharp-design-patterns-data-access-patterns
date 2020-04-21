using MyShop.Domain.Models;
using MyShop.Infrastructure.Lazy.Proxies;
using MyShop.Infrastructure.Services;
using MyShop.Infrastructure.ValueHolders;
using System;

namespace MyShop.Infrastructure.Lazy.Ghosts
{
    public class GhostCustomer : CustomerProxy
    {
        private LoadStatus status;
        private readonly Func<Customer> load;

        public bool IsGhost => status == LoadStatus.GHOST;
        public bool IsLoaded => status == LoadStatus.LOADED;

        public override string Name 
        {
            get
            {
                Load();

                return base.Name;
            }
            set
            {
                Load();

                base.Name = value;
            }
        }

        public override string ShippingAddress
        {
            get
            {
                Load();

                return base.ShippingAddress;
            }
            set
            {
                Load();

                base.ShippingAddress = value;
            }
        }

        public override string City
        {
            get
            {
                Load();

                return base.City;
            }
            set
            {
                Load();

                base.City = value;
            }
        }

        public override string PostalCode
        {
            get
            {
                Load();

                return base.PostalCode;
            }
            set
            {
                Load();

                base.PostalCode = value;
            }
        }

        public override string Country
        {
            get
            {
                Load();

                return base.Country;
            }
            set
            {
                Load();

                base.Country = value;
            }
        }

        public void Load()
        {
            if (IsGhost)
            {
                status = LoadStatus.LOADING;

                var customer = load();
                base.Name = customer.Name;
                base.ShippingAddress = customer.ShippingAddress;
                base.City = customer.City;
                base.PostalCode = customer.PostalCode;
                base.Country = customer.Country;

                ProfilePictureValueHolder = new ProfilePictureValueHolder();
                ProfilePictureLazy = new Lazy<byte[]>(() =>
                {
                    return ProfilePictureService.GetFor(customer.Name);
                });

                status = LoadStatus.LOADED;
            }
        }

        public GhostCustomer(Func<Customer> load) : base()
        {
            status = LoadStatus.GHOST;
            this.load = load;
        }
    }

    enum LoadStatus { GHOST, LOADING, LOADED };
}
