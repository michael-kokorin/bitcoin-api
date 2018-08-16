namespace BitcoinApi.Shared
{
    public class OperationResult<T>: OperationResult
    {
        public new T Data { get; set; }
    }
}