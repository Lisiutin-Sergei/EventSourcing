using EventSourcing.Core.Domain;

namespace EventSourcing.Core.ServiceBus
{
	/// <summary>
	/// Интерфейс отправки команды обработчику.
	/// </summary>
	public interface ICommandSender
	{
		/// <summary>
		/// Отправить команду.
		/// </summary>
		/// <typeparam name="TCommand">Тип команды.</typeparam>
		/// <param name="command">Команда.</param>
		void Send<TCommand>(TCommand command) where TCommand : ICommand;
	}
}
