namespace AutoMail.Infrastructure
{
    public static class Const
    {
        private static bool _initDataBase = false;
        private static readonly object _lock = new object();

        public static bool InitDataBase
        {
            get
            {
                lock (_lock)
                {
                    return _initDataBase;
                }
            }
            set
            {
                lock (_lock)
                {
                    _initDataBase = value;
                }
            }
        }
    }
}
