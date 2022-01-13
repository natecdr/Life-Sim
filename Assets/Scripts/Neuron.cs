using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Neuron
{
    public Guid Id;
    public string name;
    public List<ISynapse> inputs {get; set;}
    public List<ISynapse> outputs {get; set;}
    private List<ISynapse> inputSynapses;
    public IActivationFunction activationFunction;
    public IInputFunction inputFunction;

    public Neuron(IActivationFunction activationFunction, IInputFunction inputFunction) {
        this.Id = Guid.NewGuid();
        this.name = Id.ToString();
        this.inputs = new List<ISynapse>();
        this.outputs = new List<ISynapse>();
        this.activationFunction = activationFunction;
        this.inputFunction = inputFunction;
    }

    public Neuron(IActivationFunction activationFunction, IInputFunction inputFunction, String name) {
        this.Id = Guid.NewGuid();
        this.name = name;
        this.inputs = new List<ISynapse>();
        this.outputs = new List<ISynapse>();
        this.activationFunction = activationFunction;
        this.inputFunction = inputFunction;
    }

    public void AddInputNeuron(Neuron inputNeuron) {
        ISynapse synapse = new Synapse(inputNeuron, this);
        this.inputs.Add(synapse);
        inputNeuron.outputs.Add(synapse);
    }

    public void AddOutputNeuron(Neuron outputNeuron) {
        ISynapse synapse = new Synapse(this, outputNeuron);
        this.outputs.Add(synapse);
        outputNeuron.inputs.Add(synapse);
    }

    public void AddInputSynapse(double inputValue) {
        ISynapse inputSynapse= new InputSynapse(this, inputValue);
        this.inputs.Add(inputSynapse);
    }

    public double CalculateOutput() {
        return this.activationFunction.CalculateOutput(inputFunction.CalculateInput(this.inputs));
    }
}
