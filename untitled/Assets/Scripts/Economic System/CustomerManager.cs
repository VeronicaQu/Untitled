using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerManager : MonoBehaviour
{
    // ==============   variables   ==============
    [SerializeField] private Customer firstCustomer;
    private Customer selectedCustomer;
    private Generator g;
    private List<Customer> lineup = new List<Customer>();
    
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
            if (firstCustomer !=null){
                firstCustomer.CheckOrder(order);
                if (lineup.Count > 0){
                    firstCustomer = lineup[0];
                    lineup.RemoveAt(0);
                }
                else firstCustomer = null;
            }
        }
        else selectedCustomer.CheckOrder(order);
    }

    public void lineupCustomer(Customer c){ //line up the customer behind the current end one
        lineup.Add(c);
    }
}
