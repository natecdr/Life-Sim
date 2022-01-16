using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentHandler : MonoBehaviour
{
    private Transform creaturesTransform;
    // Start is called before the first frame update
    void Start()
    {
        creaturesTransform = this.gameObject.transform.GetChild(0);
    }

    // Update is called once per frame
    void Update()
    {
        foreach(Transform child in creaturesTransform){
            //Do nothing
        }
    }
}
