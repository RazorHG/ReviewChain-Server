using Nexient.Business;
using System;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace BlockChain.Reviews.Controllers
{
    [RoutePrefix("api/movie")]
    public class MovieController : ApiController
    {
        [Route("")]
        public IHttpActionResult GetReviews()
        {
            try
            {
                MovieReviewsInfoProvider blockChainInfoProvider = new MovieReviewsInfoProvider();

                HttpResponseMessage response = this.Request.CreateResponse(HttpStatusCode.OK);
                // response.Content = blockChainInfoProvider.GetBlockChain();
                return ResponseMessage(response);
            }
            catch (Exception)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

        }

        [Route("getmovielist/{query}")]
        public IHttpActionResult Get(string query)
        {
            try
            {
                string querytext = HttpUtility.UrlDecode(query);
                MovieReviewsInfoProvider movieInfoProvider = new MovieReviewsInfoProvider();
                HttpResponseMessage response = this.Request.CreateResponse(HttpStatusCode.OK);
                response.Content = movieInfoProvider.FetchMovieListAsync(querytext.Replace(@"'", "''"));
                return ResponseMessage(response);
            }
            catch (Exception)
            {
                HttpResponseMessage response = this.Request.CreateResponse(HttpStatusCode.NotFound);
                return ResponseMessage(response);
            }

        }

        [Route("getblocklist/{id:int}")]
        public IHttpActionResult GetMovie(int id)
        {
            try
            {

                MovieReviewsInfoProvider movieInfoProvider = new MovieReviewsInfoProvider();
                HttpResponseMessage response = this.Request.CreateResponse(HttpStatusCode.OK);
                response.Content = movieInfoProvider.GetBlockChain(id);
                return ResponseMessage(response);
            }
            catch (Exception)
            {
                HttpResponseMessage response = this.Request.CreateResponse(HttpStatusCode.NotFound);
                return ResponseMessage(response);

            }

        }

        [Route("getblocklist/{query}")]
        public IHttpActionResult GetMovie(string query)
        {
            try
            {
                string querytext = HttpUtility.UrlDecode(query);
                MovieReviewsInfoProvider movieInfoProvider = new MovieReviewsInfoProvider();
                HttpResponseMessage response = this.Request.CreateResponse(HttpStatusCode.OK);
                response.Content = movieInfoProvider.GetBlockChain(querytext.Replace(@"'", "''"));
                return ResponseMessage(response);
            }
            catch (Exception)
            {
                HttpResponseMessage response = this.Request.CreateResponse(HttpStatusCode.NotFound);
                return ResponseMessage(response);
            }

        }


        [Route("{id:int}")]
        public IHttpActionResult Get(int id)
        {
            try
            {
                MovieReviewsInfoProvider movieInfoProvider = new MovieReviewsInfoProvider();
                HttpResponseMessage response = this.Request.CreateResponse(HttpStatusCode.OK);
                response.Content = movieInfoProvider.FetchMovieInfo(id);
                return ResponseMessage(response);
            }
            catch (Exception)
            {
                HttpResponseMessage response = this.Request.CreateResponse(HttpStatusCode.NotFound);
                return ResponseMessage(response);
            }
        }


    }
}
