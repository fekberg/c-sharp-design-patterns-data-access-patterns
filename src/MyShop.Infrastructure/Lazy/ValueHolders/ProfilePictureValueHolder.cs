using MyShop.Domain.ValueHolders;
using MyShop.Infrastructure.Services;

namespace MyShop.Infrastructure.ValueHolders
{
    public class ProfilePictureValueHolder : ValueHolder<byte[]>
    {
        public ProfilePictureValueHolder(ProfilePictureService service) 
            : base(name => service.GetFor(name.ToString()))
        {
        }
    }
}