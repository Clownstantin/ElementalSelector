using ElementalSelector.Models;
using UniRx;

namespace ElementalSelector.ViewModels
{
	public interface IElementViewModel
	{
		IReadOnlyReactiveProperty<ElementModel> Element { get; }
		void OnDrag();
		void OnDrop(bool isCorrectSlot);
	}

	public class ElementViewModel : IElementViewModel
	{
		private readonly ReactiveProperty<ElementModel> _element;

		public IReadOnlyReactiveProperty<ElementModel> Element => _element;

		public float Speed => _element.Value.Speed;

		public ElementViewModel(ElementModel model) => _element = new ReactiveProperty<ElementModel>(model);

		public void OnDrag()
		{
			// Logic when dragging starts
		}

		public void OnDrop(bool isCorrectSlot)
		{
			// Logic for when the shape is dropped
		}
	}
}