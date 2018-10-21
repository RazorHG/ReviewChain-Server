namespace Nexient.Business.Interface.Models
{
    public class MovieBlockChainResponse
    {
        public long Index { get; set; }

        public string PreviousHash { get; set; }

        public long TimeStamp { get; set; }

        public string Hash { get; set; }

        public long nonce { get; set; }

        public string userName { get; set; }

        public long rating { get; set; }

        public string textReview { get; set; }

    }
}