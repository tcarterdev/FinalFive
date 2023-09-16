using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private string prompt;

    public string InteractionPrompt => prompt;

    public bool Interact(Interactor interactor)
    {
        var inventory = interactor.GetComponent<Inventory>();

        if (inventory == null)
        {
            return false;
        }

        if (inventory.HasKey)
        {
            Debug.Log("Open Door");
            Destroy(this.gameObject);
            return true;
        }

        Debug.Log("No Key Found");
        return false;



    }
}
