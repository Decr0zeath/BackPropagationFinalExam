
namespace BackPropagationFinalExam.Interfaces
{
    public interface INeuralNetwork
    {
        void Train(double[][] inputs, double[] targets, int epochs);
        double Predict(double[] input);
    }
}