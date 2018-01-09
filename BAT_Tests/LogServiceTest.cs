using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BAT_Services;
using Moq;
using BAT_Repository;
using BAT_Models.Log;
using BAT_Models.API;
using BAT_Models.Exceptions;
using System.Collections.Generic;

namespace BAT_Tests
{
    [TestClass]
    public class LogServiceTest
    {
        LogService _logService;
        Mock<IFileIORepository> _IORepo;

        [TestInitialize]
        public void Initialize()
        {
            _IORepo = new Mock<IFileIORepository>();
            _logService = new LogService(_IORepo.Object);
        }//Initialize

        private LogEntry GetDefaultLogEntry()
        {
            return new LogEntry()
            {
                TransactionGroup = Guid.Parse("54f470a5-4c9f-4409-a03c-4dcac14a8efb"),
                TransactionTime = new DateTime(),
                Symbol = "XRP",
                TradeType = Trade.TradeTypes.BUY,
                Quantity = 12,
                Price = 12.34m,
                Description = "'REBALANCE' Algorithm executed a buy order of 12 units."
            };
        }

        private LogEntry GetDefaultErrorLogEntry()
        {
            return new LogEntry()
            {
                TransactionGroup = Guid.Parse("54f470a5-4c9f-4409-a03c-4dcac14a8efb"),
                TransactionTime = new DateTime(),
                Symbol = LogEntry.ERROR_CODE,
                ErrorMessage = "API was unable to connect to https://api.binance.com/"
            };
        }//GetDefaultErrorLogEntry

        private string GetDefaultLogRecord()
        {
            return "54f470a5-4c9f-4409-a03c-4dcac14a8efb|1/1/0001 12:00:00 AM|XRP|BUY|12|12.34|'REBALANCE' Algorithm executed a buy order of 12 units.|";
        }//GetDefaultLogRecord

        private string GetDefaultErrorLogRecord()
        {
            return "54f470a5-4c9f-4409-a03c-4dcac14a8efb|1/1/0001 12:00:00 AM|ERROR|API was unable to connect to https://api.binance.com/|";
        }//GetDefaultLogRecord

        [TestMethod]
        public void WriteLogRecordTest()
        {
            _logService.WriteLogRecord("filename.txt", GetDefaultLogEntry());

            try
            { _logService.WriteLogRecord(null, GetDefaultLogEntry()); ; Assert.Fail(); }
            catch (LogException ce)
            { Assert.IsTrue(ce.Message.Contains("No log file was provided.  Unable to write records.")); }//catch
            
        }//WriteLogRecordTest

        [TestMethod]
        public void ConvertLogRecordToStringTest()
        {
            var parsedString = _logService.ConvertLogRecordToString(GetDefaultLogEntry());

            Assert.AreEqual(GetDefaultLogRecord(), parsedString);

            parsedString = _logService.ConvertLogRecordToString(GetDefaultErrorLogEntry());

            Assert.AreEqual(GetDefaultErrorLogRecord(), parsedString);
        }//ConvertLogRecordToStringTest

        [TestMethod]
        public void ReadLogRecordsTest()
        {
            var records = _logService.ReadLogRecords(null);

            Assert.AreEqual(0, records.Count);

            var recordsToReturn = new List<string>();
            recordsToReturn.Add(GetDefaultLogRecord());
            recordsToReturn.Add(GetDefaultErrorLogRecord());
            _IORepo.Setup(x => x.ReadAllLinesFromFile(It.IsAny<string>())).Returns(recordsToReturn);
            records = _logService.ReadLogRecords("example.txt");

            Assert.AreEqual(2, records.Count);
            Assert.AreEqual(GetDefaultLogRecord(), records[0]);
            Assert.AreEqual(GetDefaultErrorLogRecord(), records[1]);
        }//GetLogEntryTest

        [TestMethod]
        public void GetLogEntryTest()
        {
            var logEntry = _logService.GetLogEntry(GetDefaultLogRecord());

            var defaultLogEntry = GetDefaultLogEntry();

            Assert.AreEqual(defaultLogEntry.TransactionGroup, logEntry.TransactionGroup);
            Assert.AreEqual(defaultLogEntry.TransactionTime, logEntry.TransactionTime);
            Assert.AreEqual(defaultLogEntry.Symbol, logEntry.Symbol);
            Assert.AreEqual(defaultLogEntry.TradeType, logEntry.TradeType);
            Assert.AreEqual(defaultLogEntry.Quantity, logEntry.Quantity);
            Assert.AreEqual(defaultLogEntry.Price, logEntry.Price);
            Assert.AreEqual(defaultLogEntry.Description, logEntry.Description);
            Assert.AreEqual(defaultLogEntry.ErrorMessage, logEntry.ErrorMessage);
            Assert.AreEqual(defaultLogEntry.HasError(), logEntry.HasError());

            var logErrorEntry = _logService.GetLogEntry(GetDefaultErrorLogRecord());

            var defaultLogErrorEntry = GetDefaultErrorLogEntry();

            Assert.AreEqual(defaultLogErrorEntry.TransactionGroup, logErrorEntry.TransactionGroup);
            Assert.AreEqual(defaultLogErrorEntry.TransactionTime, logErrorEntry.TransactionTime);
            Assert.AreEqual(defaultLogErrorEntry.Symbol, logErrorEntry.Symbol);
            Assert.AreEqual(defaultLogErrorEntry.TradeType, logErrorEntry.TradeType);
            Assert.AreEqual(defaultLogErrorEntry.Quantity, logErrorEntry.Quantity);
            Assert.AreEqual(defaultLogErrorEntry.Price, logErrorEntry.Price);
            Assert.AreEqual(defaultLogErrorEntry.Description, logErrorEntry.Description);
            Assert.AreEqual(defaultLogErrorEntry.ErrorMessage, logErrorEntry.ErrorMessage);
            Assert.AreEqual(defaultLogErrorEntry.HasError(), logErrorEntry.HasError());

        }//GetLogEntryTest

        [TestMethod]
        public void GetLogTest()
        {
            var lines = new List<string>();
            lines.Add(GetDefaultLogRecord());
            var records = _logService.GetLog(lines);

            Assert.IsNotNull(records);
            Assert.AreEqual(1, records.Count);

            records = _logService.GetLog(null);

            Assert.IsNotNull(records);
            Assert.AreEqual(0, records.Count);

            lines = new List<string>();
            lines.Add(GetDefaultLogRecord());
            lines.Add(GetDefaultErrorLogRecord());
            records = _logService.GetLog(lines);

            Assert.IsNotNull(records);
            Assert.AreEqual(2, records.Count);
        }//GetLogEntryTest              
    }
}
