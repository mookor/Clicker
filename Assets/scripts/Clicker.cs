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
            new PowerAmp(PowerAmp.AmpType.ADD_POWER,0,false,1,2f,100,2.5f),
            new PowerAmp(PowerAmp.AmpType.BOOST_TAP,100,false,2,2f,200,2.0f),
            new PowerAmp(PowerAmp.AmpType.PASSIVE_DAMAGE,0,true,1,1.25f,10,1.3f)


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
        if(LastQuitCheck == null){
            Debug.Log("ПЕРВЫЙ ЗАПУСК");
            return;
            
            }
        var LastQuit = DateTime.Parse(PlayerPrefs.GetString("LastQuit"));
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
