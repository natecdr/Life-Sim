using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Synapse : ISynapse{
	internal INeuron fromNeuron;
	internal Neuron toNeuron;

	public double weight {get; set;}

	public Synapse(INeuron fromNeuron, Neuron toNeuron, double weight) {
		this.fromNeuron = fromNeuron;
		this.toNeuron = toNeuron;

		this.weight = weight;
	}

	public Synapse(INeuron fromNeuron, Neuron toNeuron) {
		this.fromNeuron = fromNeuron;
		this.toNeuron = toNeuron;

		System.Random tmpRandom = new System.Random();
		this.weight = tmpRandom.NextDouble()*2-1;
	}

	public double GetOutput(){
		return fromNeuron.CalculateOutput();
	}
	
	public void UpdateWeight(double weight) {
		this.weight = weight;
	}
}
