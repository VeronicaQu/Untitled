using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    private List <Ingredient> ingredientPrefabs;
    public List <Ingredient> ingredients {set{ingredientPrefabs = value;}}
    
    private List <Customer> customerPrefabs;
    public List <Customer> customers {set{customerPrefabs = value;}}


    void CreateCustomer(){ //create a customer with random ingredients
        int i = Random.Range(1, ingredientPrefabs.Count);
        int c = Random.Range(1, customerPrefabs.Count);
    }
}
