
# Person 2 — View & Update Features (PRG2782)

This package contains a single **partial** class file that adds the required **View** and **Update** functionality to `MainForm` without touching Person 1's code.

## Files
- `UI/MainForm.Person2.cs` — Implements:
  - Viewing all superheroes (loads from `superheroes.txt` into `dgvHeroes`)
  - Populating textboxes on row selection
  - Updating a hero and recalculating rank/threat on score change
  - Basic file/validation error handling

## Assumed Control Names
- `DataGridView dgvHeroes`
- `TextBox txtHeroID, txtName, txtAge, txtPower, txtScore`
- `Button btnViewAll, btnUpdate`

If your names differ, either rename your controls in the designer or update references in `MainForm.Person2.cs`.

## Repository & Models
- Assumes existing:
  - `Services.SuperheroRepository` with `GetAll()` and `SaveAll(IEnumerable<Superhero>)`
  - `Models.Superhero` (properties: HeroId, Name, Age, Superpower, ExamScore, Rank, ThreatLevel)
  - `Models.RankCalculator.FromScore(int)` -> returns `(string rank, string threat)`.

If Person 1 already declared a `repo` field, delete the `repo` field in this file to avoid duplicates.

## Wiring
- Hook `MainForm_Load` to the form's Load event in the designer.
- Hook `btnViewAll.Click` to `btnViewAll_Click` and `btnUpdate.Click` to `btnUpdate_Click`.
- Ensure the grid's `SelectionChanged` event is wired automatically by the code (`WireGridSelection()` is called in `MainForm_Load`).

## Validation
- Age must be a number
- Score must be 0–100
- Rank/Threat are recalculated on update

## Git Commit Messages (Suggested)
- `feat(view): load superheroes into grid with error handling`
- `feat(update): edit hero, validate inputs, recompute rank/threat`
- `fix(ui): selection binding to textboxes`
- `chore: comments and minor refactors for person-2 features`
