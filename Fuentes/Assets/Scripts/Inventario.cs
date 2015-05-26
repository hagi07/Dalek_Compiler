using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Inventario : MonoBehaviour, IHasChanged {
	[SerializeField] Transform slots;
	[SerializeField] Text invetarioText;

	void Start () {
		hasChanged ();
	}

	#region IHasChanged implementation
	public void hasChanged ()
	{
		System.Text.StringBuilder builder = new System.Text.StringBuilder ();
		builder.Append (" - ");
		foreach (Transform slotTransform in slots) {
			GameObject item = slotTransform.GetComponent<Slot> ().item;
			if (item) {
				builder.Append (item.name);
				builder.Append (" - ");
			}
			invetarioText.text = builder.ToString();
		}
	}
	#endregion
}

namespace UnityEngine.EventSystems
{
	public interface IHasChanged : IEventSystemHandler
	{
		void hasChanged();
	}	
}