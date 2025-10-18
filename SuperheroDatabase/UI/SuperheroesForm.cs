using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using SuperheroDatabase.Models;
using SuperheroDatabase.Services;

namespace SuperheroDatabase.UI
{
    /// <summary>
    /// Main form for the Superhero Database application
    /// Person 1: Add functionality
    /// Person 2: View and Update functionality  
    /// Person 3: Delete and Summary Report functionality
    /// </summary>
    public partial class SuperheroesForm : Form
    {
        private readonly string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "superheroes.txt");
        private readonly SuperheroRepository _repository;

        public SuperheroesForm()
        {
            InitializeComponent();
            _repository = new SuperheroRepository(filePath);
        }

        /// <summary>
        /// Person 1: Add Superhero functionality
        /// </summary>
        private void btnAddHero_Click(object sender, EventArgs e)
        {
            try
            {
                // Validate all fields
                if (string.IsNullOrWhiteSpace(txtHeroID.Text) ||
                    string.IsNullOrWhiteSpace(txtName.Text) ||
                    string.IsNullOrWhiteSpace(txtAge.Text) ||
                    string.IsNullOrWhiteSpace(txtSuperpower.Text) ||
                    string.IsNullOrWhiteSpace(txtExamScore.Text))
                {
                    MessageBox.Show("All fields are required!", "Validation Error");
                    return;
                }

                // Validate numeric input
                if (!int.TryParse(txtAge.Text, out int age) || age <= 0)
                {
                    MessageBox.Show("Age must be a valid positive number!", "Validation Error");
                    return;
                }

                if (!int.TryParse(txtExamScore.Text, out int score) || score < 0 || score > 100)
                {
                    MessageBox.Show("Exam Score must be between 0 and 100!", "Validation Error");
                    return;
                }

                // Check if Hero ID already exists
                if (_repository.ExistsById(txtHeroID.Text.Trim()))
                {
                    MessageBox.Show("A hero with this ID already exists!", "Validation Error");
                    return;
                }

                // Calculate rank and threat level using RankCalculator
                var (rank, threatLevel) = RankCalculator.FromScore(score);

                // Create new superhero object
                var newHero = new Superhero
                {
                    HeroId = txtHeroID.Text.Trim(),
                    Name = txtName.Text.Trim(),
                    Age = age,
                    Superpower = txtSuperpower.Text.Trim(),
                    ExamScore = score,
                    Rank = rank,
                    ThreatLevel = threatLevel
                };

                // Add to repository
                _repository.Add(newHero);

                MessageBox.Show("Superhero added successfully!", "Success");
                ClearFields();
                
                // Refresh the grid if data is currently displayed
                if (dgvHeroes.Rows.Count > 0)
                {
                    btnViewAll_Click(sender, e);
                }
            }
            catch (IOException ioEx)
            {
                MessageBox.Show($"File error while adding hero: {ioEx.Message}", "File Error");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error");
            }
        }

        /// <summary>
        /// Person 1 & 2: View All Superheroes functionality
        /// </summary>
        private void btnViewAll_Click(object sender, EventArgs e)
        {
            try
            {
                dgvHeroes.Rows.Clear();

                var heroes = _repository.GetAll();
                
                if (heroes.Count == 0)
                {
                    MessageBox.Show("No heroes found! Please add heroes first.", "Empty Database");
                    return;
                }

                foreach (var hero in heroes)
                {
                    dgvHeroes.Rows.Add(
                        hero.HeroId,
                        hero.Name,
                        hero.Age,
                        hero.Superpower,
                        hero.ExamScore,
                        hero.Rank,
                        hero.ThreatLevel
                    );
                }
            }
            catch (IOException ioEx)
            {
                MessageBox.Show($"File error while loading heroes: {ioEx.Message}", "File Error");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error reading file: {ex.Message}", "Error");
            }
        }

        /// <summary>
        /// Clear all input fields
        /// </summary>
        private void ClearFields()
        {
            txtHeroID.Clear();
            txtName.Clear();
            txtAge.Clear();
            txtSuperpower.Clear();
            txtExamScore.Clear();
        }

