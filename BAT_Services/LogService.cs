using BAT_Models.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAT_Services
{
    public interface ILogService
    {

        LogEntry GetLogEntry(string LogRecord);
        List<LogEntry> GetLog(List<string> LogRecordes);
        bool WriteLogRecord(LogEntry entry);

    }//ILogService

    public class LogService : ILogService
    {

        public LogEntry GetLogEntry(string LogRecord)
        {

            //TODO

            return new LogEntry();
        }//GetLogEntry

        public List<LogEntry> GetLog(List<string> LogRecordes)
        {

            //TODO

            return new List<LogEntry>();
        }//GetLog

        public bool WriteLogRecord(LogEntry entry)
        {

            //TODO

            return false;
        }//WriteLogRecord

    }//LogService
}
