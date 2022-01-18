using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface INeuron
{
    Guid Id { get; }
    List<Synapse> outputs { get; set; }
    void AddOutputNeuron(Neuron outputNeuron);
    void RemoveOutputNeuron(Neuron outputNeuron);
    double CalculateOutput();
    bool IsConnected(Neuron neuron);
    Synapse GetSynapse(Neuron neuron);
}