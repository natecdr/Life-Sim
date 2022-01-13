using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Synapse : ISynapse{
	internal Neuron fromNeuron;
	internal Neuron toNeuron;

	public double weight {get; set;}

	public Synapse(Neuron fromNeuron, Neuron toNeuron, double weight) {
		this.fromNeuron = fromNeuron;
		this.toNeuron = toNeuron;

		this.weight = weight;
	}

	public Synapse(Neuron fromNeuron, Neuron toNeuron) {
		this.fromNeuron = fromNeuron;
		this.toNeuron = toNeuron;

		System.Random tmpRandom = new System.Random();
		this.weight = tmpRandom.NextDouble();
	}

	public double GetOutput(){
		return fromNeuron.CalculateOutput();
	}
	public void UpdateWeight(double weight) {
		this.weight = weight;
	}
}
