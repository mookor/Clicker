using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class scroll : MonoBehaviour // , IBeginDragHandler, IDragHandler
{
    float y_pos;
    public GameObject swipe;
    RectTransform swipe_pos;
    Vector3 start_pos;
    [SerializeField]
    Camera cam;
    /*
    public void OnBeginDrag(PointerEventData eventData)
    {   float speed = 2000f;
        Debug.Log(eventData.delta.x);
        
        if (eventData.position.y < 440f){
        if(eventData.delta.x > 0)
        {
            swipe_pos.anchoredPosition = new Vector2(swipe_pos.anchoredPosition.x +speed*Time.deltaTime,swipe_pos.anchoredPosition.y);

            if (swipe_pos.anchoredPosition.x > 997)
            swipe_pos.anchoredPosition = new Vector2(1168f,swipe_pos.anchoredPosition.y);
            }
        
        else if(eventData.delta.x < 0)
            swipe_pos.anchoredPosition = new Vector2(swipe_pos.anchoredPosition.x +speed*Time.deltaTime,swipe_pos.anchoredPosition.y);

            if (swipe_pos.anchoredPosition.x < -1037)
            swipe_pos.anchoredPosition = new Vector2(-998f,swipe_pos.anchoredPosition.y);
            }
    }

    public void OnDrag(PointerEventData eventData)
    {
        411001000100
    }
    */

    // Start is called before the first frame update
    
    void Start()
    {
        y_pos = 500f;
        swipe_pos = swipe.GetComponent<RectTransform>();
        Debug.Log(swipe_pos.anchoredPosition);
    }

    // Update is called once per frame
    void Update()
    {   
        
        if (Input.GetMouseButtonDown(0)) {start_pos = cam.ScreenToWorldPoint(swipe_pos.anchoredPosition);
        y_pos = Input.mousePosition.y;
        
        }
        
        else if (Input.GetMouseButton(0) && y_pos < 450)
        {
            Debug.Log($"{cam.ScreenToWorldPoint(Input.mousePosition).x}  {start_pos.x}");
        
            Debug.Log(start_pos.x);
            float pos = cam.ScreenToWorldPoint(Input.mousePosition).x * 10f ;
            swipe_pos.anchoredPosition = new Vector3(swipe_pos.anchoredPosition.x -pos,swipe_pos.anchoredPosition.y);
            if (swipe_pos.anchoredPosition.x < -1037)
            swipe_pos.anchoredPosition = new Vector2(-998f,swipe_pos.anchoredPosition.y);
            if (swipe_pos.anchoredPosition.x > 1170)
            swipe_pos.anchoredPosition = new Vector2(1168f,swipe_pos.anchoredPosition.y);

        }
        
    }
}
