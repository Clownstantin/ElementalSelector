using System;
using System.Collections.Generic;

namespace ElementalSelector.Core.EventBus
{
	public class EventBus
	{
		private readonly Dictionary<Type, List<Action<object>>> _subscribers = new();

		public void Subscribe<T>(Action<T> action) where T : struct
		{
			var type = typeof(T);
			if(!_subscribers.ContainsKey(type))
				_subscribers[type] = new List<Action<object>>();

			_subscribers[type].Add(e => action((T)e));
		}

		public void Unsubscribe<T>(Action<T> action) where T : struct
		{
			var type = typeof(T);
			if(_subscribers.ContainsKey(type))
			{
				// This is a simplified unsubscribe. A more robust solution might require keeping a reference to the wrapper action.
				// For now, this is a placeholder.
			}
		}

		public void Publish<T>(T eventData) where T : struct
		{
			var type = typeof(T);
			if(_subscribers.ContainsKey(type))
				foreach(var subscriber in _subscribers[type])
					subscriber(eventData);
		}
	}
}