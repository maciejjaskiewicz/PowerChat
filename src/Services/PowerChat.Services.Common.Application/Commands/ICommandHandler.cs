using MediatR;

namespace PowerChat.Services.Common.Application.Commands
{
    public interface ICommandHandler<in TCommand> : INotificationHandler<TCommand>
        where TCommand : ICommand
    { }

    public abstract class CommandHandler<TCommand> : NotificationHandler<TCommand>, ICommandHandler<TCommand>
        where TCommand : ICommand
    { }
}