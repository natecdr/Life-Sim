using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brain
{
    public Creature creature;
    public List<InputNeuron> inputs;
    public List<Neuron> outputs;
    public List<Neuron> hiddenNeurons;

    public Brain(Creature creature, List<InputNeuron> inputs, List<Neuron> outputs){
        this.creature = creature;
        this.inputs = inputs;
        this.outputs = outputs;
    }

    public Brain(Creature creature) {
        this.creature = creature;

        this.inputs = new List<InputNeuron>();
        this.outputs = new List<Neuron>();

        this.inputs.Add(new InputNeuron(new RectifiedActivationFunction(), new WeightedSums(), "fabrice"));
        this.outputs.Add(new Neuron(new StepActivationFunction(0.5), new WeightedSums(), "avancer"));

        this.inputs[0].SetInputSynapse(0);
        this.AddConnection(inputs[0], outputs[0]);
    }

    public void AddConnection(INeuron fromNeuron, Neuron toNeuron) {
        fromNeuron.AddOutputNeuron(toNeuron);
    }
    
    public void Act() {
        if (this.outputs[0].CalculateOutput() > 0.75) {
            this.creature.MoveForward();
        }
    }
}
