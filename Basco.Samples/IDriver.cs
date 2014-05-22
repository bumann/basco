namespace Basco.Samples
{
    public interface IDriver
    {
        void Connect();

        void Disconnect();

        void RunProgram();

        void ProduceError();

        void ResetError();
    }
}