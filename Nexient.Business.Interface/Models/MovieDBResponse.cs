using System;

namespace Nexient.Business.Interface.Models
{
    public class MovieDBResponse
    {
        public int Id { get; set; }

        public int Votecount { get; set; }

        public double VoteAverage { get; set; }

        public string OriginalTitle { get; set; }

        public double Popularity { get; set; }

        public string PosterPath { get; set; }

        public string BackdropPath { get; set; }

        public string OverView { get; set; }

        public DateTime? ReleaseDate { get; set; }

        public double BlockChainAverage { get; set; }

        public double BlockChainReviewCount { get; set; }


    }
}