using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AliaSQL.Console;
using AliaSQL.Core;
using AliaSQL.Core.Model;
using AliaSQL.Core.Services.Impl;
using NUnit.Framework;
using Should;

namespace AliaSQL.IntegrationTests
{
  [TestFixture]
  public class LongRunningScriptTester
  {
    [Test]
    public void Should_Not_Time_Out_When_Timeout_Value_High_Enough()
    {
      //arrange
      string scriptsDirectory = Path.Combine("Scripts", GetType().Name.Replace("Tester", ""));

      string scriptFileMd5 =
        ChangeScriptExecutor.GetFileMD5Hash(Path.Combine(scriptsDirectory, "Update", "LongRunningScript.sql"));
      var settings = new ConnectionSettings(".\\sqlexpress", "aliasqltest", true, null, null, TimeSpan.FromSeconds(10));
      new ConsoleAliaSQL().UpdateDatabase(settings, scriptsDirectory, RequestedDatabaseAction.Drop);

      //act
      bool success = new ConsoleAliaSQL().UpdateDatabase(settings, scriptsDirectory, RequestedDatabaseAction.Update);

      //assert
      success.ShouldEqual(true);
    }

    [Test]
    public void Should_Time_Out_When_Timeout_Value_Is_Not_High_Enough()
    {
      //arrange
      string scriptsDirectory = Path.Combine("Scripts", GetType().Name.Replace("Tester", ""));

      string scriptFileMd5 =
        ChangeScriptExecutor.GetFileMD5Hash(Path.Combine(scriptsDirectory, "Update", "LongRunningScript.sql"));
      var settings = new ConnectionSettings(".\\sqlexpress", "aliasqltest", true, null, null, TimeSpan.FromSeconds(1));
      new ConsoleAliaSQL().UpdateDatabase(settings, scriptsDirectory, RequestedDatabaseAction.Drop);

      //act
      bool success = new ConsoleAliaSQL().UpdateDatabase(settings, scriptsDirectory, RequestedDatabaseAction.Update);

      //assert
      success.ShouldEqual(false);
    }
  }
}
