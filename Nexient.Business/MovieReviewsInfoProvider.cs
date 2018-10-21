using Nexient.Business.Interface.Services;
using Nexient.MovieDB;
using System;
using System.Net.Http;

namespace Nexient.Business
{
    public class MovieReviewsInfoProvider : IMovieReviewService, IDisposable
    {
        readonly IMovieReviewService _movieReviewService;

        public MovieReviewsInfoProvider()
        {
            _movieReviewService = new MovieInfoProvider();
        }

        public void Dispose()
        {
        }

        public StringContent FetchMovieInfo(int movieID)
        {
            return _movieReviewService.FetchMovieInfo(movieID);
        }

        public StringContent FetchMovieListAsync(string query)
        {
            return _movieReviewService.FetchMovieListAsync(query);
        }

        public StringContent GetBlockChain(int movieID)
        {
            return _movieReviewService.GetBlockChain(movieID);
        }

        public StringContent GetBlockChain(string query)
        {
            return _movieReviewService.GetBlockChain(query);
        }
    }
}
