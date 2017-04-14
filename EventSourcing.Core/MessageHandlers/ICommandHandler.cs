using EventSourcing.Core.Domain;

namespace EventSourcing.Core.MessageHandlers
{
	/// <summary>
	/// Интерфейс обработчика команд.
	/// </summary>
	/// <typeparam name="T">Тип команды.</typeparam>
	public interface ICommandHandler<T> : IMessageHandler<T> where T : ICommand
	{
	}
}
