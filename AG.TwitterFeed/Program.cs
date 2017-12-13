using AG.TwitterFeed.Manager;
using AG.TwitterFeed.Models;
using System;

namespace AG.TwitterFeed
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                // Check for 2 files
                if (args.Length < 2)
                {
                    Console.WriteLine("**********ERROR**********");
                    Console.WriteLine("Please enter 2 arguments.");
                    Console.ReadKey();
                    return;
                }
                else if (args.Length > 2)
                {
                    Console.WriteLine("**********ERROR**********");
                    Console.WriteLine("Too many arguments entered.");
                    Console.ReadKey();
                    return;
                }

                TweetManager tweetManager = TweetManager.GetInstance;
                tweetManager.ImportFiles(args[0], args[1]);
            }
            catch (Exception ex)
            {
                Console.WriteLine("**********ERROR**********");
                Console.WriteLine(ex.Message);
            }

            // Pause the console
            Console.WriteLine("\nPress any key to continue.");
            Console.ReadKey();
        }
    }
}
