using ElementalSelector.Enums;

namespace ElementalSelector.Models
{
	public class ElementModel
	{
		public ElementType Type { get; }
		public float Speed { get; }

		public ElementModel(ElementType type, float speed)
		{
			Type = type;
			Speed = speed;
		}
	}
}