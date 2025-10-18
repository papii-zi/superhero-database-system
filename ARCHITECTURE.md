# Superhero Database System - Architecture Documentation

## Overview
This document describes the architecture and organization of the Superhero Database System project for PRG2782 Project 2025.

## Project Structure

```
SuperheroDatabase/
│
├── Models/                          # Data Models Layer
│   ├── Superhero.cs                # Entity class representing a superhero
│   └── RankCalculator.cs           # Utility for rank/threat calculations
│
├── Services/                        # Business Logic Layer
│   └── SuperheroRepository.cs      # Data access and persistence
│
├── UI/                              # Presentation Layer
│   ├── SuperheroesForm.cs         # Main form code-behind
│   └── SuperheroesForm.Designer.cs # Form designer code
│
├── Data/                            # Data Storage
│   ├── superheroes.txt            # Superhero records (CSV)
│   └── summary.txt                # Generated reports
│
├── Properties/                      # Project Metadata
│   ├── AssemblyInfo.cs
│   ├── Resources.Designer.cs
│   ├── Resources.resx
│   ├── Settings.Designer.cs
│   └── Settings.settings
│
├── Program.cs                       # Application entry point
├── App.config                       # Application configuration
└── SuperheroDatabase.csproj        # Project file
```

## Architecture Pattern

The application follows a **3-Tier Architecture** pattern:

### 1. Presentation Layer (UI)
- **Namespace**: `SuperheroDatabase.UI`
- **Components**: `SuperheroesForm`
- **Responsibilities**:
  - User interface and user interaction
  - Input validation and error display
  - Calling service layer for data operations
  - Displaying data in DataGridView

### 2. Business Logic Layer (Services)
- **Namespace**: `SuperheroDatabase.Services`
- **Components**: `SuperheroRepository`
- **Responsibilities**:
  - Data access logic
  - CRUD operations (Create, Read, Update, Delete)
  - File I/O management
  - Data integrity validation

### 3. Data Model Layer (Models)
- **Namespace**: `SuperheroDatabase.Models`
- **Components**: `Superhero`, `RankCalculator`
- **Responsibilities**:
  - Data entity definitions
  - Business rule calculations (rank/threat)
  - Data serialization/deserialization

## Key Classes

### Superhero (Model)
```csharp
namespace SuperheroDatabase.Models
{
    public class Superhero
    {
        public string HeroId { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Superpower { get; set; }
        public int ExamScore { get; set; }
        public string Rank { get; set; }
        public string ThreatLevel { get; set; }
        
        public string ToCsvString()
        public static Superhero FromCsvString(string csvLine)
    }
}
```

**Purpose**: Represents a superhero trainee with all their attributes.

**Key Methods**:
- `ToCsvString()`: Serializes object to CSV format
- `FromCsvString()`: Deserializes CSV string to object

### RankCalculator (Model)
```csharp
namespace SuperheroDatabase.Models
{
    public static class RankCalculator
    {
        public static (string Rank, string ThreatLevel) FromScore(int examScore)
    }
}
```

**Purpose**: Calculates rank and threat level based on exam scores.

**Rank Calculation Rules**:
- Score >= 81: S-Rank → "Finals Week"
- Score >= 61: A-Rank → "Midterm Madness"
- Score >= 41: B-Rank → "Group Project Gone Wrong"
- Score < 41: C-Rank → "Pop Quiz"

### SuperheroRepository (Service)
```csharp
namespace SuperheroDatabase.Services
{
    public class SuperheroRepository
    {
        public List<Superhero> GetAll()
        public void Add(Superhero hero)
        public bool Update(string heroId, Superhero updatedHero)
        public bool Delete(string heroId)
        public Superhero GetById(string heroId)
        public bool ExistsById(string heroId)
        public void SaveAll(IEnumerable<Superhero> heroes)
    }
}
```

**Purpose**: Manages all data persistence operations.

**Key Features**:
- File path management
- CRUD operations
- Data validation
- Error handling for I/O operations

### SuperheroesForm (UI)
```csharp
namespace SuperheroDatabase.UI
{
    public partial class SuperheroesForm : Form
    {
        // Person 1 Features
        private void btnAddHero_Click(object sender, EventArgs e)
        
        // Person 1 & 2 Features
        private void btnViewAll_Click(object sender, EventArgs e)
        
        // Person 3 Features
        private void btnDelete_Click(object sender, EventArgs e)
        private void btnGenerateReport_Click(object sender, EventArgs e)
        private string GetSelectedHeroID()
        
        // Utility
        private void ClearFields()
    }
}
```

