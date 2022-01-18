using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

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
        this.brain.inputs[0].UpdateInput(Vector2.Distance(this.creatureTransform.position,Camera.main.ScreenToWorldPoint(Input.mousePosition)));
        if (Input.GetKeyDown("space")) {
            this.brain.Mutate();
        }
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

    public void OnDrawGizmos() {
        if (!Application.isPlaying) return;

        Vector2 pos = (Vector2)this.creatureTransform.position;

        for (int i = 0; i<this.brain.inputs.Count; i++) {
            InputNeuron fromNeuron = this.brain.inputs[i];

            for(int j = 0; j<this.brain.hiddenNeurons.Count; j++) {
                Neuron toNeuron = this.brain.hiddenNeurons[j];
                if (fromNeuron.IsConnected(toNeuron)) {
                    Gizmos.color = new Color(Math.Abs(Math.Min((float)fromNeuron.GetSynapse(toNeuron).weight, 0)), Math.Max((float)fromNeuron.GetSynapse(toNeuron).weight, 0), 0);
                    Gizmos.DrawLine(pos + new Vector2(-1, i), pos + new Vector2(0, 0.5f + j));
                }
            }
            for(int j = 0; j<this.brain.outputs.Count; j++) {
                Neuron toNeuron = this.brain.outputs[j];
                if (fromNeuron.IsConnected(this.brain.outputs[j])) {
                    Gizmos.color = new Color(Math.Abs(Math.Min((float)fromNeuron.GetSynapse(toNeuron).weight, 0)), Math.Max((float)fromNeuron.GetSynapse(toNeuron).weight, 0), 0);
                    Gizmos.DrawLine(pos + new Vector2(-1, i), pos + new Vector2(1, j));
                }
            }
        }

        for (int i = 0; i<this.brain.hiddenNeurons.Count; i++) {
            Neuron fromNeuron = this.brain.hiddenNeurons[i];
            for(int j = 0; j<this.brain.outputs.Count; j++) {
                Neuron toNeuron = this.brain.outputs[j];
                if (fromNeuron.IsConnected(toNeuron)) {
                    Gizmos.color = new Color(Math.Abs(Math.Min((float)fromNeuron.GetSynapse(toNeuron).weight, 0)), Math.Max((float)fromNeuron.GetSynapse(toNeuron).weight, 0), 0);
                    Gizmos.DrawLine(pos + new Vector2(0, 0.5f + i), pos + new Vector2(1, j));
                }
            }
        }
        
        Gizmos.color = Color.gray;
        for (int i = 0; i<this.brain.inputs.Count; i++) {
            Gizmos.DrawSphere((Vector2)this.creatureTransform.position + new Vector2(-1, i), 0.1f);
        }
        for (int i = 0; i<this.brain.hiddenNeurons.Count; i++) {
            Gizmos.DrawSphere((Vector2)this.creatureTransform.position + new Vector2(0, 0.5f + i), 0.1f);
        }
        for (int i = 0; i<this.brain.outputs.Count; i++) {
            Gizmos.DrawSphere((Vector2)this.creatureTransform.position + new Vector2(1, i), 0.1f);
        }
    }
}
