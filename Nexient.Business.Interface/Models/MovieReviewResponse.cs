using System;
using System.Collections.Generic;

namespace Nexient.Business.Interface.Models
{
    public class MovieReviewResponse
    {
        public List<MovieBlockChainResponse> Reviews { get; set; }
        public int Id { get; set; }

        public int Votecount { get; set; }

        public double VoteAverage { get; set; }

        public string OriginalTitle { get; set; }

        public double Popularity { get; set; }

        public string PosterPath { get; set; }

        public string BackdropPath { get; set; }

        public string OverView { get; set; }

        public DateTime? ReleaseDate { get; set; }

    }
}
