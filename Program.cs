using System;
using System.IO;

namespace ServerSentEventsParser
{
    class Program
    {
        static void Main (string[] args)
        {
            try
            {
                using (StreamReader sr = new StreamReader ("Test.txt"))
                {
                    string line;

                    // Read and display lines from the file until the end of 
                    // the file is reached.
                    while ((line = sr.ReadLine ()) != null)
                    {
                        Console.WriteLine (line);
                    }
                }
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine ("Could not fine {0}:", e.FileName);
            }

        }
    }
}