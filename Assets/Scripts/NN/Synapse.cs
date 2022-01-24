using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Synapse : ISynapse{
	public INeuron fromNeuron {get; set;}
	public Neuron toNeuron {get; set;}

	public double weight {get; set;}

	public Synapse(INeuron fromNeuron, Neuron toNeuron, double weight) {
		this.fromNeuron = fromNeuron;
		this.toNeuron = toNeuron;

		this.weight = weight;
	}

	public Synapse(INeuron fromNeuron, Neuron toNeuron) {
		this.fromNeuron = fromNeuron;
		this.toNeuron = toNeuron;

		this.weight = Random.Range(-1f, 1f);
	}

	public double GetOutput(){
		return fromNeuron.CalculateOutput();
	}
	
	public void UpdateWeight(double weight) {
		this.weight = weight;
	}
}
