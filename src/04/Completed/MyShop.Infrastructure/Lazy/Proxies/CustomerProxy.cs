using MyShop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyShop.Infrastructure.Lazy.Proxies
{
    public class CustomerProxy : Customer
    {
        public override byte[] ProfilePicture
        {
            get
            {
                if (base.ProfilePicture == null)
                {
                    base.ProfilePicture = ProfilePictureService.GetFor(Name);
                }

                return base.ProfilePicture;
            }
        }
    }
}
