using MyShop.Domain.ValueHolders;
using MyShop.Infrastructure.Services;

namespace MyShop.Infrastructure.ValueHolders
{
    public class ProfilePictureValueHolder : ValueHolder<byte[]>
    {
        public ProfilePictureValueHolder() 
            : base(name => ProfilePictureService.GetFor(name.ToString()))
        {
        }
    }
}