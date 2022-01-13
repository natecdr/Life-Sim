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
        List<Neuron> inputs = new List<Neuron>();
        List<Neuron> outputs = new List<Neuron>();

        brain = new Brain(this, inputs, outputs);
    }
    

    // Update is called once per frame
    void Update()
    {
        this.MoveForward();
        this.Turn(50);
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
