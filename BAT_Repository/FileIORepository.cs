using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace BAT_Repository
{
    public interface IFileIORepository
    {
        void AppendAllLinesToFile(string FileName, List<string> Lines);

        List<string> ReadAllLinesFromFile(string FileName);

    }//IFileIORepository

    /// <summary>
    /// Contains methods associated with reading and writing to files.
    /// </summary>
    public class FileIORepository : IFileIORepository
    {

        public void AppendAllLinesToFile(string FileName , List<string> Lines)
        {
            //Create this file if it is not currently on disk.
            if (!File.Exists(FileName))
            { File.Create(FileName); }//if

            File.AppendAllLines(FileName, Lines);
        }//AppendAllLinesToFile

        public List<string> ReadAllLinesFromFile(string FileName)
        {
            //Create this file if it is not currently on disk.
            if (!File.Exists(FileName))
            { return new List<string>(); }//if
            
            return File.ReadAllLines(FileName).ToList();
        }//ReadAllLinesFromFile
    }//FileIORepository
}
