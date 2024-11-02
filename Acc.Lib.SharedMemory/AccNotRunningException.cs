using System;

namespace Acc.Lib.SharedMemory;

/// <summary>
/// Some code in this namespace is based on code from https://github.com/RiddleTime/Race-Element, which is in turn based on code from other open source repositories
/// </summary>
public class AccNotRunningException : Exception
{
    public AccNotRunningException() :
        base("Unable to connect to ACC Shared Memory. Please start ACC before using this application or feature.") { }
}