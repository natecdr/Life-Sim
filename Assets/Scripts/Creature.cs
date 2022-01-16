using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creature : MonoBehaviour
{
    private Transform creatureTransform;
    private Brain brain;
    // Start is called before the first frame update
    void Start()
    {
        creatureTransform = gameObject.GetComponent<Transform>();
        this.brain = new Brain(this);
    }
    

    // Update is called once per frame
    void Update()
    {
        this.brain.Act();
        //Debug.Log(Vector2.Distance(this.creatureTransform.position,Camera.main.ScreenToWorldPoint(Input.mousePosition)));
        this.brain.inputs[0].UpdateInput(Vector2.Distance(this.creatureTransform.position,Camera.main.ScreenToWorldPoint(Input.mousePosition)));

        Debug.Log(this.brain.inputs[0].CalculateOutput() * this.brain.inputs[0].outputs[0].weight);
        Debug.Log("Weight : " + this.brain.inputs[0].outputs[0].weight);
    }

    public void MoveForward() {
        creatureTransform.Translate(Vector3.right * Time.deltaTime);
    }

    public void TurnLeft() {
        creatureTransform.Rotate(0, 0, 20 * Time.deltaTime);
    }

    public void TurnRight() {
        creatureTransform.Rotate(0, 0, -20 * Time.deltaTime);
    }

    public void Turn(float turnValue) {
        creatureTransform.Rotate(0, 0, turnValue * Time.deltaTime);
    }
}
