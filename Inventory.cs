using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour {



	public List<GameObject> slots = new List<GameObject> ();

	[HideInInspector]
	public bool isDragging = false;
	public Image draggingImage = null;
	[HideInInspector]
	public Item draggingItem;
	[HideInInspector]
	public int draggingAmount;

	public GameObject toolTipPanel;
	public Text toolTipText;



	public bool AddItem(Item itemToAdd, int amount){
		Slot emptySlot = null;
		for (int i = 0; i < slots.Count; i++) {
			Slot currentSlot = slots [i].GetComponent<Slot> ();
			if (currentSlot.myItem == itemToAdd && itemToAdd.isStackable && currentSlot.myAmount + amount <= itemToAdd.maxStackAmount) {
				currentSlot.AddItem (itemToAdd, amount);
				return true;
			} else if (currentSlot.myItem == null && emptySlot == null) {
				emptySlot = currentSlot;
			}
		}

		if (emptySlot != null) {
			emptySlot.AddItem (itemToAdd, amount);
			return true;
		} else {
			print ("Inventory is full!!!");
			return false;
		}


	}

	public void RemoveItem(Item itemToRemove, int amount){
		for (int i = 0; i < slots.Count; i++) {
			Slot currentSlot = slots [i].GetComponent <Slot> ();
			if (currentSlot.myItem == itemToRemove) {
				currentSlot.RemoveItem (amount);
			}
		}
	}

	//------------------------------------------------------------Crafting Helpers-----------------------------


	public bool HasInInventory(string lookupItem, int amnt){
		for (int i = 0; i < slots.Count; i++) {
			if (slots [i].GetComponent<Slot> ().myItem != null) {
				if (slots[i].GetComponent<Slot>().myItem.itemName == lookupItem && slots[i].GetComponent<Slot>().myAmount >= amnt) {
					return true;
				}
			}
		}
		return false;
	}


	//------------------------------------------------------------Drag Drop Helpers-----------------------------

	public void DoDrag(Item itemToDrag, int amnt){
		draggingItem = itemToDrag;

		isDragging = true;
		draggingImage.enabled = true;
		draggingImage.sprite = draggingItem.itemIcon;
		draggingAmount = amnt;
	}

	public void EndDrag(){
		draggingItem = null;
		isDragging = false;
		draggingImage.enabled = false;
		draggingAmount = 0;
	}


	//--------------------------------------------------------------Inentory tool tip---------------------------------

	public void ShowToolTip(Item toolTipItem){
		toolTipPanel.SetActive (true);
		toolTipText.text = toolTipItem.itemName + "\n" + toolTipItem.MinLevelToCraft.ToString ();
	}

	public void HideToolTip(){
		toolTipPanel.SetActive (false);
	}

//end o class
}
