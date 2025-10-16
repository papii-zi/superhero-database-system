
// MainForm.Person2.cs
// Person 2: View + Update features (PRG2782 Superhero Database)
// Drop this next to your MainForm.cs. Ensure the designer names match the controls used below.
// Requires: Superhero, RankCalculator, SuperheroRepository already present.

using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace SuperheroApp.UI
{
    public partial class MainForm : Form
    {
        // If Person 1 already declared 'repo', remove the line below and use theirs.
        private readonly Services.SuperheroRepository repo =
            new Services.SuperheroRepository(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "superheroes.txt"));

        private void MainForm_Load(object sender, EventArgs e)
        {
            // Optional: if Person 1 already reloads, you can remove this.
            SafeReloadGrid();
            WireGridSelection();
        }

        private void btnViewAll_Click(object sender, EventArgs e)
        {
            SafeReloadGrid();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                var heroes = repo.GetAll();
                var id = txtHeroID.Text?.Trim();
                if (string.IsNullOrWhiteSpace(id))
                {
                    MessageBox.Show("Select a hero to update.");
                    return;
                }

                var hero = heroes.FirstOrDefault(h => h.HeroId == id);
                if (hero == null)
                {
                    MessageBox.Show("Hero not found in file.");
                    return;
                }

                if (!int.TryParse(txtAge.Text, out int age))
                {
                    MessageBox.Show("Age must be a valid number.");
                    return;
                }

                if (!int.TryParse(txtScore.Text, out int score) || score < 0 || score > 100)
                {
                    MessageBox.Show("Exam Score must be a number between 0 and 100.");
                    return;
                }

                hero.Name = txtName.Text?.Trim() ?? "";
                hero.Age = age;
                hero.Superpower = txtPower.Text?.Trim() ?? "";
                hero.ExamScore = score;

                (hero.Rank, hero.ThreatLevel) = Models.RankCalculator.FromScore(score);

                repo.SaveAll(heroes);
                SafeReloadGrid();
                MessageBox.Show("Superhero updated successfully!");
            }
            catch (IOException ioEx)
            {
                MessageBox.Show($"File error while updating: {ioEx.Message}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Unexpected error: {ex.Message}");
            }
        }

        private void WireGridSelection()
        {
            dgvHeroes.SelectionChanged -= dgvHeroes_SelectionChanged;
            dgvHeroes.SelectionChanged += dgvHeroes_SelectionChanged;
        }

        private void dgvHeroes_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvHeroes.CurrentRow?.DataBoundItem is Models.Superhero h)
            {
                txtHeroID.Text = h.HeroId;
                txtName.Text = h.Name;
                txtAge.Text = h.Age.ToString();
                txtPower.Text = h.Superpower;
                txtScore.Text = h.ExamScore.ToString();
            }
        }

        private void SafeReloadGrid()
        {
            try
            {
                dgvHeroes.DataSource = null;
                dgvHeroes.DataSource = repo.GetAll();
            }
            catch (IOException ioEx)
            {
                MessageBox.Show($"File error while loading: {ioEx.Message}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Unexpected error while loading: {ex.Message}");
            }
        }
    }
}
