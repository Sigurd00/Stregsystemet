namespace Stregsystemet
{
    public interface ILogger
    {
        void Info(string message);
        void Warn(string message);
        void Error(string message);
        void Fatal(string message);
        void OnCommandEntered(string command);
    }
}