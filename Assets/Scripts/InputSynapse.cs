using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InputSynapse : ISynapse{
  internal Neuron toNeuron;
  public double weight {get; set;}
  public double output {get; set;}

  public InputSynapse(Neuron toNeuron) {
    this.toNeuron = toNeuron;
    this.weight = 1;
  }

  public InputSynapse(Neuron toNeuron, double output) {
    this.toNeuron = toNeuron;
    this.output = output;
    this.weight = 1;
  }

  public double GetOutput(){
    return output;
  }
}
