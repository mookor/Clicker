using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerAmp
{
    public enum AmpType
    {
        ADD_POWER,
        BOOST_TAP,
        PASSIVE_DAMAGE

    }
    public AmpType Type{ get; private set;}
    public int Priority {get;private set;}

    public bool IsPassive {get;private set;}

    public float Value => InitValue + IncPerLevel* Mathf.Clamp(Level -1 ,0 , int.MaxValue);
    public float InitValue {get;private set;}

    public float IncPerLevel {get;private set;}

    public float Level {get;private set;}

    public int Price {get;private set;}

    public int Chance {get;private set;}

    public PowerAmp(AmpType type,int priority, bool isPassive, float value, float incr, int price,int chance = 100)
    {
        Type = type;
        Priority = priority;
        IsPassive = isPassive;
        InitValue = value;
        IncPerLevel = incr;
        Price = price;
        Chance = chance;

    }

    public float CalcPow(float initPower)
    {
        if (Level == 0 )
            return initPower;
        
        switch(Type)
        {
            case AmpType.ADD_POWER:
            case AmpType.PASSIVE_DAMAGE:
                return initPower + Value;

            case AmpType.BOOST_TAP:
                if (Random.Range(0,100) < Chance)
                    return initPower * Value;
                else
                    return initPower;

            default:
                return initPower;                
                
        }

    }

    public void LevelUP()
    {

        Level++ ; 
    }
}
