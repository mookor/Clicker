using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System;
public class Clicker : MonoBehaviour
{
    public static Clicker Instance;
    [SerializeField]
    public float Money
    {
        get => PlayerPrefs.GetFloat("Money",0);
        private set => PlayerPrefs.SetFloat("Money",value);
    }

    [SerializeField]
    private TextMeshProUGUI money;

    [SerializeField]
    private List<AmpPref> ampPrefs;

    private List<PowerAmp> amps;
    private void Awake() {
        Instance = this;    
    }
    // Start is called before the first frame update
    void Start()
    {
        
        amps = new List<PowerAmp>()
        {
            new PowerAmp(type : PowerAmp.AmpType.ADD_POWER, priority: 0, isPassive: false,value: 1,incr: 2f,initPrice:100,priceIncrease:2.5f),
            new PowerAmp(type : PowerAmp.AmpType.BOOST_TAP, priority: 100, isPassive: false,value: 2,incr: 2f,initPrice:200,priceIncrease:2.5f),
            new PowerAmp(type : PowerAmp.AmpType.PASSIVE_DAMAGE, priority: 0, isPassive: true,value: 1,incr: 1f,initPrice:10,priceIncrease:1.2f),

            new PowerAmp(type : PowerAmp.AmpType.PASSIVE_DAMAGE, priority: 0, isPassive: true,value: 1,incr: 1f,initPrice:15,priceIncrease:1.2f),
            new PowerAmp(type : PowerAmp.AmpType.PASSIVE_DAMAGE, priority: 0, isPassive: true,value: 5,incr: 1f,initPrice:93,priceIncrease:1.24f),
            new PowerAmp(type : PowerAmp.AmpType.PASSIVE_DAMAGE, priority: 0, isPassive: true,value: 16,incr: 1f,initPrice:666,priceIncrease:1.22f),
            new PowerAmp(type : PowerAmp.AmpType.PASSIVE_DAMAGE, priority: 0, isPassive: true,value: 56,incr: 1f,initPrice:5323,priceIncrease:1.25f),
            new PowerAmp(type : PowerAmp.AmpType.PASSIVE_DAMAGE, priority: 0, isPassive: true,value: 135,incr: 1f,initPrice:31290,priceIncrease:1.27f),
            new PowerAmp(type : PowerAmp.AmpType.PASSIVE_DAMAGE, priority: 0, isPassive: true,value: 418,incr: 1f,initPrice:173228,priceIncrease:1.3f),
            new PowerAmp(type : PowerAmp.AmpType.PASSIVE_DAMAGE, priority: 0, isPassive: true,value: 14275,incr: 1f,initPrice:1385824,priceIncrease:1.28f),

        };
        for (int i = 0 ; i < ampPrefs.Count;i++)
            ampPrefs[i].setAmp(amps[i]);
        offline();
        StartCoroutine(PassiveTap());
        UpdateUI();
    }
    private void OnApplicationQuit() {
        PlayerPrefs.SetString("LastQuit",DateTime.UtcNow.ToString());
    }
    private void offline()
    {
        string LastQuitCheck =PlayerPrefs.GetString("LastQuit",null);
        Debug.Log(LastQuitCheck);
        if(LastQuitCheck.Length == 0){
            Debug.Log("ПЕРВЫЙ ЗАПУСК");
            return;
            
            }
        var LastQuit = DateTime.Parse(LastQuitCheck);
        double secondsSpan = (DateTime.UtcNow - LastQuit).TotalSeconds;
        float totalPower = (float)(secondsSpan) * GetPassivePower();
        
        TapTarget(totalPower);
        Debug.Log($"Братишка, ты пока в офлайне был тебе накапало - {totalPower}");
    }
    private IEnumerator PassiveTap()
    {

        while (true)
        {
            yield return new WaitForSeconds(1);
            TapTarget(GetPassivePower());
        }
    }
    public void Click()
    {
        TapTarget(GetPower());
    }

    private void TapTarget(float power){
        AddMoney(power);
    }

    private float GetPower(){
        float power = 1;
        var sort_amps = amps.FindAll(x => !x.IsPassive);
        sort_amps.Sort((x,y) => x.Priority.CompareTo(y.Priority));

        foreach( var amp in sort_amps)
            power = amp.CalcPow(power);
        return power;
    }

    private float GetPassivePower(){ 
        float power = 0;
        var sort_amps = amps.FindAll(x => x.IsPassive);
        sort_amps.Sort((x,y) => x.Priority.CompareTo(y.Priority));

        foreach( var amp in sort_amps)
            power = amp.CalcPow(power);
        return power;
    }
    public void UpdateUI()
    {

        money.text = Money.ToString();
    }
    
    public void AddMoney(float Value)
    {
        Money += Value;
        UpdateUI();
        foreach( var prefab in ampPrefs)
            prefab.UpdateUI();
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }

}
