using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
public class AmpPref : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private TextMeshProUGUI level;
    private PowerAmp amp;
    [SerializeField]
    private TextMeshProUGUI price;
    

    public void setAmp(PowerAmp amp)
    {
        this.amp = amp;
        UpdateUI();
    }

    private void UpdateUI()
    {
        level.text = "X" + amp.Level;
        price.text = "$" + amp.Price;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("qwe");
        if (Clicker.Instance.Money < amp.Price)
            
            return;
        
        Clicker.Instance.Money -= amp.Price;
        amp.LevelUP();
        UpdateUI();
        Clicker.Instance.UpdateUI();
    }
}
