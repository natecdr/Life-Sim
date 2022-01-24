using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brain
{
    public Creature creature;
    public List<InputNeuron> inputs;
    public List<Neuron> outputs;
    public List<Neuron> hiddenNeurons;
    public List<Synapse> synapses;

    public Brain(Creature creature, List<InputNeuron> inputs, List<Neuron> outputs){
        this.creature = creature;
        this.inputs = inputs;
        this.hiddenNeurons = new List<Neuron>();
        this.outputs = outputs;
        this.synapses = new List<Synapse>();
    }

    public Brain(Creature creature) {
        this.creature = creature;

        this.inputs = new List<InputNeuron>();
        this.hiddenNeurons = new List<Neuron>();
        this.outputs = new List<Neuron>();
        this.synapses = new List<Synapse>();

        this.inputs.Add(new InputNeuron(new RectifiedActivationFunction(), new WeightedSums(), "distanceToClosestFood"));
        this.inputs.Add(new InputNeuron(new LinearActivationFunction(), new WeightedSums(), "angleToClosestFood"));

        this.outputs.Add(new Neuron(new StepActivationFunction(0.5), new WeightedSums(), "avancer"));

        this.inputs[0].SetInputSynapse(0);
        this.inputs[1].SetInputSynapse(1);
    }

    public Brain(Creature creature, Brain parentBrain) {
        this.creature = creature;
        this.inputs = parentBrain.inputs;
        this.hiddenNeurons = parentBrain.hiddenNeurons;
        this.outputs = parentBrain.outputs;
        this.synapses = parentBrain.synapses;
    }

    public void AddConnection(INeuron fromNeuron, Neuron toNeuron) {
        if (!fromNeuron.IsConnected(toNeuron)) {
            fromNeuron.AddOutputNeuron(toNeuron);
            synapses.Add(fromNeuron.GetSynapse(toNeuron));
        }
    }

    public void RemoveConnection(INeuron fromNeuron, Neuron toNeuron) {
        if (fromNeuron.IsConnected(toNeuron)) {
            synapses.Remove(fromNeuron.GetSynapse(toNeuron));
            fromNeuron.RemoveOutputNeuron(toNeuron);   
        }
    }
    
    public void Act() {
        if (this.outputs[0].CalculateOutput() > 0.5) {
            this.creature.MoveForward();
        }
    }

    public void Mutate() {
        if (Random.Range(0f, 1f) > 0.9) {
            int choice = Random.Range(0,4);
            switch(choice) {
                case 0:
                    MutateAddConnection();
                    break;
                case 1:
                    MutateAddHiddenNeuron();
                    break;
                case 2: 
                    MutateChangeWeight();
                    break;
                case 3:
                    MutateRemoveConnection();
                    break;
            }
            Mutate();
        }
    }

    private void MutateAddConnection() {
        if (this.hiddenNeurons.Count > 0) {
            int combination = Random.Range(0, 3);
            switch(combination) {
                case 0:
                    AddConnection(inputs[Random.Range(0, this.inputs.Count)], outputs[Random.Range(0, this.outputs.Count)]);
                    break;
                case 1:
                    AddConnection(inputs[Random.Range(0, this.inputs.Count)], hiddenNeurons[Random.Range(0, this.hiddenNeurons.Count)]);
                    break;
                case 2: 
                    AddConnection(hiddenNeurons[Random.Range(0, this.hiddenNeurons.Count)], outputs[Random.Range(0, this.outputs.Count)]);
                    break;
            }
        } else {
            AddConnection(inputs[Random.Range(0, this.inputs.Count)], outputs[Random.Range(0, this.outputs.Count)]);
        } 
    }

    private void MutateAddHiddenNeuron() {
        if (synapses.Count > 0 && hiddenNeurons.Count < 4) {
            Synapse synapse = synapses[Random.Range(0, this.synapses.Count)];
            Neuron newNeuron = new Neuron(new SigmoidActivationFunction(1), new WeightedSums());
            RemoveConnection(synapse.fromNeuron, synapse.toNeuron);
            AddConnection(synapse.fromNeuron, newNeuron);
            AddConnection(newNeuron, synapse.toNeuron);
            this.hiddenNeurons.Add(newNeuron);
        }
    }

    private void MutateChangeWeight() {
        if (synapses.Count > 0) {
            Synapse synapse = synapses[Random.Range(0, this.synapses.Count)];
            synapse.weight = Random.Range(-1f, 1f);
        }
    }

    private void MutateRemoveConnection() {
        if (synapses.Count > 0) {
            Synapse synapse = synapses[Random.Range(0, this.synapses.Count)];
            RemoveConnection(synapse.fromNeuron, synapse.toNeuron);
        }
    }
}
