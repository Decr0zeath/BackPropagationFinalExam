using System;
using System.Windows.Forms;

namespace BackPropagationFinalExam
{
    public partial class Form1 : Form
    {
        // Network configuration
        const int inputNodes = 2;
        const int hiddenNodes = 2;
        const int outputNodes = 1;
        const double learningRate = 0.5;

        double[,] weightsInputHidden = new double[inputNodes, hiddenNodes];
        double[] weightsHiddenOutput = new double[hiddenNodes];
        Random rand = new Random();

        public Form1()
        {
            InitializeComponent();
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

        private void btnTrain_Click(object sender, EventArgs e)
        {
            // OR gate training set
            double[][] inputs = new double[][]
            {
                new double[] { 0, 0 },
                new double[] { 0, 1 },
                new double[] { 1, 0 },
                new double[] { 1, 1 }
            };
            double[] targets = { 0, 1, 1, 1 };

            for (int epoch = 0; epoch < 10000; epoch++)
            {
                for (int i = 0; i < inputs.Length; i++)
                    Train(inputs[i], targets[i]);
            }

            MessageBox.Show("Training complete.");
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            try
            {
                double i1 = double.Parse(cmbInput1.Text);
                double i2 = double.Parse(cmbInput2.Text);

                double output = Predict(new double[] { i1, i2 });
                lblOutput.Text = $"Output: {Math.Round(output, 3)}";
            }
            catch
            {
                MessageBox.Show("Please enter valid numeric inputs (0 or 1).");
            }

        }


        private void Train(double[] input, double target)
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

        private double Predict(double[] input)
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
