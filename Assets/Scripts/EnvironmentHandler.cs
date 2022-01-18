using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentHandler : MonoBehaviour
{
    public GameObject creaturePrefab;
    private Transform creaturesTransform;
    // Start is called before the first frame update
    void Start()
    {
        creaturesTransform = this.gameObject.transform.GetChild(0);
    }

    // Update is called once per frame
    void Update()
    {
        foreach(Transform child in creaturesTransform) {
            //Do nothing
        }
        // if (Input.GetKeyDown("space")) {
        //     GameObject creature = creaturesTransform.GetChild(0).gameObject;
        //     Destroy(creature);
        //     creature = Instantiate(creaturePrefab, new Vector3(0f, 0f, 0f), Quaternion.identity);
        //     creature.transform.parent=creaturesTransform;
        // }
    }
}
