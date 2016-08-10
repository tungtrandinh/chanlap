namespace ChanLap.Infrastructure
{
    public class DatabaseFactory : Disposable, IDatabaseFactory
    {
        public ChanLapEntity dataContext;
        public ChanLapEntity Get()
        {
            return dataContext ?? (dataContext = new ChanLapEntity());
        }
        protected override void DisposeCore()
        {
            if (dataContext != null)
                dataContext.Dispose();
        }
    }
}
