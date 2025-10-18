# Superhero Database System

A Windows Forms application for managing superhero trainee records at One Kick Heroes Academy. Created for PRG2782 Project 2025.

# Project Features
- Add new superheroes with automatic rank calculation
- View all superheroes in a data grid  
- Update superhero information
- Delete superhero records
- Generate summary reports

# Team Members & Roles
- **Aphiwe Shabalala**: Interface & Add Functionality
- **Ryno Hartman**: View & Update Functionality
- **Ethan Ogle**: Delete & Summary Report
- **Kuzivakwashe Bvunyenge**: Version Control & Documentation

# How to Run
1. Clone this repository
2. Open `SuperheroDatabase.sln` in Visual Studio
3. Build the solution (Ctrl + Shift + B)
4. Run the application (F5)

# Git Collaboration
- Using Git for version control with frequent commits
- Each member works on their assigned features
- Regular pushes to GitHub to maintain project sync

# Project Structure

```
SuperheroDatabase/
├── Models/                    # Data models
│   ├── Superhero.cs          # Superhero entity class
│   └── RankCalculator.cs     # Rank/threat level calculator
├── Services/                  # Business logic layer
│   └── SuperheroRepository.cs # Data persistence service
├── UI/                        # User interface forms
│   ├── SuperheroesForm.cs    # Main application form
│   └── SuperheroesForm.Designer.cs
├── Data/                      # Data storage
│   ├── superheroes.txt       # Superhero records (CSV)
│   └── summary.txt           # Generated summary reports
├── Properties/                # Project properties
└── Program.cs                # Application entry point
```

# Technical Details

## Architecture
- **3-Tier Architecture**: UI → Services → Data
- **Namespace Structure**:
  - `SuperheroDatabase.Models` - Data entities
  - `SuperheroDatabase.Services` - Business logic
  - `SuperheroDatabase.UI` - User interface

## Data Format
CSV format: `HeroID,Name,Age,Superpower,ExamScore,Rank,ThreatLevel`