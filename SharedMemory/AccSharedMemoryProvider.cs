using System.IO.MemoryMappedFiles;
using static System.IO.MemoryMappedFiles.MemoryMappedFile;

namespace Acc.Lib.SharedMemory;

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