using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientSpawner : MonoBehaviour
{
    [SerializeField] private GameObject myIngredientPrefab;

    private Player player;

    private void Awake(){
        this.transform.SetParent(FindObjectOfType<GameManager>().ingredientParent);
        player = FindObjectOfType<Player>();
    }

    private void OnMouseDown(){
        if (!player.handFree) return;
        GameObject newIngredient = Instantiate(myIngredientPrefab, this.transform);
        player.PickUpItem(newIngredient);
    }
}
