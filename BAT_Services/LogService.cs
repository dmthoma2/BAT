using BAT_Models.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BAT_Models.Exceptions;
using BAT_Repository;

namespace BAT_Services
{
    public interface ILogService
    {
        string ConvertLogRecordToString(LogEntry logEntry);
        LogEntry GetLogEntry(string LogRecord);
        List<LogEntry> GetLog(List<string> LogRecords);
        List<string> ReadLogRecords(string FileName);
        void WriteLogRecord(string FileName, LogEntry entry);

    }//ILogService

    public class LogService : ILogService
    {
        private IFileIORepository _iFileIORepository;

        public LogService(IFileIORepository iFileIORepository)
        {
            _iFileIORepository = iFileIORepository;
        }//LogService


        /// <summary>
        /// Converts a LogEntry object into a string representation for writing to disk.
        /// </summary>
        public string ConvertLogRecordToString(LogEntry logEntry)
        {
            var sb = new StringBuilder();

            sb.Append(logEntry.TransactionGroup.ToString());
            sb.Append(LogEntry.DELIMITER);
            sb.Append(logEntry.TransactionTime.ToString());
            sb.Append(LogEntry.DELIMITER);
            sb.Append(logEntry.Symbol.ToString());
            sb.Append(LogEntry.DELIMITER);

            if (logEntry.HasError())
            {
                sb.Append(logEntry.ErrorMessage.ToString());
                sb.Append(LogEntry.DELIMITER);
                return sb.ToString();
            }//if

            sb.Append(logEntry.TradeType.ToString());
            sb.Append(LogEntry.DELIMITER);
            sb.Append(logEntry.Quantity.ToString());
            sb.Append(LogEntry.DELIMITER);
            sb.Append(logEntry.Price.ToString());
            sb.Append(LogEntry.DELIMITER);
            sb.Append(logEntry.Description.ToString());
            sb.Append(LogEntry.DELIMITER);

            return sb.ToString();
        }//ConvertLogRecordToString

        /// <summary>
        /// Converts a single string representation of a log record into a LogEntry object.
        /// </summary>
        public LogEntry GetLogEntry(string LogRecord)
        {
            var output = new LogEntry();

            if (string.IsNullOrWhiteSpace(LogRecord))
            { return output; }//if

            var tokens = LogRecord.Split(LogEntry.DELIMITER);

            output.TransactionGroup = Guid.Parse(tokens[0]);
            output.TransactionTime = DateTime.Parse(tokens[1]);
            output.Symbol = tokens[2];

            if (output.HasError())
            {
                output.ErrorMessage = tokens[3];
                return output;
            }//if

            output.TradeType = tokens[3];
            output.Quantity = decimal.Parse(tokens[4]);
            output.Price = decimal.Parse(tokens[5]);
            output.Description = tokens[6];

            return output;
        }//GetLogEntry

        /// <summary>
        /// Converts a list of string representations of log records into a collection of LogEntry objects.
        /// </summary>
        public List<LogEntry> GetLog(List<string> LogRecords)
        {
            var output = new List<LogEntry>();

            if (LogRecords == null)
            { return output; }//if
                    
            foreach(var record in LogRecords)
            {
                output.Add(GetLogEntry(record));
            }//foreach

            return output;            
        }//GetLog

        /// <summary>
        /// Reads all records of the specified log file. 
        /// </summary>
        public List<string> ReadLogRecords(string FileName)
        {
            if (string.IsNullOrWhiteSpace(FileName))
            { return new List<string>(); }//if
            
            return _iFileIORepository.ReadAllLinesFromFile(FileName);          
        }//ReadLogRecords

        /// <summary>
        /// Writes a record to the specified log file.  Records will be written or  an exception thrown.
        /// </summary>
        public void WriteLogRecord(string FileName, LogEntry entry)
        {
            if (string.IsNullOrWhiteSpace(FileName))
            { throw new LogException("No log file was provided.  Unable to write records."); }//if

            var records = new List<string>();
            records.Add(ConvertLogRecordToString(entry));

            _iFileIORepository.AppendAllLinesToFile(FileName, records);
        }//WriteLogRecord

    }//LogService
}
