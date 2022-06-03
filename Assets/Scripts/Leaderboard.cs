#if !UNITY_WEBGL
using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json;
#endif
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public class Leaderboard : MonoBehaviour
{
    private const string url = "mongodb+srv://thumbwar-api:n3lJFdVeasdIurKx@cluster0.e6uob.mongodb.net/Leaderboard?retryWrites=true";
    #if !UNITY_WEBGL
    MongoClient client = new MongoClient(url);
    IMongoDatabase database;
    IMongoCollection<BsonDocument> collection;
    #endif
    public List<ThumberWar.Leaderboard.Models.Leaderboard> leaderboards = new List<ThumberWar.Leaderboard.Models.Leaderboard>();

    private void Start()
    {
        #if UNITY_WEBGL
        #else
                database = client.GetDatabase("Leaderboard");
                collection = database.GetCollection<BsonDocument>("Leaderboard");
                StartCoroutine(GetLeaderboard());
        #endif

    }

    public void UpdateLeaderboard()
    {
        #if UNITY_WEBGL
        #else
        StartCoroutine(GetLeaderboard());
        #endif
    }

    public async void Insert(ThumberWar.Leaderboard.Models.Leaderboard leaderboard)
    {
        #if UNITY_WEBGL
        #else
        var document = new BsonDocument(true);
        int difficulty = 0;

        switch(leaderboard.difficulty)
        {
            case ThumberWar.Leaderboard.Models.Enums.DIFFICULTY_LEVEL.EASY:
                difficulty = 0;
                break;
            case ThumberWar.Leaderboard.Models.Enums.DIFFICULTY_LEVEL.HARD:
                difficulty = 2;
                break;
            case ThumberWar.Leaderboard.Models.Enums.DIFFICULTY_LEVEL.NORMAL:
                difficulty = 1;
                break;
        }

        string totalTimeString =
                        string.Format("{0:00}:{1:00}:{2:00}",
                                      (int)(leaderboard.time.TotalHours),
                                      leaderboard.time.Minutes,
                                      leaderboard.time.Seconds);


        document.Add(new BsonElement("difficulty", difficulty));
        document.Add(new BsonElement("username", leaderboard.playerName));
        document.Add(new BsonElement("time", totalTimeString));

        if(collection != null)
        {
            collection.InsertOne(document);
        }
        #endif
    }

    public IEnumerator GetLeaderboard()
    {
        #if UNITY_WEBGL
            yield return new WaitForSeconds(0.1f);
        #else
        leaderboards.Clear();
        var leaderboard = collection.FindAsync(new BsonDocument());

        while(!leaderboard.IsCompleted)
        {
            yield return new WaitForSeconds(1.0f);
        }

        var result = leaderboard.Result;
        
        foreach(var r in result.ToList())
        {
            try
            {
                string name = r["username"].ToString();
                int difficulty = int.Parse(r["difficulty"].ToString());
                string timestring = r["time"].ToString();

                DateTime dt;
                if (!DateTime.TryParseExact(timestring, "HH:mm:ss", CultureInfo.InvariantCulture,
                                                              DateTimeStyles.None, out dt))
                {
                    // handle validation error
                }
                TimeSpan time = dt.TimeOfDay;
                if(difficulty == 0)
                {
                    leaderboards.Add(new ThumberWar.Leaderboard.Models.Leaderboard
                    {
                        difficulty = ThumberWar.Leaderboard.Models.Enums.DIFFICULTY_LEVEL.EASY,
                        playerName = name,
                        time = time
                    });
                }
                else if (difficulty == 1)
                {
                    leaderboards.Add(new ThumberWar.Leaderboard.Models.Leaderboard
                    {
                        difficulty = ThumberWar.Leaderboard.Models.Enums.DIFFICULTY_LEVEL.NORMAL,
                        playerName = name,
                        time = time
                    });
                }
                else if (difficulty == 2)
                {
                    leaderboards.Add(new ThumberWar.Leaderboard.Models.Leaderboard
                    {
                        difficulty = ThumberWar.Leaderboard.Models.Enums.DIFFICULTY_LEVEL.HARD,
                        playerName = name,
                        time = time
                    });
                }
            }
            catch(Exception e)
            {

            }
        }

        /*var board = leaderboardAwaited.Result.ToList().Select(x => JsonConvert.DeserializeObject(x.ToJson())).ToList();

        foreach(object result in board)
        {
            Debug.Log(result);
        }*/
        #endif
    }
}
