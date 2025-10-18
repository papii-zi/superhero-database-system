# Superhero Database - SuperheroDatabase Application

## Overview
Windows Forms application for managing superhero trainee records at One Kick Heroes Academy.

## Architecture
This application follows a **3-Tier Architecture** pattern:

```
┌─────────────────────────────────────────┐
│         Presentation Layer              │
│         (SuperheroDatabase.UI)          │
│  ┌─────────────────────────────────┐   │
│  │    SuperheroesForm.cs           │   │
│  │  - Add/View/Delete/Report UI    │   │
│  └─────────────────────────────────┘   │
└─────────────────┬───────────────────────┘
                  │
                  ▼
┌─────────────────────────────────────────┐
│       Business Logic Layer              │
│      (SuperheroDatabase.Services)       │
│  ┌─────────────────────────────────┐   │
│  │   SuperheroRepository.cs        │   │
│  │  - CRUD Operations              │   │
│  │  - Data Validation              │   │
│  └─────────────────────────────────┘   │
└─────────────────┬───────────────────────┘
                  │
                  ▼
┌─────────────────────────────────────────┐
│         Data Model Layer                │
│      (SuperheroDatabase.Models)         │
│  ┌──────────────────┬─────────────┐    │
│  │  Superhero.cs    │ RankCalc... │    │
│  │  - Entity Class  │ - Rankings  │    │
│  └──────────────────┴─────────────┘    │
└─────────────────┬───────────────────────┘
                  │
                  ▼
              ┌───────┐
              │ Data/ │
              │ Folder│
              └───────┘
```

## Features by Team Member

### Person 1: Aphiwe Shabalala - Add Functionality
- ✅ User interface design
- ✅ Add superhero with validation
- ✅ Automatic rank calculation
- ✅ Duplicate ID prevention

### Person 2: Ryno Hartman - View & Update
- ✅ View all superheroes in grid
- ✅ Update superhero information
- ✅ Grid selection handling
- ✅ Data refresh on changes

### Person 3: Ethan Ogle - Delete & Reports
- ✅ Delete with confirmation dialog
- ✅ Generate summary statistics
- ✅ Save reports to file
- ✅ Architecture restructuring

### Person 4: Kuzivakwashe Bvunyenge - Version Control
- ✅ Git repository management
- ✅ Documentation maintenance
- ✅ Commit coordination
- ✅ Change tracking

## Project Structure

```
SuperheroDatabase/
├── Models/                        Data entities
│   ├── Superhero.cs              Hero entity with CSV methods
│   └── RankCalculator.cs         Rank/threat calculation logic
│
├── Services/                      Business logic
│   └── SuperheroRepository.cs    Data access layer (CRUD)
│
├── UI/                            User interface
│   ├── SuperheroesForm.cs        Main form logic
│   └── SuperheroesForm.Designer.cs Form designer
│
├── Data/                          Data storage
│   ├── superheroes.txt           Hero records (CSV)
│   └── summary.txt               Generated reports
│
├── Properties/                    Project metadata
│   ├── AssemblyInfo.cs
│   ├── Resources.Designer.cs
│   ├── Resources.resx
│   ├── Settings.Designer.cs
│   └── Settings.settings
│
├── Program.cs                     Application entry point
├── App.config                     Configuration
└── SuperheroDatabase.csproj      Project file
```

## Technology Stack

- **Framework**: .NET Framework 4.7.2
- **UI**: Windows Forms
- **Data Storage**: CSV text files
- **Architecture**: 3-Tier (UI → Services → Data)
- **Patterns**: Repository, Entity, Static Factory

## How to Build & Run

### Using Visual Studio
1. Open `SuperheroDatabase.sln`
2. Build: `Ctrl + Shift + B`
3. Run: `F5`

### Using Command Line
```bash
cd SuperheroDatabase
msbuild SuperheroDatabase.csproj /t:Rebuild /p:Configuration=Debug
cd bin\Debug
SuperheroDatabase.exe
```

## Data Format

