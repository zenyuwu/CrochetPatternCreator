using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] GameObject selectedShape;
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            int viewportLayerMask = 1 << LayerMask.NameToLayer("Viewport");
            int clickableLayerMask = ~viewportLayerMask;
            Vector2 mousePosition = Input.mousePosition;
            Ray ray = Camera.main.ScreenPointToRay(mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, clickableLayerMask))
            {
                if (hit.collider.gameObject.CompareTag("Shape"))
                {
                    if(selectedShape != null && selectedShape != hit.collider.gameObject)
                    {
                        //selectedShape.GetComponent<Renderer>().material.color = Color.white;
                    }
                    selectedShape = hit.collider.gameObject;
                    //selectedShape.GetComponent<Renderer>().material.color = Color.red;
                }
            }
        }
    }

    public void IncrementSelected()
    {
        if (selectedShape != null)
        {
            selectedShape.GetComponent<Pattern>().Increment();
            selectedShape.GetComponent<Pattern>().PrintPattern();
        }
    }

    public void DecrementSelected()
    {
        if(selectedShape != null)
        {
            selectedShape.GetComponent<Pattern>().Decrement();
            selectedShape.GetComponent<Pattern>().PrintPattern();
        }
    }

    public void Up()
    {
        if(selectedShape != null)
        {
            selectedShape.GetComponent<Transform>().gameObject.transform.position += new Vector3(0, 1, 0);
        }
    }

    public void Down()
    {
        if(selectedShape != null)
        {
            selectedShape.GetComponent<Transform>().gameObject.transform.position += new Vector3(0, -1, 0);
        }
    }
}
