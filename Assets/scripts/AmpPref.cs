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
    
    private CanvasGroup group;

    public void setAmp(PowerAmp amp)
    {
        group = GetComponent<CanvasGroup>();
        this.amp = amp;
        UpdateUI();
    }

    public void UpdateUI()
    {
        level.text = "X" + amp.Level;
        price.text = "$" + amp.Price;

        group.alpha = Clicker.Instance.Money >= amp.Price ? 1 : .5f;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("qwe");
        if (Clicker.Instance.Money < amp.Price)
            
            return;
        
        Clicker.Instance.AddMoney(-amp.Price);
        amp.LevelUP();
        UpdateUI();
        Clicker.Instance.UpdateUI();
    }
}
