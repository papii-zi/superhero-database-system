namespace SuperheroDatabase
{
   partial class Superheroesfrm : Form
{
    private System.ComponentModel.IContainer components = null;
    private System.Windows.Forms.Label lblHeroID;
    private System.Windows.Forms.Label lblName;
    private System.Windows.Forms.Label lblAge;
    private System.Windows.Forms.Label lblSuperpower;
    private System.Windows.Forms.Label lblExamScore;
    private System.Windows.Forms.TextBox txtHeroID;
    private System.Windows.Forms.TextBox txtName;
    private System.Windows.Forms.TextBox txtAge;
    private System.Windows.Forms.TextBox txtSuperpower;
    private System.Windows.Forms.TextBox txtExamScore;
    private System.Windows.Forms.Button btnAddHero;
    private System.Windows.Forms.Button btnViewAll;
    private System.Windows.Forms.DataGridView dgvHeroes;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        this.lblHeroID = new System.Windows.Forms.Label();
        this.lblName = new System.Windows.Forms.Label();
        this.lblAge = new System.Windows.Forms.Label();
        this.lblSuperpower = new System.Windows.Forms.Label();
        this.lblExamScore = new System.Windows.Forms.Label();
        this.txtHeroID = new System.Windows.Forms.TextBox();
        this.txtName = new System.Windows.Forms.TextBox();
        this.txtAge = new System.Windows.Forms.TextBox();
        this.txtSuperpower = new System.Windows.Forms.TextBox();
        this.txtExamScore = new System.Windows.Forms.TextBox();
        this.btnAddHero = new System.Windows.Forms.Button();
        this.btnViewAll = new System.Windows.Forms.Button();
        this.dgvHeroes = new System.Windows.Forms.DataGridView();
        ((System.ComponentModel.ISupportInitialize)(this.dgvHeroes)).BeginInit();
        this.SuspendLayout();
        
        // Labels and TextBoxes
        this.lblHeroID.Text = "Hero ID:";
        this.lblHeroID.Location = new System.Drawing.Point(20, 20);
        this.txtHeroID.Location = new System.Drawing.Point(120, 20);

        this.lblName.Text = "Name:";
        this.lblName.Location = new System.Drawing.Point(20, 50);
        this.txtName.Location = new System.Drawing.Point(120, 50);

        this.lblAge.Text = "Age:";
        this.lblAge.Location = new System.Drawing.Point(20, 80);
        this.txtAge.Location = new System.Drawing.Point(120, 80);

        this.lblSuperpower.Text = "Superpower:";
        this.lblSuperpower.Location = new System.Drawing.Point(20, 110);
        this.txtSuperpower.Location = new System.Drawing.Point(120, 110);

        this.lblExamScore.Text = "Exam Score:";
        this.lblExamScore.Location = new System.Drawing.Point(20, 140);
        this.txtExamScore.Location = new System.Drawing.Point(120, 140);

        // Buttons
        this.btnAddHero.Text = "Add Superhero";
        this.btnAddHero.Location = new System.Drawing.Point(20, 180);
        this.btnAddHero.Click += new System.EventHandler(this.btnAddHero_Click);

        this.btnViewAll.Text = "View All";
        this.btnViewAll.Location = new System.Drawing.Point(160, 180);
        this.btnViewAll.Click += new System.EventHandler(this.btnViewAll_Click);

        // DataGridView
        this.dgvHeroes.Location = new System.Drawing.Point(20, 220);
        this.dgvHeroes.Size = new System.Drawing.Size(740, 200);
        this.dgvHeroes.ColumnCount = 7;
        this.dgvHeroes.Columns[0].Name = "Hero ID";
        this.dgvHeroes.Columns[1].Name = "Name";
        this.dgvHeroes.Columns[2].Name = "Age";
        this.dgvHeroes.Columns[3].Name = "Superpower";
        this.dgvHeroes.Columns[4].Name = "Exam Score";
        this.dgvHeroes.Columns[5].Name = "Rank";
        this.dgvHeroes.Columns[6].Name = "Threat Level";

        // Form
        this.ClientSize = new System.Drawing.Size(800, 450);
        this.Controls.Add(this.lblHeroID);
        this.Controls.Add(this.txtHeroID);
        this.Controls.Add(this.lblName);
        this.Controls.Add(this.txtName);
        this.Controls.Add(this.lblAge);
        this.Controls.Add(this.txtAge);
        this.Controls.Add(this.lblSuperpower);
        this.Controls.Add(this.txtSuperpower);
        this.Controls.Add(this.lblExamScore);
        this.Controls.Add(this.txtExamScore);
        this.Controls.Add(this.btnAddHero);
        this.Controls.Add(this.btnViewAll);
        this.Controls.Add(this.dgvHeroes);
        this.Text = "Superhero Database";
        ((System.ComponentModel.ISupportInitialize)(this.dgvHeroes)).EndInit();
        this.ResumeLayout(false);
    }
}
}

