using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class WeightedSums : IInputFunction {
	public double CalculateInput(List<Synapse> inputSynapses) {
		return inputSynapses.Select(x=>x.weight * x.GetOutput()).Sum();
	}
	public double CalculateInput(InputSynapse inputSynapse) {
		return inputSynapse.GetOutput();
	}
}
