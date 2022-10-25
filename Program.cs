

using System;
using System.IO;
using System.Text;
using System.Net;
using System.Threading.Tasks;
using Discord;
using System.Web;

namespace HttpListenerExample
{



    class HttpServer
    {
        public static HttpListener listener;
        public static string url = "http://localhost:6361/";
        public static long currentElapsed = 0;
        public static string currentSong = "";
        public static string CLIENT_ID_ENV = Environment.GetEnvironmentVariable("YT_CLIENT_ID");
        public static long CLIENT_ID = 0;
        public static Discord.Discord discord;


        public static Discord.ActivityManager activityManager;

        public static async Task HandleIncomingConnections()
        {
            bool runServer = true;

            while (runServer)
            {



                HttpListenerContext ctx = await listener.GetContextAsync();

                HttpListenerRequest req = ctx.Request;
                HttpListenerResponse resp = ctx.Response;

                Console.WriteLine(req.Url.ToString());
                Console.WriteLine(HttpUtility.UrlDecode(req.QueryString["name"]));
                Console.WriteLine(HttpUtility.UrlDecode(req.QueryString["artist"]));
                Console.WriteLine(HttpUtility.UrlDecode(req.QueryString["imgsrc"]));
                Console.WriteLine();

                Update(req.QueryString["name"], req.QueryString["artist"], req.QueryString["imgsrc"]);
                resp.Close();
            }
        }


        public static void Main(string[] args)
        {

            listener = new HttpListener();
            listener.Prefixes.Add(url);
            listener.Start();
            Console.WriteLine("Listening for connections on {0}", url);
            Console.WriteLine("Starting Youtube Music Activity");

            if (CLIENT_ID_ENV == null) {
                CLIENT_ID = 0;
            } else {
                CLIENT_ID = long.Parse(CLIENT_ID_ENV);
            }
            discord = new Discord.Discord(CLIENT_ID, (UInt64)Discord.CreateFlags.Default);
            activityManager = discord.GetActivityManager();
            Task listenTask = HandleIncomingConnections();
            Update("", "", "https://cdn.discordapp.com/app-icons/1034297846100394056/a8a318b62d368addd303cd2d140da09b.png");
            listenTask.GetAwaiter().GetResult();

            listener.Close();
        }

        public static void Update(string songname, string artistname, string imagesrc)
        {

            // Get the offset from current time in UTC time
            DateTimeOffset dto = new DateTimeOffset(DateTime.UtcNow);
            // Get the unix timestamp in seconds
            long unixTime = dto.ToUnixTimeSeconds();

            if (currentSong == "")
            {
                currentSong = songname;
                currentElapsed = unixTime;
            }
            else if (songname != currentSong)
            {
                currentSong = songname;
                currentElapsed = unixTime;
            }

            var activity = new Discord.Activity
            {
                State = artistname,
                Details = songname,
                Type = Discord.ActivityType.Listening,
                Timestamps = 
                {
                    Start = currentElapsed
                },
                Assets =
                {
                    LargeImage = imagesrc,
                    LargeText = "Youtube Music",
                    SmallImage = "https://cdn.discordapp.com/app-icons/1034297846100394056/a8a318b62d368addd303cd2d140da09b.png?size=64",
                    SmallText = "Youtube Music",
                },
                Instance = false,
            };

            
            activityManager.UpdateActivity(activity, (result) =>
            {
                if (result != Discord.Result.Ok)
                     Console.WriteLine("Failed");
                
            });

            discord.RunCallbacks();
        }


    }
}