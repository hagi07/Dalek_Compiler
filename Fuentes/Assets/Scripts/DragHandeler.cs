using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class DragHandeler : MonoBehaviour, IBeginDragHandler, IDragHandler , IEndDragHandler{

	public static GameObject itemBeginDragged;
	public string tipo;
	Vector3 startPosition;
	Transform startParent;

	#region IBeginDragHandler implementation
	public void OnBeginDrag (PointerEventData eventData)
	{
		Slot.modulo = gameObject;
		itemBeginDragged = gameObject;
		startPosition = transform.position;
		startParent = transform.parent;
		GetComponent<CanvasGroup> ().blocksRaycasts = false;
	}
	#endregion

	#region IDragHandler implementation
	public void OnDrag (PointerEventData eventData)
	{
		transform.position = Input.mousePosition;
	}
	#endregion
	
	#region IEndDragHandler implementation
	public void OnEndDrag (PointerEventData eventData)
	{
		itemBeginDragged = null;
		GetComponent<CanvasGroup> ().blocksRaycasts = true;
		if (transform.parent == startParent || gameObject.tag == "Killer") {
			transform.position = startPosition;
		}
	}
	#endregion

}
