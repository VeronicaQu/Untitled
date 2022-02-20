using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    private List <Ingredient> baseIngredientPrefabs;
    public List <Ingredient> baseIngredients {set{baseIngredientPrefabs = value;}}
    private List <Ingredient> ingredientPrefabs;
    public List <Ingredient> ingredients {set{ingredientPrefabs = value;}}
    
    private List <Customer> customerPrefabs;
    public List <Customer> customers {set{customerPrefabs = value;}}
    private Customer newCustomer;

    //timer vars
    [SerializeField] private float minTimeToCustomer;
    [SerializeField] private float maxTimeToCustomer;
    [SerializeField] private float rushPercentIncrease;
    private Timer timer;
    GameManager gm;

    void Awake(){
        gm = FindObjectOfType<GameManager>();
        timer = Instantiate(gm.timerPrefab, this.transform).GetComponent<Timer>();
    }

    void CreateCustomer(){ //create a customer with random ingredients
        int c = Random.Range(0, customerPrefabs.Count);
        int b = Random.Range(0, baseIngredientPrefabs.Count);

        //FIX: This should be the customer profile
        newCustomer = Instantiate(customerPrefabs[c], gm.orderParent).GetComponent<Customer>();
        newCustomer.AddToOrder(ingredientPrefabs[b].initialSprite, baseIngredientPrefabs[b].name, baseIngredientPrefabs[b].price);
        //FIX: Then from a map of customer profiles to prefabs of possible customer images

        for (int n = Random.Range(1, gm.maxIngredients); n >= 0; n--){
            int i = Random.Range(0, customerPrefabs.Count);
            newCustomer.AddToOrder(ingredientPrefabs[i].initialSprite, ingredientPrefabs[i].name, ingredientPrefabs[i].price);
        }

        newCustomer.Init();
    }
}
