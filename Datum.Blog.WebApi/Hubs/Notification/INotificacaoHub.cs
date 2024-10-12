namespace Datum.Blog.WebApi.Hubs.Notification
{
    public interface INotificacaoHub
    {
        Task EnviarMensagem(string messagem);
    }
}
