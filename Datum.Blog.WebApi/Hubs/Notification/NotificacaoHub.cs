using Microsoft.AspNetCore.SignalR;

namespace Datum.Blog.WebApi.Hubs.Notification
{
    public class NotificacaoHub : Hub, INotificacaoHub
    {
        private readonly IHubContext<NotificacaoHub> _context;

        public NotificacaoHub(IHubContext<NotificacaoHub> context)
        {
            _context = context;
        }

        public async Task EnviarMensagem(string messagem)
        {
            await _context.Clients.All.SendAsync("ReceiveMessage", "DatumBlog", messagem);
        }
    }
}
