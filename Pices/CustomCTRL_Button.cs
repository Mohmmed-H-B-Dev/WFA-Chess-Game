using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WFA_Chess_Game
{
    public  partial class CustomCTRL_Button : Button
    {
    
        public CustomCTRL_Button()
        {
            InitializeComponent();
        }
        public enum enCheesPieces { Pawn = 1, Knight = 2, Bishop = 3, Rook = 4, Queen = 5, King = 6 };

        enCheesPieces _CheesPieceName =enCheesPieces.Pawn;
        public int SetCheesPieceName { set { _CheesPieceName=(enCheesPieces)value; } }
        public enCheesPieces GetCheesPieceName { get { return _CheesPieceName; }  }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
        }

    }
}
