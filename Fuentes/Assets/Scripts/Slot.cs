using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IDropHandler {
	public GameObject item
	{
		get
		{
			if(transform.childCount > 0)
				return transform.GetChild(0).gameObject;
			return null;
		}
	}
	public static GameObject modulo;

	#region IDropHandler implementation
	public void OnDrop (PointerEventData eventData)
	{
        if (gameObject.name == "Eliminador" && DragHandeler.itemBeginDragged.gameObject.transform.parent.tag != "Modulo")
        {
			Destroy(DragHandeler.itemBeginDragged.gameObject);
		}
		if (!item && gameObject.tag != "Killer") {
			DragHandeler.itemBeginDragged.transform.SetParent (transform);
			ExecuteEvents.ExecuteHierarchy<IHasChanged> (gameObject, null, (x,y) => x.hasChanged ());
		}
	}
	#endregion

	void start()
	{
	}

	void Update()
	{
		if (transform.childCount == 0 && this.tag == "Modulo") {
			GameObject x = Instantiate(modulo);
			x.name = modulo.name;
			x.transform.SetParent(transform);
		}
	}
}
