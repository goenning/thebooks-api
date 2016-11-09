using System;

namespace TheBooks.Api.Utils
{ 
    public static class DbHelpers
    {
        public static string FormatUrlToConnectionString(string databaseUrl)
        {
            var connectionStringFormat = "Host={0};Port={1};Database={2};Username={3};Password={4};";
            var url = new Uri(databaseUrl);
            var userInfo = url.UserInfo.Split(new[] { ':' }, 2);
            var userName = userInfo[0];
            var password = userInfo[1];
            var host = url.Host;
            var port = url.Port;
            var databaseName = url.AbsolutePath.Remove(0, 1);
            return string.Format(connectionStringFormat, host, port, databaseName, userName, password);
        }
    }
}