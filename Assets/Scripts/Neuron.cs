using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Neuron : INeuron
{
    public Guid Id { get; private set; }
    public string name;
    public List<Synapse> inputs {get; set;}
    public List<Synapse> outputs {get; set;}
    public IActivationFunction activationFunction;
    public IInputFunction inputFunction;

    public Neuron(IActivationFunction activationFunction, IInputFunction inputFunction) {
        this.Id = Guid.NewGuid();
        this.name = Id.ToString();
        this.inputs = new List<Synapse>();
        this.outputs = new List<Synapse>();
        this.activationFunction = activationFunction;
        this.inputFunction = inputFunction;
    }

    public Neuron(IActivationFunction activationFunction, IInputFunction inputFunction, String name) {
        this.Id = Guid.NewGuid();
        this.name = name;
        this.inputs = new List<Synapse>();
        this.outputs = new List<Synapse>();
        this.activationFunction = activationFunction;
        this.inputFunction = inputFunction;
    }

    public void AddInputNeuron(Neuron inputNeuron) {
        Synapse synapse = new Synapse(inputNeuron, this);
        this.inputs.Add(synapse);
        inputNeuron.outputs.Add(synapse);
    }

    public void AddOutputNeuron(Neuron outputNeuron) {
        Synapse synapse = new Synapse(this, outputNeuron);
        this.outputs.Add(synapse);
        outputNeuron.inputs.Add(synapse);
    }

    public void RemoveOutputNeuron(Neuron outputNeuron) {
        Synapse synapse = this.GetSynapse(outputNeuron);
        this.outputs.Remove(synapse);
        outputNeuron.inputs.Remove(synapse);
    }

    public double CalculateOutput() {
        return this.activationFunction.CalculateOutput(inputFunction.CalculateInput(this.inputs));
    }

    public bool IsConnected(Neuron neuron) {
        for(int i = 0; i<this.outputs.Count; i++) {
            if (outputs[i].toNeuron.Equals(neuron)) {
                return true;
            }
        }
        return false;
    }

    public Synapse GetSynapse(Neuron neuron) {
        for(int i = 0; i<this.outputs.Count; i++) {
            if (outputs[i].toNeuron.Equals(neuron)) {
                return outputs[i];
            }
        }
        return null;
    }
}
