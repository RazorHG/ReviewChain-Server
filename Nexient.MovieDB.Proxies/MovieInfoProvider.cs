using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Nexient.Business.Interface.Models;
using Nexient.Business.Interface.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TMDbLib.Client;
using TMDbLib.Objects.General;
using TMDbLib.Objects.Movies;
using TMDbLib.Objects.Search;

namespace Nexient.MovieDB
{
    public class MovieInfoProvider : IMovieReviewService, IDisposable
    {
        private readonly TMDbClient _client;

        public MovieInfoProvider()
        {
            _client = MovieDBConnection.database;
        }

        public void Dispose()
        { }

        public StringContent FetchMovieListAsync(string query)
        {
            SearchContainer<SearchMovie> results = _client.SearchMovieAsync(query).Result;

            StringBuilder str = new StringBuilder();
            List<MovieDBResponse> items = new List<MovieDBResponse>();
            foreach (SearchMovie searchMovie in results.Results)
            {
                MovieDBResponse movieDBResponse = new MovieDBResponse();
                movieDBResponse.BackdropPath = searchMovie.BackdropPath;
                movieDBResponse.Id = searchMovie.Id;
                movieDBResponse.OriginalTitle = searchMovie.OriginalTitle;
                movieDBResponse.OverView = searchMovie.Overview;
                movieDBResponse.PosterPath = searchMovie.PosterPath;
                movieDBResponse.ReleaseDate = searchMovie.ReleaseDate;
                movieDBResponse.VoteAverage = searchMovie.VoteAverage;
                movieDBResponse.Votecount = searchMovie.VoteCount;
                movieDBResponse.Popularity = searchMovie.Popularity;
                //str.Append(JsonConvert.SerializeObject(movieDBResponse));
                items.Add(movieDBResponse);
            }

            return GetStaticcontent(JsonConvert.SerializeObject(items));
        }

        public StringContent FetchMovieInfo(int movieID)
        {
            MovieDBResponse movieDBResponse = GetMovieInfo(movieID);
            return GetStaticcontent(JsonConvert.SerializeObject(movieDBResponse).ToString());
        }

        private MovieDBResponse GetMovieInfo(int movieID)
        {
            Task<Movie> results = _client.GetMovieAsync(movieID);

            MovieDBResponse movieDBResponse = new MovieDBResponse();
            movieDBResponse.BackdropPath = results.Result.BackdropPath;
            movieDBResponse.Id = results.Result.Id;
            movieDBResponse.OriginalTitle = results.Result.OriginalTitle;
            movieDBResponse.OverView = results.Result.Overview;
            movieDBResponse.PosterPath = results.Result.PosterPath;
            movieDBResponse.ReleaseDate = results.Result.ReleaseDate;
            movieDBResponse.VoteAverage = results.Result.VoteAverage;
            movieDBResponse.Votecount = results.Result.VoteCount;
            movieDBResponse.Popularity = results.Result.Popularity;
            return movieDBResponse;
        }

        public StringContent GetBlockChain(int movieID)
        {
            WebClient web = new WebClient();
            System.IO.Stream stream = GetBlockJson(web);
            List<MovieBlockChainResponse> items = new List<MovieBlockChainResponse>();

            using (System.IO.StreamReader reader = new System.IO.StreamReader(stream))
            {
                string blockChain = reader.ReadToEnd();
                IList<JToken> results = JObject.Parse(blockChain)["blockchain"].Values("data").ToList();

                int index = 0;
                MovieReviewResponse movieReviewResponse = new MovieReviewResponse();
                foreach (JToken result in results)
                {

                    if (index == 0)
                    {
                        index++;
                        continue;
                    }
                    dynamic userinfo = JsonConvert.DeserializeObject<object>(result.ToString());
                    int currentmovieid = 0;
                    int.TryParse(userinfo.movieId.Value.ToString(), out currentmovieid);

                    if (currentmovieid > 0 && movieID == currentmovieid)
                    {
                        MovieDBResponse moveinfo = GetMovieInfo(movieID);


                        if (moveinfo != null && movieReviewResponse.OverView == null)
                        {
                            GetMoveInfo(movieReviewResponse, moveinfo);
                        }
                        MovieBlockChainResponse movieBlockChainResponse = new MovieBlockChainResponse();
                        {
                            GetUserInformation(blockChain, index, userinfo, movieBlockChainResponse);

                        };

                        items.Add(movieBlockChainResponse);

                    }
                    index++;
                }
                movieReviewResponse.Votecount = items.Count;
                movieReviewResponse.VoteAverage = Math.Round((double)items.Count / 5);
                movieReviewResponse.Reviews = items;
                return GetStaticcontent(JsonConvert.SerializeObject(movieReviewResponse).ToString());
            }




        }

