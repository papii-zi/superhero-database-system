namespace SuperheroDatabase.Models
{
    /// <summary>
    /// Utility class for calculating superhero rank and threat level based on exam scores
    /// </summary>
    public static class RankCalculator
    {
        /// <summary>
        /// Calculates the rank and threat level from an exam score
        /// </summary>
        /// <param name="examScore">The exam score (0-100)</param>
        /// <returns>A tuple containing (Rank, ThreatLevel)</returns>
        public static (string Rank, string ThreatLevel) FromScore(int examScore)
        {
            string rank = GetRank(examScore);
            string threatLevel = GetThreatLevel(rank);
            return (rank, threatLevel);
        }

        /// <summary>
        /// Determines the rank based on exam score
        /// </summary>
        /// <param name="score">The exam score (0-100)</param>
        /// <returns>Rank: S, A, B, or C</returns>
        private static string GetRank(int score)
        {
            if (score >= 81) return "S";
            if (score >= 61) return "A";
            if (score >= 41) return "B";
            return "C";
        }

        /// <summary>
        /// Determines the threat level based on rank
        /// </summary>
        /// <param name="rank">The superhero's rank</param>
        /// <returns>Threat level description</returns>
        private static string GetThreatLevel(string rank)
        {
            switch (rank)
            {
                case "S": return "Finals Week";
                case "A": return "Midterm Madness";
                case "B": return "Group Project Gone Wrong";
                default: return "Pop Quiz";
            }
        }
    }
}

