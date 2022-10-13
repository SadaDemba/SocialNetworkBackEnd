using SocialNetworkBackEnd.Interfaces;
using SocialNetworkBackEnd.Services;

namespace SocialNetworkBackEnd.Extensions
{
    public static class ServiceExtension
    {
        public static void RegisterAppServices(this IServiceCollection service)
        {
            service.AddScoped<IUser, UserService>();
            service.AddScoped<IPublication, PublicationService>();
            service.AddScoped<ICommentaire, CommentService>();
            service.AddScoped<ILike, LikeService>();
        }
    }
}
