using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class indicator : MonoBehaviour
{
    public RawImage one;
    public RawImage two;
    public RawImage three;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setIndicators(int num)
    {
        switch (num)
        {
            case 1:
                one.color = Color.green;
                two.color = Color.red;
                three.color = Color.red;
                break;
            case 2:
                one.color = Color.green;
                two.color = Color.green;
                three.color = Color.red;
                break;
            case 3:
                one.color = Color.green;
                two.color = Color.green;
                three.color = Color.green;
                break;
        }
    }
}
