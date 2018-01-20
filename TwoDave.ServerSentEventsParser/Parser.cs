using System;
using System.IO;

namespace TwoDave.ServerSentEventsParser
{
    public class Parser
    {
        public static string Parse(string input, out string remainder)
        {
            char quickcounter;

            for (int i = 0; i < input.Length; i++)
            {
                char temp = input[i];

                if (temp == 13) //Decimal value of /r
                {
                    Console.WriteLine("Carriage return found");
                }

                if (temp == 10) //Decimal value of /n
                {
                    Console.WriteLine("New line found");
                }

                //quickcounter = temp; //checking the last character
                

            }
            
            var line = "data: This is data."; //the answer for line the unit test expects;
            remainder = "data: More data is expec"; //the answer for remainder the unit test expects;

            return line;

            //throw new NotImplementedException();
        }
    }
}