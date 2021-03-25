using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
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


    public float Value => InitValue * (float)(IncPerLevel * Level);
    public float Price => InitPrice * (float)(Math.Pow(IncreacePricePerLevel,Level));


    public float InitValue {get;private set;}
    public float IncPerLevel {get;private set;}
    public int Level {get;private set;}

    public int InitPrice {get;private set;}
    public float IncreacePricePerLevel {get; private set;}
    public int Chance {get;private set;}

    public PowerAmp(AmpType type,int priority, bool isPassive, float value, float incr, int initPrice,float priceIncrease,int chance = 100)
    {
        Type = type;
        Priority = priority;
        IsPassive = isPassive;
        InitValue = value;
        IncPerLevel = incr;
        InitPrice = initPrice;
        IncreacePricePerLevel = priceIncrease;
        Chance = chance;

        Level = PlayerPrefs.GetInt("DA_" + Type.ToString(),0);

    }

    public float CalcPow(float initPower)
    {
        if (Level == 0 )
            return initPower;
        
        switch(Type)
        {
            case AmpType.ADD_POWER:
            return initPower*Value;

            case AmpType.PASSIVE_DAMAGE:
                return Value;

            case AmpType.BOOST_TAP:
                if (UnityEngine.Random.Range(0,100) < Chance)
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
        PlayerPrefs.SetInt("DA_" + Type.ToString(),Level);
    }
}
