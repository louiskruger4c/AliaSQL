using System;
using System.Collections.Generic;
using AliaSQL.Core.Model;
using AliaSQL.Core.Services;

namespace AliaSQL.Core
{
  public interface IDbUpdater : ITaskObserver
  {
    new void Log(string message);
    new void SetVariable(string name, string value);
    AliaSqlResult UpdateDatabase(string connectionString, TimeSpan scriptTimeout, RequestedDatabaseAction action, string scriptDirectory = "");
    List<string> PendingChanges(string connectionString, TimeSpan scriptTimeout, string scriptDirectory = "");
    List<string> PendingTestData(string connectionString, TimeSpan scriptTimeout, string scriptDirectory = "");
    bool DatabaseExists(string connectionString, TimeSpan scriptTimeout);
    int DatabaseVersion(string connectionString, TimeSpan scriptTimeout);
  }
}