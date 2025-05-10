using BackPropagationFinalExam.Interfaces;
using System;

namespace BackPropagationFinalExam.Models
{
    public class NeuralNetwork : INeuralNetwork
    {
        private readonly int inputNodes = 2;
        private readonly int hiddenNodes = 2;
        private readonly int outputNodes = 1;
        private readonly double learningRate;

        private readonly double[,] weightsInputHidden;
        private readonly double[] weightsHiddenOutput;
        private readonly Random rand = new Random();

        public NeuralNetwork(double learningRate = 0.5)
        {
            this.learningRate = learningRate;
            weightsInputHidden = new double[inputNodes, hiddenNodes];
            weightsHiddenOutput = new double[hiddenNodes];
            InitializeWeights();
        }

        private void InitializeWeights()
        {
            for (int i = 0; i < inputNodes; i++)
                for (int j = 0; j < hiddenNodes; j++)
                    weightsInputHidden[i, j] = rand.NextDouble() - 0.5;

            for (int j = 0; j < hiddenNodes; j++)
                weightsHiddenOutput[j] = rand.NextDouble() - 0.5;
        }

        public void Train(double[][] inputs, double[] targets, int epochs)
        {
            for (int epoch = 0; epoch < epochs; epoch++)
                for (int i = 0; i < inputs.Length; i++)
                    TrainSingle(inputs[i], targets[i]);
        }

        private void TrainSingle(double[] input, double target)
        {
            double[] hiddenOutputs = new double[hiddenNodes];
            for (int j = 0; j < hiddenNodes; j++)
            {
                double sum = 0;
                for (int i = 0; i < inputNodes; i++)
                    sum += input[i] * weightsInputHidden[i, j];
                hiddenOutputs[j] = Sigmoid(sum);
            }

            double finalInput = 0;
            for (int j = 0; j < hiddenNodes; j++)
                finalInput += hiddenOutputs[j] * weightsHiddenOutput[j];
            double finalOutput = Sigmoid(finalInput);

            double error = target - finalOutput;
            double outputDelta = error * finalOutput * (1 - finalOutput);

            double[] hiddenDeltas = new double[hiddenNodes];
            for (int j = 0; j < hiddenNodes; j++)
            {
                hiddenDeltas[j] = outputDelta * weightsHiddenOutput[j] * hiddenOutputs[j] * (1 - hiddenOutputs[j]);
                weightsHiddenOutput[j] += learningRate * outputDelta * hiddenOutputs[j];
            }

            for (int i = 0; i < inputNodes; i++)
                for (int j = 0; j < hiddenNodes; j++)
                    weightsInputHidden[i, j] += learningRate * hiddenDeltas[j] * input[i];
        }

        public double Predict(double[] input)
        {
            double[] hiddenOutputs = new double[hiddenNodes];
            for (int j = 0; j < hiddenNodes; j++)
            {
                double sum = 0;
                for (int i = 0; i < inputNodes; i++)
                    sum += input[i] * weightsInputHidden[i, j];
                hiddenOutputs[j] = Sigmoid(sum);
            }

            double finalInput = 0;
            for (int j = 0; j < hiddenNodes; j++)
                finalInput += hiddenOutputs[j] * weightsHiddenOutput[j];
            return Sigmoid(finalInput);
        }

        private double Sigmoid(double x)
        {
            return 1.0 / (1.0 + Math.Exp(-x));
        }
    }
}
