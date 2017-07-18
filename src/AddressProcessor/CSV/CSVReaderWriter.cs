using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace AddressProcessing.CSV
{
    /*
        2) Refactor this class into clean, elegant, rock-solid & well performing code, without over-engineering.
           Assume this code is in production and backwards compatibility must be maintained.
    */

    public class CSVReaderWriter
    {
        //null is a default value, therefore it is not needed
        private StreamReader _readerStream;
        private StreamWriter _writerStream;

        [Flags]
        public enum Mode { Read = 1, Write = 2 };

        public void Open(string fileName, Mode mode)
        {
            if (mode == Mode.Read)
            {
                _readerStream = File.OpenText(fileName);
            }
            else 
            {
                var fileInfo = new FileInfo(fileName);
                _writerStream = fileInfo.CreateText();
            }
        }


        public void Write(params string[] columns)
        {
            string outPut = string.Join("\t", columns);          
            WriteLine(outPut);
        }


        //public bool Read(params string[] inputColumns)
        //{
        //    char[] separator = { '\t' };

        //    var line = ReadLine();

        //    var columns = line?.Split(separator);

        //    if (!(columns?.Length >= inputColumns.Length)) return false;
        //    for (var i = 0; i < inputColumns.Length; i++)
        //    {
        //        inputColumns[i] = columns[i];
        //    }
        //    return true;
        //}


        //public bool Read(out string column1, out string column2)
        //{
        //    string a = column1;
        //    Debug.Assert(column1 != null, message: "column1 != null");
        //    string[] columns2 = new [] { column1, column2, "d", null};


        //    List<string> list =new List<string>() {column1};
        //    Read(column1, column2);
        //    const int FIRST_COLUMN = 0;
        //    const int SECOND_COLUMN = 1;

        //    string line;
        //    string[] columns;

        //    char[] separator = { '\t' };

        //    line = ReadLine();

        //    if (line == null)
        //    {
        //        column1 = null;
        //        column2 = null;

        //        return false;
        //    }

        //    columns = line.Split(separator);

        //    if (columns.Length == 0)
        //    {
        //        column1 = null;
        //        column2 = null;

        //        return false;
        //    } 
        //    else
        //    {
        //        column1 = columns[FIRST_COLUMN];
        //        column2 = columns[SECOND_COLUMN];

        //        return true;
        //    }
        //}


      //  public bool Read(params string[] inputColumns)
          public bool Read(string column1=null, string column2=null)
        {
            char[] separator = { '\t' };

            var line = ReadLine();
            var columns = line.Split(separator);

            return columns.Length != 0;         
        }


        public bool Read(out string column1, out string column2)
        {
           // new CSVReaderWriter().Read("rrggj", "rrrr");
            const int firstColumn = 0;
            const int secondColumn = 1;

            char[] separator = { '\t' };

            var line = ReadLine();

            if (line == null)
            {
                column1 = null;
                column2 = null;

                return false;
            }

            var columns = line.Split(separator);

            if (columns.Length == 0)
            {
                column1 = null;
                column2 = null;

                return false;
            }

            column1 = columns[firstColumn];
            column2 = columns[secondColumn];

            return true;
        }

        private void WriteLine(string line)
        {
            _writerStream.WriteLine(line);
        }

        private string ReadLine()
        {
            return _readerStream.ReadLine();
        }

        public void Close()
        {
            _writerStream?.Close();          
            _readerStream?.Close();
        }


    }
}
