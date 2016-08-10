using System;

namespace ChanLap.Infrastructure
{
    public interface IDatabaseFactory : IDisposable
    {
        ChanLapEntity Get();
    }
}
