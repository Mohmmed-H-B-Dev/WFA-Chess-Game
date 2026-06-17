namespace WFA_Chess_Game
{
    partial class Main
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnWhite = new System.Windows.Forms.Button();
            this.btnBlack = new System.Windows.Forms.Button();
            this.btnResetGame = new System.Windows.Forms.Button();
            this.gbGrid = new System.Windows.Forms.GroupBox();
            this.customCTRL_PictureBox1 = new WFA_Chess_Game.CustomCTRL_PictureBox();
            this.lstHistory = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // btnWhite
            // 
            this.btnWhite.Location = new System.Drawing.Point(136, 356);
            this.btnWhite.Name = "btnWhite";
            this.btnWhite.Size = new System.Drawing.Size(75, 38);
            this.btnWhite.TabIndex = 2;
            this.btnWhite.Text = "White ";
            this.btnWhite.UseVisualStyleBackColor = true;
            this.btnWhite.Click += new System.EventHandler(this.btnWhite_Click);
            // 
            // btnBlack
            // 
            this.btnBlack.Location = new System.Drawing.Point(12, 356);
            this.btnBlack.Name = "btnBlack";
            this.btnBlack.Size = new System.Drawing.Size(75, 38);
            this.btnBlack.TabIndex = 3;
            this.btnBlack.Text = "Black";
            this.btnBlack.UseVisualStyleBackColor = true;
            this.btnBlack.Click += new System.EventHandler(this.btnBlack_Click);
            // 
            // btnResetGame
            // 
            this.btnResetGame.Font = new System.Drawing.Font("Tahoma", 12F);
            this.btnResetGame.Location = new System.Drawing.Point(261, 356);
            this.btnResetGame.Name = "btnResetGame";
            this.btnResetGame.Size = new System.Drawing.Size(109, 38);
            this.btnResetGame.TabIndex = 4;
            this.btnResetGame.Text = "Reset Game";
            this.btnResetGame.UseVisualStyleBackColor = true;
            this.btnResetGame.Click += new System.EventHandler(this.btnResetGame_Click);
            // 
            // gbGrid
            // 
            this.gbGrid.Font = new System.Drawing.Font("Tahoma", 13F);
            this.gbGrid.Location = new System.Drawing.Point(3, 2);
            this.gbGrid.Name = "gbGrid";
            this.gbGrid.Size = new System.Drawing.Size(379, 336);
            this.gbGrid.TabIndex = 5;
            this.gbGrid.TabStop = false;
            this.gbGrid.Text = "Grid ";
            // 
            // customCTRL_PictureBox1
            // 
            this.customCTRL_PictureBox1._Bishop = null;
            this.customCTRL_PictureBox1._King = null;
            this.customCTRL_PictureBox1._Knight = null;
            this.customCTRL_PictureBox1._Pawn = null;
            this.customCTRL_PictureBox1._PieceEmpty = null;
            this.customCTRL_PictureBox1._Queen = null;
            this.customCTRL_PictureBox1._Rook = null;
            this.customCTRL_PictureBox1.BackColor = System.Drawing.Color.CornflowerBlue;
            this.customCTRL_PictureBox1.IdCol = 0;
            this.customCTRL_PictureBox1.IdPbLocationInCols = 0;
            this.customCTRL_PictureBox1.IdPbLocationInRows = 0;
            this.customCTRL_PictureBox1.IdRow = 0;
            this.customCTRL_PictureBox1.IsKingInCheck = false;
            this.customCTRL_PictureBox1.Location = new System.Drawing.Point(405, 356);
            this.customCTRL_PictureBox1.Name = "customCTRL_PictureBox1";
            this.customCTRL_PictureBox1.pbID = 0;
            this.customCTRL_PictureBox1.PieceColor = System.Drawing.Color.Empty;
            this.customCTRL_PictureBox1.Size = new System.Drawing.Size(45, 33);
            this.customCTRL_PictureBox1.StatusMove = false;
            this.customCTRL_PictureBox1.TabIndex = 0;
            this.customCTRL_PictureBox1.Text = "customCTRL_PictureBox1";
            // 
            // lstHistory
            // 
            this.lstHistory.Font = new System.Drawing.Font("Tahoma", 12F);
            this.lstHistory.FormattingEnabled = true;
            this.lstHistory.ItemHeight = 19;
            this.lstHistory.Location = new System.Drawing.Point(405, 19);
            this.lstHistory.Name = "lstHistory";
            this.lstHistory.Size = new System.Drawing.Size(371, 327);
            this.lstHistory.TabIndex = 6;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.CornflowerBlue;
            this.ClientSize = new System.Drawing.Size(788, 401);
            this.Controls.Add(this.lstHistory);
            this.Controls.Add(this.customCTRL_PictureBox1);
            this.Controls.Add(this.gbGrid);
            this.Controls.Add(this.btnResetGame);
            this.Controls.Add(this.btnBlack);
            this.Controls.Add(this.btnWhite);
            this.Name = "Form1";
            this.Text = "Form1";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnWhite;
        private System.Windows.Forms.Button btnBlack;
        private System.Windows.Forms.Button btnResetGame;
        private System.Windows.Forms.GroupBox gbGrid;
        private CustomCTRL_PictureBox customCTRL_PictureBox1;
        private System.Windows.Forms.ListBox lstHistory;
    }
}