### CSV Structure
```csv
HeroID,Name,Age,Superpower,ExamScore,Rank,ThreatLevel
H001,Flash Gordon,22,Super Speed,95,S,Finals Week
H002,Wonder Jane,24,Super Strength,75,A,Midterm Madness
```

### Rank Calculation Rules
| Score Range | Rank | Threat Level |
|-------------|------|--------------|
| 81-100 | S | Finals Week |
| 61-80 | A | Midterm Madness |
| 41-60 | B | Group Project Gone Wrong |
| 0-40 | C | Pop Quiz |

## Key Classes

### Superhero (Model)
```csharp
public class Superhero
{
    public string HeroId { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
    public string Superpower { get; set; }
    public int ExamScore { get; set; }
    public string Rank { get; set; }
    public string ThreatLevel { get; set; }
}
```

### SuperheroRepository (Service)
```csharp
public class SuperheroRepository
{
    List<Superhero> GetAll()
    void Add(Superhero hero)
    bool Update(string heroId, Superhero hero)
    bool Delete(string heroId)
    Superhero GetById(string heroId)
    bool ExistsById(string heroId)
}
```

## Features

### Add Superhero
- Enter hero details (ID, Name, Age, Superpower, Score)
- Automatic rank calculation based on exam score
- Duplicate ID validation
- Data persistence to CSV file

### View All Superheroes
- Display all heroes in DataGridView
- Shows all 7 fields
- Sortable columns
- Row selection support

### Delete Superhero
- Select hero by ID or grid selection
- Confirmation dialog with hero name
- Safe deletion with file update
- Grid refresh after deletion

### Generate Summary Report
- Calculate total hero count
- Compute average age and score
- Count heroes by rank (S/A/B/C)
- Display in popup window
- Save to `Data/summary.txt`

## Error Handling

All operations include:
- Input validation with user-friendly messages
- Try-catch blocks for I/O operations
- File existence checks
- Data format validation
- Graceful error recovery

## Documentation

- **README.md** - This file (project overview)
- **ARCHITECTURE.md** - Detailed architecture documentation
- **MIGRATION_GUIDE.md** - Restructuring guide
- **RESTRUCTURING_SUMMARY.md** - Summary of changes
- **README_Person2.md** - Person 2 features
- **README_Person3.md** - Person 3 features

## Testing

Run through this checklist:
- [ ] Add a new superhero
- [ ] View all superheroes
- [ ] Update superhero information
- [ ] Delete a superhero with confirmation
- [ ] Generate summary report
- [ ] Verify Data/superheroes.txt is updated
- [ ] Verify Data/summary.txt is created

## Version Control

```bash
# Clone repository
git clone https://github.com/papii-zi/superhero-database-system.git

# Create feature branch
git checkout -b feature/your-feature

# Commit changes
git add .
git commit -m "feat: your feature description"

# Push to remote
git push origin feature/your-feature
```

## Future Enhancements

### Planned Features
- [ ] Database integration (SQL Server)
- [ ] Search and filter functionality
- [ ] Export to Excel/PDF
- [ ] Visual statistics (charts)
- [ ] User authentication
- [ ] Backup/restore functionality
- [ ] Multi-language support

### Technical Improvements
- [ ] Unit test coverage
- [ ] Integration tests
- [ ] Logging framework
- [ ] Configuration management
- [ ] Performance optimization

## Team

- **Aphiwe Shabalala** - Interface & Add Functionality
- **Ryno Hartman** - View & Update Functionality
- **Ethan Ogle** - Delete, Summary Reports & Architecture
- **Kuzivakwashe Bvunyenge** - Version Control & Documentation

## Course Information

- **Course**: PRG2782
- **Project**: Superhero Database System
- **Year**: 2025
- **Institution**: Belgium Campus ITversity

## License

Educational project for PRG2782 course.

## Contact

For questions or issues, contact your team member or instructor.

---

**Last Updated**: October 2025  
**Version**: 2.0 (Restructured)  
**Status**: ✅ Fully Operational

