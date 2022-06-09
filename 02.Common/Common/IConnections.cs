namespace Commons
{
    public class IConnections
    {
        private static string _Server;
        private static string _Database;
        private static string _Username;
        private static string _Password;

        public static string Server
        {
            get
            {
                return _Server;
            }
            set
            {
                _Server = value;
            }
        }
        public static string Database
        {
            get
            {
                return _Database;
            }
            set
            {
                _Database = value;
            }
        }
        public static string Username
        {
            get
            {
                return _Username;
            }
            set
            {
                _Username = value;
            }
        }
        public static string Password
        {
            get
            {
                return _Password;
            }
            set
            {
                _Password = value;
            }
        }
      
        public static string CNStr
        {
            get
            {
                return "Server=" + _Server + ";database=" + _Database + ";uid=" + _Username + ";pwd=" + _Password + ";Connect Timeout=9999;";
            }
        }
    }
}
