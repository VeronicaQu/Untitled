using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerManager : MonoBehaviour
{
    private Dictionary <string, float> priceReference; 
    private Customer firstCustomer;
    private Generator g;
    // Start is called before the first frame update
    void Awake()
    {
        EventManager ev = FindObjectOfType<EventManager>();
        ev.OnLocationChange += UpdateOnLocationChange;
        g = FindObjectOfType<Generator>();
    }

    private void UpdateOnLocationChange(Location current, Location next){
        //current.customerProfiles = customerReference;
        priceReference = next.priceReference;
        
        //update the gernerator
        g.ingredients = next.ingredients;
        g.customers = next.customers;
    }

    public void ServeCustomer(List<Ingredient> order){

    }
}
