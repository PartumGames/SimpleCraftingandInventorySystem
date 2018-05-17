using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CraftingSlot : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler {

	Crafting craftingScript;
	Inventory inventory;

	public Item myItem;
	Image myIcon;

	void Start(){
		inventory = GameObject.FindObjectOfType<Inventory> ();
		if (myItem == null) {
			Destroy (gameObject);
		}
		craftingScript = GameObject.FindObjectOfType<Crafting> ();
		myIcon = GetComponent <Image> ();
		myIcon.sprite = myItem.itemIcon;
	}

	public void OnPointerClick(PointerEventData eventData){
		craftingScript.CraftItem (myItem);
	}


	public void OnPointerEnter(PointerEventData eventData){
		if (myItem != null) {
			inventory.ShowToolTip (myItem);
		}
	}

	public void OnPointerExit(PointerEventData eventData){
		inventory.HideToolTip ();
	}


//end o class
}
