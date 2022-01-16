using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InputNeuron : INeuron
{
    public Guid Id { get; private set; }
    public string name;
    public InputSynapse input {get; set;}
    public List<ISynapse> outputs {get; set;}
    public IActivationFunction activationFunction;
    public IInputFunction inputFunction;

    public InputNeuron(IActivationFunction activationFunction, IInputFunction inputFunction) {
        this.Id = Guid.NewGuid();
        this.name = Id.ToString();
        this.input = new InputSynapse(this);
        this.outputs = new List<ISynapse>();
        this.activationFunction = activationFunction;
        this.inputFunction = inputFunction;
    }

    public InputNeuron(IActivationFunction activationFunction, IInputFunction inputFunction, String name) {
        this.Id = Guid.NewGuid();
        this.name = name;
        this.input = new InputSynapse(this);
        this.outputs = new List<ISynapse>();
        this.activationFunction = activationFunction;
        this.inputFunction = inputFunction;
    }

    public void UpdateInput(Double inputValue) { 
        this.input = new InputSynapse(this, inputValue);
    }

    public void AddOutputNeuron(Neuron outputNeuron) {
        ISynapse synapse = new Synapse(this, outputNeuron);
        this.outputs.Add(synapse);
        outputNeuron.inputs.Add(synapse);
    }

    public void SetInputSynapse(double inputValue) {
        InputSynapse inputSynapse = new InputSynapse(this, inputValue);
        this.input = inputSynapse; 
    }

    public double CalculateOutput() {
        return this.activationFunction.CalculateOutput(inputFunction.CalculateInput(this.input));
    }
}
