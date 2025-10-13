using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SuperheroDatabase
{
    public partial class Superheroesfrm : Form
 {
     private string filePath = "superheroes.txt"; // File where superhero data is stored

     public Superheroesfrm()
     {
         InitializeComponent();
     }

     // Add Superhero
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

             if (!double.TryParse(txtExamScore.Text, out double score) || score < 0 || score > 100)
             {
                 MessageBox.Show("Exam Score must be between 0 and 100!", "Validation Error");
                 return;
             }

             // Calculate rank and threat level
             string rank = GetRank(score);
             string threat = GetThreat(rank);

             // Format superhero record
             string record = $"{txtHeroID.Text},{txtName.Text},{age},{txtSuperpower.Text},{score},{rank},{threat}";
             File.AppendAllText(filePath, record + Environment.NewLine);

             MessageBox.Show("Superhero added successfully!", "Success");
             ClearFields();
         }
         catch (Exception ex)
         {
             MessageBox.Show($"An error occurred: {ex.Message}", "Error");
         }
     }

     // View All Superheroes
     private void btnViewAll_Click(object sender, EventArgs e)
     {
         try
         {
             dgvHeroes.Rows.Clear();

             if (!File.Exists(filePath))
             {
                 MessageBox.Show("No data file found! Please add heroes first.", "File Not Found");
                 return;
             }

             string[] lines = File.ReadAllLines(filePath);
             foreach (string line in lines)
             {
                 string[] parts = line.Split(',');
                 if (parts.Length == 7)
                     dgvHeroes.Rows.Add(parts);
             }
         }
         catch (Exception ex)
         {
             MessageBox.Show($"Error reading file: {ex.Message}", "Error");
         }
     }

     // Determine Rank
     private string GetRank(double score)
     {
         if (score >= 81) return "S";
         if (score >= 61) return "A";
         if (score >= 41) return "B";
         return "C";
     }

     // Determine Threat Level
     private string GetThreat(string rank)
     {
         switch (rank)
         {
             case "S": return "Finals Week";
             case "A": return "Midterm Madness";
             case "B": return "Group Project Gone Wrong";
             default: return "Pop Quiz";
         }
     }

     // Clear all input fields
     private void ClearFields()
     {
         txtHeroID.Clear();
         txtName.Clear();
         txtAge.Clear();
         txtSuperpower.Clear();
         txtExamScore.Clear();
     }

     private void InitializeComponent()
     {
         this.SuspendLayout();
         // 
         // Superheroesfrm
         // 
         this.ClientSize = new System.Drawing.Size(527, 315);
         this.Name = "Superheroesfrm";
         this.ResumeLayout(false);

     }
 }
}
