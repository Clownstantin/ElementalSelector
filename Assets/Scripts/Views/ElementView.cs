using ElementalSelector.Enums;
using ElementalSelector.Models;
using ElementalSelector.ViewModels;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace ElementalSelector.Views
{
	public class ElementView : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler
	{
		private IElementViewModel _viewModel;
		private Vector3 _startPosition;
		private bool _isDragging;
		private Camera _mainCam;

		[Inject]
		public void Construct(ElementType type, float speed)
		{
			var model = new ElementModel(type, speed);
			_viewModel = new ElementViewModel(model);
		}

		private void Awake() => _mainCam = Camera.main;

		void Update()
		{
			if(_viewModel != null && !_isDragging)
				transform.position += _viewModel.Element.Value.Speed * Time.deltaTime * Vector3.right;
		}

		public void OnBeginDrag(PointerEventData eventData)
		{
			Debug.Log("BeginDrag");
			_startPosition = transform.position;
			_isDragging = true;
			_viewModel.OnDrag();
		}

		public void OnDrag(PointerEventData eventData)
		{
			Vector3 mouseScreenPos = Input.mousePosition;
			mouseScreenPos.z = Mathf.Abs(Camera.main.transform.position.z - transform.position.z);
			Vector3 worldPos = Camera.main.ScreenToWorldPoint(mouseScreenPos);
			worldPos.z = transform.position.z;
			transform.position = worldPos;
		}

		public void OnEndDrag(PointerEventData eventData)
		{
			_isDragging = false;

			// Placeholder for drop logic. We'll need to detect the slot.
			// For now, let's assume we can check if it was a correct or incorrect drop.
			bool isCorrectSlot = false; // This will be determined by the drop target

			var results = new System.Collections.Generic.List<RaycastResult>();
			EventSystem.current.RaycastAll(eventData, results);

			// A more robust implementation would involve checking for a specific component on the slot.
			if(results.Count > 1)
			{
				// Assuming the second result is the slot. The first is the shape itself.
				isCorrectSlot = true; // Placeholder
			}


			if(!isCorrectSlot)
			{
				transform.position = _startPosition;
			}

			_viewModel.OnDrop(isCorrectSlot);
		}
	}
}