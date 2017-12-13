# TwitterFeed

**Version 1.0.0**

Twitter like feed from input 2 input files.

**Assumptions made during this project:**
1. Assuming that all users are deleted with file passed in.
2. Assuming that the same Name in the different rows is the same User eg. Alan is Alan, which is the same user no matter which row its on.
3. Assuming messages greater 140 characters get truncated.

**Running the project:**
1. Requires a pc with .Net Core 2.0
2. Build the project in Debug or Release mode
3. From command line you can browse to the AG.TwitterFeed\bin\debug\netcoreapp2.0 or AG.TwitterFeed\bin\release\netcoreapp2.0 folder.
4. Run the app with the following command: dotnet.exe AG.TwitterFeed.dll user.txt tweet.txt

**Results should look like this***
<img src="https://github.com/rizaanlakay/TwitterFeed/blob/master/screenshot.jpg">
