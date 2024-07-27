using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleAction
{
    public string ActionName;
    public string ActionDescription;
    public string ActionType;
    public string ActionId;

    public string ActionArt;

    public int range;
    public int damage;

    public int mana_cost;
    public int health_cost;
    public int stamina_cost;
    public int speed;

    public string Additional_Effect()
    {
        // some actions may have additional affects the effects can be sourced from an effect library.
        // when a Unit preforms an action on Target the name of the Effect is returned to the Unit.
        // the Unit then reads the effect library, and calls the EffectMethod() passing the Target.
        // This allows all the math related to the effect to be contained in a single location
        // and used by multiple units.

        Debug.Log("This actions additional effect is...");
        return "test";
    }

    
}
