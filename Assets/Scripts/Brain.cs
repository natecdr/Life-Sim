using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brain
{
    public Creature creature;
    public List<Neuron> inputs;
    public List<Neuron> outputs;
    public List<Neuron> hiddenNeurons;

    public Brain(Creature creature, List<Neuron> inputs, List<Neuron> outputs){
        this.creature = creature;
        this.inputs = inputs;
        this.outputs = outputs;
    }
}
