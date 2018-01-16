using System;
using System.IO;

namespace TwoDave.ServerSentEventsParser
{
    class InputTest
    {
        public static void ConsumeString ()
        {
            //string input = "data: This is data.\r\ndata: More data is expec";
            //Console.WriteLine (input);

            #region StreamReader does not care about escape characters
            ///*
            try
            {
                using (StreamReader sr = new StreamReader ("Test.txt"))
                {
                    string inputline;

                    // Read and display lines from the file until the end of 
                    // the file is reached.
                    while ((inputline = sr.ReadLine ()) != null)
                    {
                        Console.WriteLine (inputline);
                    }
                }
            }

            catch (FileNotFoundException e)
            {
                Console.WriteLine ("Could not fine {0}:", e.FileName);
            }
            //*/
            #endregion            

        }
    }
}