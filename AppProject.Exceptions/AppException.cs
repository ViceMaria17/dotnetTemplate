using System;

namespace AppProject.Exceptions;

public class AppException : Exception
{
    public AppException(
        ExceptionCode exceptionCode = ExceptionCode.Generic, string? aditionalInfo = null, Exception? innerException = null)
        : base(innerException?.Message, innerException)
    {
        this.ExceptionCode = exceptionCode;
        this.AddtionalInfo = aditionalInfo;
    }

    public ExceptionCode ExceptionCode { get; }

    public string? AddtionalInfo { get; }
}
