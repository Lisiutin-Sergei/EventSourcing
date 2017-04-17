using EventSourcing.Core.Domain;
using EventSourcing.Core.ServiceBus;
using System;
using System.Collections.Generic;
using System.Threading;

namespace EventSourcing.Data.ServiceBus
{
	/// <summary>
	/// Сервисная шина.
	/// </summary>
	public class ServiceBus : IServiceBus
	{
		/// <summary>
		/// Словарь обработчиков сообщений, зарегистрированных в системе.
		/// </summary>
		private readonly Dictionary<Type, List<Action<IMessage>>> _handlers = new Dictionary<Type, List<Action<IMessage>>>();

		/// <summary>
		/// Зарегистрировать обработчик сообщения.
		/// </summary>
		/// <typeparam name="T">Тип сообщения.</typeparam>
		/// <param name="handler">Обработчик.</param>
		public void RegisterHandler<T>(Action<T> handler) where T : IMessage
		{
			List<Action<IMessage>> handlers;

			if (!_handlers.TryGetValue(typeof(T), out handlers))
			{
				handlers = new List<Action<IMessage>>();
				_handlers.Add(typeof(T), handlers);
			}

			handlers.Add((x => handler((T)x)));
		}

		/// <summary>
		/// Отправить команду обработчику.
		/// </summary>
		/// <typeparam name="T">Тип команды.</typeparam>
		/// <param name="command">Команда.</param>
		public void Send<T>(T command) where T : ICommand
		{
			List<Action<IMessage>> handlers;

			if (_handlers.TryGetValue(typeof(T), out handlers))
			{
				if (handlers.Count != 1)
				{
					throw new InvalidOperationException("Нельзя вызвать более 1 обработчика команды.");
				}
				handlers[0](command);
			}
			else
			{
				throw new InvalidOperationException("Не зарегистрировано ни одного обработчика команды.");
			}
		}

		/// <summary>
		/// Опубликовать событие.
		/// </summary>
		/// <typeparam name="T">Тип события.</typeparam>
		/// <param name="event">Событие.</param>
		public void Publish<T>(T @event) where T : IEvent
		{
			List<Action<IMessage>> handlers;

			if (!_handlers.TryGetValue(@event.GetType(), out handlers))
			{
				return;
			}

			foreach (var handler in handlers)
			{
				handler(@event);
			}
		}
	}
}
