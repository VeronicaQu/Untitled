using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerManager : MonoBehaviour
{
    // ==============   variables   ==============
    [SerializeField] private Customer firstCustomer;
    private Customer selectedCustomer;
    private Generator g;
    
    // ==============   methods   ==============
    void Start()
    {
        EventManager ev = FindObjectOfType<EventManager>();
        ev.OnLocationChange += UpdateOnLocationChange;
        g = FindObjectOfType<Generator>();
    }

    private void UpdateOnLocationChange(Location current, Location next){
        //update the generator
        g.baseIngredients = next.baseIngredients;
        g.ingredients = next.ingredients;
        g.customers = next.customers;
    }

    public void ServeCustomer(List<string> order){
        if (selectedCustomer == null) {
            if (firstCustomer !=null) firstCustomer.CheckOrder(order);
            }
        else selectedCustomer.CheckOrder(order);
    }
}
