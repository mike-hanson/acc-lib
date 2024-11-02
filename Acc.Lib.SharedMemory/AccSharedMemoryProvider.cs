using System;
using System.IO.MemoryMappedFiles;
using Acc.Lib.SharedMemory.ACC;
using Acc.Lib.SharedMemory.Models;
using static System.IO.MemoryMappedFiles.MemoryMappedFile;

namespace Acc.Lib.SharedMemory;

/// <summary>
/// Some code in this namespace is based on code from https://github.com/RiddleTime/Race-Element, which is in turn based on code from other open source repositories
/// </summary>
public static class AccSharedMemoryProvider
{
    private const string GraphicsMap = "Local\\acpmf_graphics";
    private const string PhysicsMap = "Local\\acpmf_physics";
    private const string StaticMap = "Local\\acpmf_static";

    public static GraphicsData ReadGraphicsData()
    {
        var mappedFile = CreateOrOpen(GraphicsMap, sizeof(byte), MemoryMappedFileAccess.ReadWrite);
        return new GraphicsData(mappedFile.ToStruct<GraphicsPage>(GraphicsPage.Buffer));
    }

    public static PhysicsData ReadPhysicsData()
    {
        var mappedFile = CreateOrOpen(PhysicsMap, sizeof(byte), MemoryMappedFileAccess.ReadWrite);
        return new PhysicsData(mappedFile.ToStruct<PhysicsPage>(PhysicsPage.Buffer));
    }

    public static StaticData ReadStaticData()
    {
        var mappedFile = CreateOrOpen(StaticMap, sizeof(byte), MemoryMappedFileAccess.ReadWrite);
        return new StaticData(mappedFile.ToStruct<StaticDataPage>(StaticDataPage.Buffer));
    }
}