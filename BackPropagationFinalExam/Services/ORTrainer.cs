using BackPropagationFinalExam.Interfaces;

namespace BackPropagationFinalExam.Services
{
    public class ORTrainer
    {
        private readonly INeuralNetwork _network;

        public ORTrainer(INeuralNetwork network)
        {
            _network = network;
        }

        public void TrainORGate()
        {
            double[][] inputs = new double[][]
            {
                new double[] { 0, 0 },
                new double[] { 0, 1 },
                new double[] { 1, 0 },
                new double[] { 1, 1 }
            };
            double[] outputs = { 0, 1, 1, 1 };
            _network.Train(inputs, outputs, 10000);
        }
    }
}
