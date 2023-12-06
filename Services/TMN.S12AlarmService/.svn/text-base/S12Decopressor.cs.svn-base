using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Enterprise;
using System.Threading;

namespace TMN
{
    public class  S12Decompressor  
    {

        //const string mirrorFile = @"C:\Windows\Tmn_Alarm_Log.txt";
        public int lastFilePos
        {
            get
            {
                return int.Parse(TextSettings.Get("V2_" + _path.Replace('.','_').Replace('\\', '_'), "0"));
            }
            set
            {
                TextSettings.Set("V2_" + _path.Replace('.','_').Replace('\\', '_'), value.ToString());
            }
        }

        private string _path = "";
        public  string Decompress(string path, out bool onlyNewContent)
        {
            onlyNewContent = false;
            _path = path;
            if (_path.Contains("test"))
                return System.IO.File.ReadAllText(_path);

            //byte[] src = System.IO.File.ReadAllBytes(path);
            //change because in some cases stop thread with unkonw reason
            //so maybe it occure 

            string result = "";
            int lfp = lastFilePos;
            if (lfp > 0)
                onlyNewContent = true;
            Thread t  = new Thread(() =>
                {
                    byte[] src = readLogFile(ref lfp);
                    //Logger.WriteDebug("source array is {0}", src.ToString());
                    result = decompressLog(src);
                    //Logger.WriteDebug("result: \n {0}", result);
                });
            t.Start();
            t.Join();
            
            lastFilePos = lfp;
            GC.Collect();
            return result;
        }



        private byte[] readLogFile(ref int lfp)
        {
            try
            {

                //File.Copy(_path, mirrorFile, true);

                //List<byte> result;
                using (FileStream fsSource = new FileStream(_path,
                    FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {

                    // Read the source file into a byte array.
                    int numBytesRead = 0;
                    int numBytesToRead = (int)fsSource.Length - lfp ;
                    byte[] bytes = new byte[numBytesToRead];
                    while (numBytesToRead > 0)
                    {
                        // Read may return anything from 0 to numBytesToRead.
                        fsSource.Seek(lfp, SeekOrigin.Begin);
                        int n = fsSource.Read(bytes, numBytesRead, numBytesToRead);

                        numBytesRead += n;
                        numBytesToRead -= n;

                        // Break when the end of the file is reached.
                        if (n == 0)
                            break;

                        lfp = Convert.ToInt32( fsSource.Length);
                    }
                    //numBytesToRead = bytes.Length;
                    //result = bytes.ToList();
                    //bytes = null;
                    //Logger.WriteDebug("file {0} last position is {1}.", _path, lfp);
                    return bytes;
                }

                //GC.Collect(0, GCCollectionMode.Forced);
                //GC.WaitForPendingFinalizers();
            }
            catch (FileNotFoundException)
            {
                throw;
            }
        }


        private string decompressLog(byte[] inString)
        {
            bool doDecrypt = true;
            bool compressedRecord = true; // checkCompressed(inString);
            bool copiedFile = true;  // checkCopiedFile(inString);
            StringBuilder result = new StringBuilder();
             
            if(inString == null || inString.Length == 0)
                return "";
            int i = 0;
            while (i < inString.Length)
            {
                char testChar = (char)inString[i];
                if (compressedRecord || copiedFile)
                {
                    if (testChar >= 0x80 && testChar <= 0xBF)
                    {
                        result.Append(Replicate(' ', testChar - 0x80));
                    }
                    else if (testChar == 0xC0)
                    {
                        char temp1 = (char)inString[i + 1];
                        char temp2 = (char)inString[i + 2];
                        result.Append( Replicate(temp1, temp2));
                        i += 2;
                    }
                    else
                    {
                        if (testChar < 0x30)
                        {
                            if (testChar == 0x20 || testChar == 0x2 || testChar == 0xD)
                            {
                                // remove this characters from the list -> characters = '', 0x2, \r 
                            }
                            else if (testChar == 0x3)
                            {
                                result.Append('♂');
                            }
                            else
                                result.AppendLine();
                        }
                        else if (doDecrypt && testChar >= 0x30 && testChar <= 0x7f)
                        {
                            result.Append((char)(0x9f - testChar));
                        }
                        else
                        {
                            result.AppendFormat("\n[{0}->{1}]", (int)testChar, (int)(0x9f - testChar));
                        }
                    }
                }
                else
                {
                    if (testChar < 0x30)
                    {
                        if (testChar == 0x20 || testChar == 0x2 || testChar == 0xD)
                        {
                            // remove this characters from the list -> characters = '', 0x2, \r 
                        }
                        else if (testChar == 0x3)
                        {
                            result.Append('♂');
                        }
                        else
                            result.AppendLine();
                    }
                    else if (doDecrypt && testChar >= 0x30 && testChar <= 0x7f)
                    {
                        result.Append((char)(0x9f - testChar));
                    }
                    else
                    {
                        result.Append(string.Format("\n[{0}->{1}]", (int)testChar, (int)(0x9f - testChar)));
                    }
                }


                i++;
            }

            string temp = result.ToString();
            //result.Clear();
            //GC.Collect(2, GCCollectionMode.Forced);
            return temp;
        }

        //private  bool checkCompressed(string inString)
        //{
        //    if (inString[1] == 0x02 && inString[2] == 0x0D && inString[3] == 0x0A && inString[4] == 0x0D && inString[5] == 0x0A) 
        //        return false;
        //    //if (inString[1] == 0x03 && inString[2] == 0x02 && inString[3] == 0x0D && inString[4] == 0x0A && inString[5] == 0x0A) 
        //    //    return true;
        //    return true;
        //}

        //private  bool checkCopiedFile(string inString)
        //{
        //    if (inString[1] == 0x01 && inString[2] == 0x02 && inString[3] == 0x00)
        //        return true;
        //    return false;
        //}

        private  string Replicate(char Kar, int Len)
        {
            StringBuilder result = new StringBuilder(); ;
            for (int i = 0; i < Len; i++)
                result.Append(Kar);
            string temp = result.ToString();
            return temp;
        }

        
     }

}
