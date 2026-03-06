using AXExpansion.AXHelper.Extensions;
using Microsoft.Data.Sqlite;

namespace AXExpansion.Miscellaneous;

public static class MiscellaneousExpansions
{
    extension(string pathToDb)
    {
        public string CreateDirectoryOfDataSource()
        {
            var pathToCreate = new SqliteConnectionStringBuilder(pathToDb).DataSource
                .Split(['/', '\\'], StringSplitOptions.TrimEntries).RemoveLast().Combine(Path.DirectorySeparatorChar); 
            Directory.CreateDirectory(pathToCreate);
            return pathToDb;
        }
    }
}