        private static System.IO.Stream GetBlockJson(WebClient web)
        {
            string blockChainPath = ConfigurationManager.AppSettings["BlockChainURL"].ToString();
            System.IO.Stream stream = web.OpenRead(blockChainPath);
            return stream;
        }

        public StringContent GetBlockChain(string query)
        {
            String blockChain = string.Empty;
            WebClient web = new WebClient();
            string blockChainPath = ConfigurationManager.AppSettings["BlockChainURL"].ToString();
            System.IO.Stream stream = web.OpenRead(blockChainPath);
            List<MovieBlockChainResponse> items = new List<MovieBlockChainResponse>();

            using (System.IO.StreamReader reader = new System.IO.StreamReader(stream))
            {
                blockChain = reader.ReadToEnd();
                IList<JToken> results = JObject.Parse(blockChain)["blockchain"].Values("data").ToList();

                int index = 0;
                MovieReviewResponse movieReviewResponse = new MovieReviewResponse();
                foreach (JToken result in results)
                {

                    if (index == 0)
                    {
                        index++;
                        continue;
                    }
                    dynamic userinfo = JsonConvert.DeserializeObject<object>(result.ToString());

                    if (userinfo.userName.Value.ToString() == query)
                    {
                        int currentmovieid = 0;
                        int.TryParse(userinfo.movieId.Value.ToString(), out currentmovieid);
                        MovieDBResponse moveinfo = GetMovieInfo(currentmovieid);

                        if (moveinfo != null && movieReviewResponse.OverView == null)
                        {
                            GetMoveInfo(movieReviewResponse, moveinfo);
                        }
                        MovieBlockChainResponse movieBlockChainResponse = new MovieBlockChainResponse();
                        {
                            GetUserInformation(blockChain, index, userinfo, movieBlockChainResponse);

                        };

                        items.Add(movieBlockChainResponse);

                    }
                    index++;
                }
                movieReviewResponse.Votecount = items.Count;
                movieReviewResponse.VoteAverage = Math.Round((double)items.Count / 5);
                movieReviewResponse.Reviews = items;
                return GetStaticcontent(JsonConvert.SerializeObject(movieReviewResponse).ToString());
            }




        }

        private static void GetUserInformation(string blockChain, int index, dynamic userinfo, MovieBlockChainResponse movieBlockChainResponse)
        {
            movieBlockChainResponse.userName = userinfo.userName.Value;
            movieBlockChainResponse.rating = userinfo.rating.Value;
            movieBlockChainResponse.textReview = userinfo.textReview.Value;
            JToken indexobject = JObject.Parse(blockChain)["blockchain"][index];
            movieBlockChainResponse.Index = (long)Convert.ToDouble(indexobject["index"].ToString());
            movieBlockChainResponse.PreviousHash = indexobject["previousHash"].ToString();
            movieBlockChainResponse.TimeStamp = (long)Convert.ToDouble(indexobject["timestamp"].ToString());
            movieBlockChainResponse.Hash = indexobject["hash"].ToString();
            movieBlockChainResponse.nonce = (long)Convert.ToDouble(indexobject["nonce"].ToString());
        }

        private static void GetMoveInfo(MovieReviewResponse movieReviewResponse, MovieDBResponse moveinfo)
        {
            movieReviewResponse.BackdropPath = moveinfo.BackdropPath;
            movieReviewResponse.OriginalTitle = moveinfo.OriginalTitle;
            movieReviewResponse.OverView = moveinfo.OverView;
            movieReviewResponse.PosterPath = moveinfo.PosterPath;
            movieReviewResponse.ReleaseDate = moveinfo.ReleaseDate;
            movieReviewResponse.Popularity = moveinfo.Popularity;
            movieReviewResponse.Id = moveinfo.Id;
        }

        private StringContent GetStaticcontent(string json)
        {
            if (string.IsNullOrEmpty(json))
            {
                return new StringContent(string.Empty, Encoding.UTF8, "application/json");
            }
            return new StringContent(json, Encoding.UTF8, "application/json");

        }
    }
}
