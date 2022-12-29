namespace Core.Exceptions
{
    public class PlanoContaExeception
        : Exception
    {
        public int ErrorCode { get; private set; }
        public PlanoContaExeception() { }

        public PlanoContaExeception(string message)
            : base(message)
        {

        }

        public PlanoContaExeception(int errorCode, string message)
            : base(message)
        {
            ErrorCode = errorCode;
        }
    }
}
