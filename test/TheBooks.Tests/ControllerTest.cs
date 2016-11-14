using System.Data.Common;
using Npgsql;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using FluentAssertions;
using FluentAssertions.Mvc;

namespace TheBooks.Tests
{
    public abstract class ControllerTest<T> : IDisposable where T : Controller
    {
        protected DbConnection connection;

        protected T controller;

        protected ControllerTest()
        {
            Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;
            var connectionString = @"Server=localhost;Port=5556;Database=thebooks-test;User Id=thebooks-test;Password=thebooks-test-pw;";
            this.connection = new NpgsqlConnection(connectionString);
            this.RunNpm("migrate-reset -- -e test");
            this.RunNpm("migrate-up -- -e test");
        }

        protected void RunNpm(string command) 
        {
            var info = new ProcessStartInfo();
            info.FileName = "npm";
            info.Arguments = $"run {command}";
            info.RedirectStandardOutput = true;
            info.UseShellExecute = false;
            var process = new Process();
            process.StartInfo = info;
            process.Start();
            // Console.Write(process.StandardOutput.ReadToEnd());
            process.WaitForExit();
        }

        protected async Task ExecuteAction(Func<T, Task<IActionResult>> action)
        {
            var result = await action(this.controller);
            result.Should().BeOfType<OkResult>();
        }

        protected async Task<K> ExecuteAction<K>(Func<T, Task<IActionResult>> action)
        {
            var result = await action(this.controller);
            var ok = result.Should().BeOfType<OkObjectResult>().Subject;
            return ok.Value.Should().BeAssignableTo<K>().Subject;
        }
        
        public void Dispose()
        {
            this.connection.Dispose();
        }
    }
}