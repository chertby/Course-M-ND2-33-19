using System;
using Htp.Crocodili.Web.Servicies;
using Microsoft.Extensions.DependencyInjection;

namespace Htp.Crocodili.Web.Extensions
{
    public static class ChatRoomServiceExtensions
    {
        public static void AddChatRoom(this IServiceCollection services)
        {
            services.AddSingleton<IChatRoomService, InMemoryChatRoomService>();
            services.AddSignalR();
        }
    }
}
