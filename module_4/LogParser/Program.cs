using System;
using System.IO;
using System.Text;
using MSUtil;

namespace LogParser
{
    internal class Program
    {
        private static readonly LogQueryClassClass LogParser = new LogQueryClassClass();
        private static readonly COMCSVInputContextClassClass InputContext = new COMCSVInputContextClassClass();

        private static void Main(string[] args)
        {
            var filePath = args[0];

            try
            {
                var levelCountSql = $"Select Level, Count(*) AS CountMessages From {filePath} GROUP BY Level";
                var errorMessagesSql = $"Select Time, Level, Message From {filePath} WHERE Level = 'ERROR'";

                var isExists = File.Exists(filePath);

                if (!isExists)
                {
                    Console.WriteLine("Incorrect file path");
                    return;
                }

                Console.WriteLine(GetLogParserResult(levelCountSql));
                Console.WriteLine(GetLogParserResult(errorMessagesSql));
                
            }
            catch (System.Runtime.InteropServices.COMException exc)
            {
                Console.WriteLine("Unexpected error: " + exc.Message);
            }
        }

        private static string GetLogParserResult(string sqlString)
        {
            var stringBuilder = new StringBuilder();
            var logRecordset = LogParser.Execute(sqlString, InputContext);

            while (!logRecordset.atEnd())
            {
                var logRecord = logRecordset.getRecord();
                stringBuilder.AppendLine(logRecord.toNativeString(";"));
                logRecordset.moveNext();
            }

            return stringBuilder.ToString();
        }
    }
}