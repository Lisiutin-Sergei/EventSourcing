using EventSourcing.Core.Domain;
using EventSourcing.Core.ServiceBus;
using EventSourcing.Core.Utils;
using System;
using System.Collections.Generic;

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
		/// <typeparam name="TMessage">Тип сообщения.</typeparam>
		/// <param name="handler">Обработчик.</param>
		public void RegisterHandler<TMessage>(Action<TMessage> handler) 
			where TMessage : IMessage
		{
			Argument.NotNull(handler, "Не задан обработчик собщения.");

			List<Action<IMessage>> handlers;

			if (!_handlers.TryGetValue(typeof(TMessage), out handlers))
			{
				handlers = new List<Action<IMessage>>();
				_handlers.Add(typeof(TMessage), handlers);
			}

			handlers.Add((x => handler((TMessage)x)));
		}

		/// <summary>
		/// Отправить команду обработчику.
		/// </summary>
		/// <typeparam name="TCommand">Тип команды.</typeparam>
		/// <param name="command">Команда.</param>
		public void Send<TCommand>(TCommand command) 
			where TCommand : ICommand
		{
			Argument.NotNull(command, "Не задана команда.");

			List<Action<IMessage>> handlers;

			if (_handlers.TryGetValue(typeof(TCommand), out handlers))
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
		/// <typeparam name="TEvent">Тип события.</typeparam>
		/// <param name="event">Событие.</param>
		public void Publish<TEvent>(TEvent @event) 
			where TEvent : IEvent
		{
			Argument.NotNull(@event, "Не задано событие.");

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
