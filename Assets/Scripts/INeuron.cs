using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface INeuron
{
    Guid Id { get; }
    List<ISynapse> outputs { get; set; }
    void AddOutputNeuron(Neuron inputNeuron);
    double CalculateOutput();
}