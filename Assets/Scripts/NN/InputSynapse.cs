using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InputSynapse : ISynapse{
  public INeuron toNeuron {get; set;}
  public double weight {get; set;}
  public double output {get; set;}

  public InputSynapse(InputNeuron toNeuron) {
    this.toNeuron = toNeuron;
    this.weight = 1;
  }

  public InputSynapse(InputNeuron toNeuron, double output) {
    this.toNeuron = toNeuron;
    this.output = output;
    this.weight = 1;
  }

  public double GetOutput(){
    return output;
  }
}