        /// <summary>
        /// Person 3: Helper method to get the selected Hero ID from either the textbox or the DataGridView
        /// </summary>
        /// <returns>Hero ID string if available, null otherwise</returns>
        private string GetSelectedHeroID()
        {
            // First, check if txtHeroID has a value
            if (!string.IsNullOrWhiteSpace(txtHeroID.Text))
            {
                return txtHeroID.Text.Trim();
            }

            // Otherwise, check if a row is selected in the DataGridView
            if (dgvHeroes.CurrentRow != null && dgvHeroes.CurrentRow.Cells[0].Value != null)
            {
                return dgvHeroes.CurrentRow.Cells[0].Value.ToString();
            }

            return null;
        }

        /// <summary>
        /// Person 3: Delete Superhero - Remove a superhero record from the file with confirmation
        /// </summary>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                // Get the Hero ID from textbox or selected row
                string heroID = GetSelectedHeroID();

                if (string.IsNullOrWhiteSpace(heroID))
                {
                    MessageBox.Show("Please select a superhero to delete or enter a Hero ID.", "No Selection");
                    return;
                }

                // Get the hero from repository
                var hero = _repository.GetById(heroID);

                if (hero == null)
                {
                    MessageBox.Show($"Hero with ID '{heroID}' not found in the database.", "Not Found");
                    return;
                }

                // Show confirmation dialog
                DialogResult result = MessageBox.Show(
                    $"Are you sure you want to delete {hero.Name} (ID: {heroID})?",
                    "Confirm Delete",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    // Delete from repository
                    _repository.Delete(heroID);

                    // Refresh the DataGridView
                    btnViewAll_Click(sender, e);

                    // Clear input fields
                    ClearFields();

                    MessageBox.Show($"Superhero '{hero.Name}' deleted successfully!", "Success");
                }
            }
            catch (IOException ioEx)
            {
                MessageBox.Show($"File error while deleting: {ioEx.Message}", "File Error");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error");
            }
        }

        /// <summary>
        /// Person 3: Generate Summary Report - Calculate statistics and save to file
        /// </summary>
        private void btnGenerateReport_Click(object sender, EventArgs e)
        {
            try
            {
                // Get all heroes from repository
                var heroes = _repository.GetAll();

                if (heroes.Count == 0)
                {
                    MessageBox.Show("No heroes in the database to generate a report.", "Empty Database");
                    return;
                }

                // Initialize variables for calculations
                int totalHeroes = heroes.Count;
                double totalAge = 0;
                double totalScore = 0;
                int countS = 0, countA = 0, countB = 0, countC = 0;

                // Calculate statistics from hero objects
                foreach (var hero in heroes)
                {
                    totalAge += hero.Age;
                    totalScore += hero.ExamScore;

                    // Count by Rank
                    switch (hero.Rank)
                    {
                        case "S":
                            countS++;
                            break;
                        case "A":
                            countA++;
                            break;
                        case "B":
                            countB++;
                            break;
                        case "C":
                            countC++;
                            break;
                    }
                }

                // Calculate averages
                double avgAge = totalHeroes > 0 ? totalAge / totalHeroes : 0;
                double avgScore = totalHeroes > 0 ? totalScore / totalHeroes : 0;

                // Format the report
                string report = "========== SUPERHERO DATABASE SUMMARY ==========\n\n";
                report += $"Total Number of Heroes: {totalHeroes}\n";
                report += $"Average Age: {avgAge:F2} years\n";
                report += $"Average Exam Score: {avgScore:F2}\n\n";
                report += "Heroes by Rank:\n";
                report += $"  S-Rank: {countS} hero(es)\n";
                report += $"  A-Rank: {countA} hero(es)\n";
                report += $"  B-Rank: {countB} hero(es)\n";
                report += $"  C-Rank: {countC} hero(es)\n\n";
                report += "================================================";

                // Display the report in a MessageBox
                MessageBox.Show(report, "Summary Report", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Save the report to summary.txt in Data folder
                string summaryFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "summary.txt");
                File.WriteAllText(summaryFilePath, report);

                MessageBox.Show($"Report saved to: {summaryFilePath}", "Report Saved");
            }
            catch (IOException ioEx)
            {
                MessageBox.Show($"File error while generating report: {ioEx.Message}", "File Error");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error");
            }
        }
    }
}

