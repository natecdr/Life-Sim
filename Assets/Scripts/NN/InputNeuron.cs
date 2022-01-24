using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InputNeuron : INeuron
{
    public Guid Id { get; private set; }
    public string name;
    public InputSynapse input {get; set;}
    public List<Synapse> outputs {get; set;}
    public IActivationFunction activationFunction;
    public IInputFunction inputFunction;

    public InputNeuron(IActivationFunction activationFunction, IInputFunction inputFunction) {
        this.Id = Guid.NewGuid();
        this.name = Id.ToString();
        this.input = new InputSynapse(this);
        this.outputs = new List<Synapse>();
        this.activationFunction = activationFunction;
        this.inputFunction = inputFunction;
    }

    public InputNeuron(IActivationFunction activationFunction, IInputFunction inputFunction, String name) {
        this.Id = Guid.NewGuid();
        this.name = name;
        this.input = new InputSynapse(this);
        this.outputs = new List<Synapse>();
        this.activationFunction = activationFunction;
        this.inputFunction = inputFunction;
    }

    public void UpdateInput(Double inputValue) { 
        this.input = new InputSynapse(this, inputValue);
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

    public void SetInputSynapse(double inputValue) {
        InputSynapse inputSynapse = new InputSynapse(this, inputValue);
        this.input = inputSynapse; 
    }

    public double CalculateOutput() {
        return this.activationFunction.CalculateOutput(inputFunction.CalculateInput(this.input));
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
