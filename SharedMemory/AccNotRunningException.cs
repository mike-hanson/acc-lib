namespace Acc.Lib.SharedMemory;

public class AccNotRunningException : Exception
{
    public AccNotRunningException() :
        base("Unable to connect to ACC Shared Memory. Please start ACC before using this application or feature.") { }
}