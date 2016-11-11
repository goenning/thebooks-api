using System;
using System.Data.Common;
using Npgsql;
using System.Diagnostics;
namespace TheBooks.Tests
{
  public abstract class DbTest : IDisposable
  {
      protected DbConnection connection;

      protected DbTest()
      {
          var connectionString = @"Server=localhost;Port=5556;Database=thebooks-test;User Id=thebooks-test;Password=thebooks-test-pw;";
          this.connection = new NpgsqlConnection(connectionString);
          this.RunNpm("migrate-reset -- -e test");
          this.RunNpm("migrate-up -- -e test");
      }

      private void RunNpm(string command) 
      {
          var info = new ProcessStartInfo();
          info.FileName = "npm";
          info.Arguments = $"run {command}";
          info.RedirectStandardOutput = true;
          info.UseShellExecute = false;
          var process = new Process();
          process.StartInfo = info;
          process.Start();
          Console.Write(process.StandardOutput.ReadToEnd());
          process.WaitForExit();
      }

      public void Dispose()
      {
          this.connection.Dispose();
      }
  }
}