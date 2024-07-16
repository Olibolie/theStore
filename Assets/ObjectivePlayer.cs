using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectivePlayer : MonoBehaviour
{
    public TextMeshProUGUI objectiveText;
 
    public List<PickupObj> shoppingList = new List<PickupObj>();


    // Start is called before the first frame update
    void Start()
    {
        if (objectiveText == null)
        {
            objectiveText = GameObject.Find("objectiveText").GetComponent<TextMeshProUGUI>();
        }

        UpdateShoppingListDisplay();
        Debug.Log(shoppingList);

    }

    //public void AddItem(string itemName)
    //{
    //    PickupObj newItem = new PickupObj(itemName);
    //    shoppingList.Add(newItem);
    //    UpdateShoppingListDisplay();
    //}

    //public void RemoveItem(string itemName)
    //{
    //    PickupObj itemToRemove = shoppingList.Find(item => item.Name == itemName);
    //    if (itemToRemove != null)
    //    {
    //        shoppingList.Remove(itemToRemove);
    //        UpdateShoppingListDisplay();
    //    }
    //}

    public void ToggleItemBought(string itemName)
    {
        PickupObj itemToToggle = shoppingList.Find(item => item.Name == itemName);
        if (itemToToggle != null)
        {
            itemToToggle.ToggleBought();
            UpdateShoppingListDisplay();
        }
    }
    private void UpdateShoppingListDisplay()
    {
        objectiveText.text = "";
        foreach (var item in shoppingList)
        {


            string status = item.IsBought ? " (Bought)" : " (Not Bought)";
            objectiveText.text += item.Name + status + "\n";
        }
    }



  
}
