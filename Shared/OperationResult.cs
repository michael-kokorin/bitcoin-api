﻿namespace BitcoinApi.Shared
{
    public class OperationResult
    {
        public bool Success { get; set; }

        public string Message { get; set; }

        public object Data { get; set; }
    }
}