using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using SuperheroDatabase.Models;

namespace SuperheroDatabase.Services
{
    /// <summary>
    /// Repository class for managing superhero data persistence
    /// </summary>
    public class SuperheroRepository
    {
        private readonly string _filePath;

        /// <summary>
        /// Constructor that initializes the repository with a file path
        /// </summary>
        /// <param name="filePath">Path to the superheroes data file</param>
        public SuperheroRepository(string filePath)
        {
            _filePath = filePath;
            EnsureDataFileExists();
        }

        /// <summary>
        /// Ensures the data file exists, creates it if it doesn't
        /// </summary>
        private void EnsureDataFileExists()
        {
            if (!File.Exists(_filePath))
            {
                // Create the directory if it doesn't exist
                string directory = Path.GetDirectoryName(_filePath);
                if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                // Create an empty file
                File.Create(_filePath).Dispose();
            }
        }

        /// <summary>
        /// Retrieves all superheroes from the data file
        /// </summary>
        /// <returns>List of all superheroes</returns>
        public List<Superhero> GetAll()
        {
            var heroes = new List<Superhero>();

            if (!File.Exists(_filePath))
                return heroes;

            string[] lines = File.ReadAllLines(_filePath);
            
            foreach (string line in lines)
            {
                if (string.IsNullOrWhiteSpace(line))
                    continue;

                var hero = Superhero.FromCsvString(line);
                if (hero != null)
                {
                    heroes.Add(hero);
                }
            }

            return heroes;
        }

        /// <summary>
        /// Saves all superheroes to the data file
        /// </summary>
        /// <param name="heroes">Collection of superheroes to save</param>
        public void SaveAll(IEnumerable<Superhero> heroes)
        {
            var lines = heroes.Select(h => h.ToCsvString()).ToArray();
            File.WriteAllLines(_filePath, lines);
        }

        /// <summary>
        /// Adds a new superhero to the data file
        /// </summary>
        /// <param name="hero">The superhero to add</param>
        public void Add(Superhero hero)
        {
            string line = hero.ToCsvString();
            File.AppendAllText(_filePath, line + Environment.NewLine);
        }

        /// <summary>
        /// Updates an existing superhero in the data file
        /// </summary>
        /// <param name="heroId">The ID of the hero to update</param>
        /// <param name="updatedHero">The updated hero data</param>
        /// <returns>True if the hero was found and updated, false otherwise</returns>
        public bool Update(string heroId, Superhero updatedHero)
        {
            var heroes = GetAll();
            var hero = heroes.FirstOrDefault(h => h.HeroId == heroId);

            if (hero == null)
                return false;

            // Update the hero's properties
            hero.Name = updatedHero.Name;
            hero.Age = updatedHero.Age;
            hero.Superpower = updatedHero.Superpower;
            hero.ExamScore = updatedHero.ExamScore;
            hero.Rank = updatedHero.Rank;
            hero.ThreatLevel = updatedHero.ThreatLevel;

            SaveAll(heroes);
            return true;
        }

        /// <summary>
        /// Deletes a superhero from the data file
        /// </summary>
        /// <param name="heroId">The ID of the hero to delete</param>
        /// <returns>True if the hero was found and deleted, false otherwise</returns>
        public bool Delete(string heroId)
        {
            var heroes = GetAll();
            var initialCount = heroes.Count;

            heroes = heroes.Where(h => h.HeroId != heroId).ToList();

            if (heroes.Count == initialCount)
                return false;

            SaveAll(heroes);
            return true;
        }

        /// <summary>
        /// Finds a superhero by ID
        /// </summary>
        /// <param name="heroId">The hero ID to search for</param>
        /// <returns>The superhero if found, null otherwise</returns>
        public Superhero GetById(string heroId)
        {
            return GetAll().FirstOrDefault(h => h.HeroId == heroId);
        }

        /// <summary>
        /// Checks if a hero ID already exists
        /// </summary>
        /// <param name="heroId">The hero ID to check</param>
        /// <returns>True if the ID exists, false otherwise</returns>
        public bool ExistsById(string heroId)
        {
            return GetAll().Any(h => h.HeroId == heroId);
        }
    }
}

