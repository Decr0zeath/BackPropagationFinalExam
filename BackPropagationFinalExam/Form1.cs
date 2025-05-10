using BackPropagationFinalExam.Interfaces;
using BackPropagationFinalExam.Models;
using BackPropagationFinalExam.Services;
using System;
using System.Windows.Forms;

namespace BackPropagationFinalExam
{
    public partial class Form1 : Form
    {
        private readonly INeuralNetwork network;
        private readonly ORTrainer trainer;

        public Form1()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            network = new NeuralNetwork();
            trainer = new ORTrainer(network);
        }

        private void btnTrain_Click(object sender, EventArgs e)
        {
            trainer.TrainORGate();
            MessageBox.Show("Training complete.");
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            try
            {
                double i1 = double.Parse(cmbInput1.Text);
                double i2 = double.Parse(cmbInput2.Text);

                double output = network.Predict(new double[] { i1, i2 });
                lblOutput.Text = $"Output: {Math.Round(output, 3)}";
            }
            catch
            {
                MessageBox.Show("Please enter valid inputs.");
            }
        }
    }
}
