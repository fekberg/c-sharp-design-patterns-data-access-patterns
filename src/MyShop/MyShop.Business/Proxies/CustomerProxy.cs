using MyShop.Business.Models;
using MyShop.Business.Services;

namespace MyShop.Business.Proxies
{
    // Lazy Loading: Virtual Proxy
    public class CustomerProxy : Customer
    {
        public override byte[] ProfilePicture
        {
            get
            {
                if (base.ProfilePicture == null)
                {
                    // Could be injected as an interface
                    base.ProfilePicture = new ProfilePictureService().GetFor(Name);
                }

                return base.ProfilePicture;
            }
        }
    }
}
