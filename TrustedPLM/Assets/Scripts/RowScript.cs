using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RowScript : MonoBehaviour
{
     string url = "vk.com";
    public GameObject rowPrefab;
    public GameObject rowPrefab2;
    public float subrowOffset = 25f;
//    private float parentRowPosX;
//    private float parentRowPosY;
//    private float parentRowWidth;
//    private float parentRowHeight;
    private RectTransform parentRow;
    public float spaceBetweenRows = 0.07f;
    public GameObject parentContainer;
    public List<GameObject> nextRows = new List<GameObject>();
    
    
    // Start is called before the first frame update
    void Start()
    {
        parentRow = gameObject.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void ExpandRow()
    {

        GameObject newRow = Instantiate(rowPrefab, parentContainer.transform);
        RectTransform newRowPos = newRow.GetComponent<RectTransform>();
        newRowPos.localPosition = new Vector3(parentRow.localPosition.x + subrowOffset,
                parentRow.localPosition.y - parentRow.rect.height - spaceBetweenRows); 
        
        GameObject newRow2 = Instantiate(rowPrefab2, parentContainer.transform);
        RectTransform newRowPos2 = newRow2.GetComponent<RectTransform>();
        newRowPos2.localPosition = new Vector3(parentRow.localPosition.x + subrowOffset,
            parentRow.localPosition.y - parentRow.rect.height*2 - spaceBetweenRows*2);
        
       // Application.OpenURL(url);
                                      
      // offset following rows
        foreach (GameObject row in nextRows)
        {
            row.GetComponent<RectTransform>().localPosition = new Vector3( 
                row.GetComponent<RectTransform>().localPosition.x,
                row.GetComponent<RectTransform>().localPosition.y 
                 - 2*parentRow.rect.height - 2*spaceBetweenRows);
        }
        
    }
}