**Purpose**: Main application form with all user interface functionality.

**Features by Person**:
- **Person 1 (Aphiwe)**: Add Superhero functionality
- **Person 2 (Ryno)**: View All and Update functionality
- **Person 3 (Ethan)**: Delete and Summary Report functionality

## Data Flow

### Adding a Superhero
```
User Input → Validation → RankCalculator.FromScore() → 
Create Superhero Object → Repository.Add() → File System
```

### Viewing All Superheroes
```
User Click → Repository.GetAll() → Parse CSV → 
Create Superhero Objects → Display in DataGridView
```

### Deleting a Superhero
```
User Selection → GetSelectedHeroID() → Repository.GetById() →
Confirmation Dialog → Repository.Delete() → Refresh Grid
```

### Generating Report
```
User Click → Repository.GetAll() → Calculate Statistics →
Format Report → Display MessageBox → Save to File
```

## File Storage

### Data Format (CSV)
```
HeroID,Name,Age,Superpower,ExamScore,Rank,ThreatLevel
H001,Flash Gordon,22,Super Speed,95,S,Finals Week
H002,Wonder Jane,24,Super Strength,75,A,Midterm Madness
```

### File Locations
- **Superhero Data**: `Data/superheroes.txt`
- **Summary Reports**: `Data/summary.txt`

## Design Patterns Used

### 1. Repository Pattern
- **Class**: `SuperheroRepository`
- **Benefit**: Abstracts data access, makes code testable and maintainable

### 2. Entity Pattern
- **Class**: `Superhero`
- **Benefit**: Encapsulates data and related operations

### 3. Static Factory Pattern
- **Class**: `RankCalculator`
- **Benefit**: Centralizes business logic for calculations

### 4. Separation of Concerns
- **Implementation**: 3-Tier Architecture
- **Benefit**: Clear boundaries between UI, logic, and data

## Error Handling Strategy

### UI Layer
- Input validation with user-friendly messages
- Try-catch blocks around all service calls
- MessageBox notifications for errors

### Service Layer
- File existence checks
- IOException handling
- Data integrity validation

### Model Layer
- Null checking in static methods
- Safe parsing with TryParse methods

## Extensibility Points

The architecture supports easy extensions:

1. **Database Integration**: Replace repository file operations with database calls
2. **New Forms**: Add new forms in UI folder
3. **Business Rules**: Extend RankCalculator for new calculations
4. **Export Formats**: Add methods to Superhero for JSON/XML export
5. **Authentication**: Add user management service layer

## Testing Strategy

### Unit Testing (Future)
- Test RankCalculator logic
- Test Superhero serialization
- Mock repository for UI testing

### Integration Testing
- Test file I/O operations
- Test full CRUD cycle
- Test report generation

## Version Control Structure

```
main/
├── SuperheroDatabase/    # Application code
├── README.md            # Project overview
├── README_Person2.md    # Person 2 documentation
├── README_Person3.md    # Person 3 documentation
├── ARCHITECTURE.md      # This file
└── .gitignore          # Git ignore rules
```

## Dependencies

- .NET Framework 4.7.2
- System.Windows.Forms
- System.Linq
- System.IO

## Build Configuration

- **Debug**: Full debugging symbols, no optimization
- **Release**: Optimized code, minimal symbols
- **Output**: `bin/Debug/` or `bin/Release/`

## Contributing Guidelines

1. Each person works in their assigned feature area
2. Follow namespace conventions
3. Add XML documentation comments
4. Handle exceptions appropriately
5. Test before committing
6. Update relevant README files

## Future Enhancements

1. **Database Support**: SQL Server or SQLite integration
2. **Search Functionality**: Filter heroes by various criteria
3. **Export/Import**: JSON, XML, Excel formats
4. **Charts/Graphs**: Visual statistics for reports
5. **Multi-user Support**: Authentication and authorization
6. **Backup/Restore**: Automated data backup system

## Team Contributions

- **Person 1 (Aphiwe Shabalala)**: Initial form design and Add functionality
- **Person 2 (Ryno Hartman)**: View and Update functionality
- **Person 3 (Ethan Ogle)**: Delete, Reports, and Architecture restructuring
- **Person 4 (Kuzivakwashe Bvunyenge)**: Version control and documentation

---

**Last Updated**: October 2025  
**Project**: PRG2782 - Superhero Database System  
**Institution**: One Kick Heroes Academy

