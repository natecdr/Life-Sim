using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class WeightedSums : IInputFunction {
	public double CalculateInput(List<ISynapse> inputSynapses) {
		return inputSynapses.Select(x=>x.weight * x.GetOutput()).Sum();
	}
}
