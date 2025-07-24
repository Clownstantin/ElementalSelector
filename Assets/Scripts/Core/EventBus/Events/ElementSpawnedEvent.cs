using ElementalSelector.Enums;
using UnityEngine;

namespace ElementalSelector.Core.EventBus
{
	public readonly struct ElementSpawnedEvent
	{
		public ElementType Type { get; }
		public Vector3 Position { get; }

		public ElementSpawnedEvent(ElementType type, Vector3 position)
		{
			Type = type;
			Position = position;
		}
	}
}