using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Crafting : MonoBehaviour {

	public ItemDatabase dataBase;
	public Inventory inventory;

	public GameObject craftingPanel;
	public GameObject craftingSlot;


	public int playerLevel;

	void Start(){
		GenSlots ();
	}


	void GenSlots(){
		for (int i = 0; i < dataBase.dataBaseItems.Count; i++) {
			Item currentItem = dataBase.dataBaseItems [i];
			if (currentItem.isCraftable && playerLevel >= currentItem.MinLevelToCraft) {
				GameObject go = Instantiate (craftingSlot, craftingPanel.transform.position, Quaternion.identity);
				go.transform.SetParent (craftingPanel.transform);
				go.GetComponent <CraftingSlot> ().myItem = currentItem;
			}
		}
	}


	public void CraftItem(Item itemToCraft){
		if (itemToCraft.isCraftable) {
			if (canCraft (itemToCraft)) {
				Add (itemToCraft);
			} else {
				print ("cant craft that item");
			}
		} else {
			return;
		}
	}

	bool canCraft(Item itemToLookUP){
		for (int i = 0; i < itemToLookUP.crftItmes.Count; i++) {
			Item currentItem = itemToLookUP.crftItmes [i];
			int currentAmount = itemToLookUP.crftAmnt [i];
			if (!inventory.HasInInventory (currentItem.itemName, currentAmount)) {
				return false;
			}
		}
		return true;
	}



	void Add(Item itemToAdd){
		inventory.AddItem (itemToAdd, itemToAdd.makesHowMany);
		Remove (itemToAdd);
	}

	void Remove(Item itemToRemove){
		for (int i = 0; i < itemToRemove.crftItmes.Count; i++) {
			Item currentItem = itemToRemove.crftItmes [i];
			int currentAmount = itemToRemove.crftAmnt [i];
			inventory.RemoveItem (currentItem, currentAmount);
		}
	}


//end o class
}
