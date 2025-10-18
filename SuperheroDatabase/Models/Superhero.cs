using System;

namespace SuperheroDatabase.Models
{
    /// <summary>
    /// Represents a superhero trainee at One Kick Heroes Academy
    /// </summary>
    public class Superhero
    {
        /// <summary>
        /// Gets or sets the unique identifier for the hero
        /// </summary>
        public string HeroId { get; set; }

        /// <summary>
        /// Gets or sets the hero's name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the hero's age
        /// </summary>
        public int Age { get; set; }

        /// <summary>
        /// Gets or sets the hero's superpower ability
        /// </summary>
        public string Superpower { get; set; }

        /// <summary>
        /// Gets or sets the hero's exam score (0-100)
        /// </summary>
        public int ExamScore { get; set; }

        /// <summary>
        /// Gets or sets the hero's rank (S, A, B, or C)
        /// </summary>
        public string Rank { get; set; }

        /// <summary>
        /// Gets or sets the hero's threat level based on rank
        /// </summary>
        public string ThreatLevel { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public Superhero()
        {
        }

        /// <summary>
        /// Parameterized constructor for creating a superhero
        /// </summary>
        public Superhero(string heroId, string name, int age, string superpower, int examScore, string rank, string threatLevel)
        {
            HeroId = heroId;
            Name = name;
            Age = age;
            Superpower = superpower;
            ExamScore = examScore;
            Rank = rank;
            ThreatLevel = threatLevel;
        }

        /// <summary>
        /// Returns a CSV string representation of the superhero
        /// </summary>
        public string ToCsvString()
        {
            return $"{HeroId},{Name},{Age},{Superpower},{ExamScore},{Rank},{ThreatLevel}";
        }

        /// <summary>
        /// Creates a Superhero object from a CSV line
        /// </summary>
        public static Superhero FromCsvString(string csvLine)
        {
            if (string.IsNullOrWhiteSpace(csvLine))
                return null;

            string[] parts = csvLine.Split(',');
            if (parts.Length < 7)
                return null;

            return new Superhero
            {
                HeroId = parts[0].Trim(),
                Name = parts[1].Trim(),
                Age = int.TryParse(parts[2].Trim(), out int age) ? age : 0,
                Superpower = parts[3].Trim(),
                ExamScore = int.TryParse(parts[4].Trim(), out int score) ? score : 0,
                Rank = parts[5].Trim(),
                ThreatLevel = parts[6].Trim()
            };
        }
    }
}

