using System.Net.Http;

namespace Nexient.Business.Interface.Services
{
    public interface IMovieReviewService
    {
        StringContent FetchMovieListAsync(string query);

        StringContent FetchMovieInfo(int movieID);

        StringContent GetBlockChain(int movieID);

        StringContent GetBlockChain(string query);
    }
}