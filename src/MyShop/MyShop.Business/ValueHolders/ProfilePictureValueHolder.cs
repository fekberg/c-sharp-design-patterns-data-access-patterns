using MyShop.Business.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyShop.Business.ValueHolders
{
    public class ProfilePictureValueHolder : ValueHolder<byte[]>
    {
        public ProfilePictureValueHolder(ProfilePictureService service) 
            : base(name => service.GetFor(name.ToString()))
        {
        }
    }
}
