using System.Collections.Generic;

namespace WFA_Chess_Game
{
    public class clsKing : ChessGame
    {
        Position _Current_Pos;
        Position Current_Pos;
        public clsKing(int row, int col)
        : base(row, col)
        {
            _Current_Pos.row = row;
            _Current_Pos.col = col;
            Current_Pos=_Current_Pos;
        }

        

        CustomCTRL_PictureBox _TempPictureBox = null;
        public CustomCTRL_PictureBox T = null;
        public Position GetPosition() { return _Current_Pos; }

        public void SetPosition(int row, int col) { _Current_Pos.row=row; _Current_Pos.col=col; }
        /// <summary>
        /// Move One Step : Lift , Right,Forward, Backwards
        /// 
        /// </summary>
        /// <param name="New_pos"></param>
        /// <returns></returns>
        private bool _MoveOneStep(CustomCTRL_PictureBox New_pos)
        {
            if (_Current_Pos.row==New_pos.IdRow)
            {
                if (_Current_Pos.col==New_pos.IdCol+1||_Current_Pos.col==New_pos.IdCol-1)
                {
                    return true;
                }
                else
                    return false;
            }
            else if (_Current_Pos.col==New_pos.IdCol)
            {
                if (_Current_Pos.row==New_pos.IdRow+1||_Current_Pos.row==New_pos.IdRow-1)
                {
                    return true;
                }
                else
                    return false;
            }
            else
            {
                if (_Current_Pos.row!=New_pos.IdRow&&_Current_Pos.col!=New_pos.IdCol)
                {
                    if ((_Current_Pos.col+1==New_pos.IdCol&&_Current_Pos.row-1==New_pos.IdRow)||(_Current_Pos.col-1==New_pos.IdCol&&_Current_Pos.row-1==New_pos.IdRow))
                    {
                        return true;
                    }
                    else if ((_Current_Pos.col+1==New_pos.IdCol&&_Current_Pos.row+1==New_pos.IdRow)||(_Current_Pos.col-1==New_pos.IdCol&&_Current_Pos.row+1==New_pos.IdRow))
                    {
                        return true;
                    }else 
                        return false;
                }
            }

            return false;

        }

        /// <summary>
        /// Check is valid for move or not .
        /// and it will get change position pieces if is valid.
        /// </summary>
        /// <param name="New_pos"></param>
        /// <param name="_PictureBoxGrid"></param>
        /// <returns></returns>
        public override bool _IsValidMove(CustomCTRL_PictureBox New_pos, ref CustomCTRL_PictureBox[,] _PictureBoxGrid)
        {

            if (_MoveOneStep(New_pos))
            {
                //if (_PictureBoxGrid[New_pos.col, New_pos.row].GetCheesPieceType!=_PictureBoxGrid[this.GetPosition().col, this.GetPosition().row].GetCheesPieceType &&
                //     _PictureBoxGrid[New_pos.col, New_pos.row].GetCheesPieceName!=CustomCTRL_PictureBox.enCheesPieces.Empty)
                //{
                //    return ProcessAvailableCaptures(ref _PictureBoxGrid[this.GetPosition().col, this.GetPosition().row], ref _PictureBoxGrid[New_pos.col, New_pos.row]);
                //    //   Execute Function Prosses Available Captures
                //}




                _TempPictureBox=new CustomCTRL_PictureBox();

                _TempPictureBox.SetCheesPieceName=(int)_PictureBoxGrid[this.GetPosition().row, this.GetPosition().col].GetCheesPieceName;
                _TempPictureBox.BackgroundImage=_PictureBoxGrid[this.GetPosition().row, this.GetPosition().col].BackgroundImage;
                _TempPictureBox.SetCheesPieceType=_PictureBoxGrid[this.GetPosition().row, this.GetPosition().col].GetCheesPieceType;
                _TempPictureBox._King=new clsKing(this.GetPosition().row, this.GetPosition().col);

                _PictureBoxGrid[New_pos.IdRow, New_pos.IdCol].SetCheesPieceName=(int)_TempPictureBox.GetCheesPieceName;
                _PictureBoxGrid[New_pos.IdRow, New_pos.IdCol].BackgroundImage =_TempPictureBox.BackgroundImage;
                _PictureBoxGrid[New_pos.IdRow, New_pos.IdCol].SetCheesPieceType=_TempPictureBox.GetCheesPieceType;
                _PictureBoxGrid[New_pos.IdRow, New_pos.IdCol]._King=new clsKing(New_pos.IdRow, New_pos.IdCol);
                _PictureBoxGrid[New_pos.IdRow, New_pos.IdCol]._King.CurrentCTRL=_PictureBoxGrid[this.GetPosition().row, this.GetPosition().col]._King.CurrentCTRL;

                _PictureBoxGrid[this.GetPosition().row, this.GetPosition().col].SetCheesPieceName=(int)CustomCTRL_PictureBox.enCheesPieces.Empty;
                _PictureBoxGrid[this.GetPosition().row, this.GetPosition().col].SetCheesPieceType=CustomCTRL_PictureBox.enCheesPiecesType.Empty;
                _PictureBoxGrid[this.GetPosition().row, this.GetPosition().col].BackgroundImage=null;


                this.SetPosition(New_pos.IdRow, New_pos.IdCol);
           
                return true;
            }
            return false;

        }

        private bool _IsVerticalKnightAroundMe(CustomCTRL_PictureBox[,] _PictureBoxGrid)
        {

            if (_IsNumberBetweenBoard(Current_Pos.row+2)&&_IsNumberBetweenBoard(Current_Pos.col+1))
            {
                if (_PictureBoxGrid[Current_Pos.row+2, Current_Pos.col+1].GetCheesPieceName==CustomCTRL_PictureBox.enCheesPieces.Knight)
                {
                    if (_IsNotTypeColorSame(_PictureBoxGrid[Current_Pos.row+2, Current_Pos.col+1]))
                    {
                        //return IsKindInCheck
                        return true;
                    }
                }
            }
            else if (_IsNumberBetweenBoard(Current_Pos.row+2)&&_IsNumberBetweenBoard(Current_Pos.col-1))
            {
                if (_PictureBoxGrid[Current_Pos.row+2, Current_Pos.col-1].GetCheesPieceName==CustomCTRL_PictureBox.enCheesPieces.Knight)
                {
                    if (_IsNotTypeColorSame(_PictureBoxGrid[Current_Pos.row+2, Current_Pos.col-1]))
                    {
                        //return IsKindInCheck
                        return true;
                    }
                }
            }



            if (_IsNumberBetweenBoard(Current_Pos.row-2)&&_IsNumberBetweenBoard(Current_Pos.col-1))
            {
                if (_PictureBoxGrid[Current_Pos.row-2, Current_Pos.col-1].GetCheesPieceName==CustomCTRL_PictureBox.enCheesPieces.Knight)
                {
                    if (_IsNotTypeColorSame(_PictureBoxGrid[Current_Pos.row-2, Current_Pos.col-1]))
                    {
                        //return IsKindInCheck
                        return true;
                    }
                }
            }
            else if (_IsNumberBetweenBoard(Current_Pos.row-2)&&_IsNumberBetweenBoard(Current_Pos.col+1))
            {
                if (_PictureBoxGrid[Current_Pos.row-2, Current_Pos.col+1].GetCheesPieceName==CustomCTRL_PictureBox.enCheesPieces.Knight)
                {
                    if (_IsNotTypeColorSame(_PictureBoxGrid[Current_Pos.row-2, Current_Pos.col+1]))
                    {
                        //return IsKindInCheck
                        return true;
                    }
                }
            }








            return false;
        }

        private bool _IsHorizontalKnightAroundMe(CustomCTRL_PictureBox[,] _PictureBoxGrid)
        {




            if (_IsNumberBetweenBoard(Current_Pos.col+2)&&_IsNumberBetweenBoard(Current_Pos.row+1))
            {
                if (_PictureBoxGrid[Current_Pos.row+1, Current_Pos.col+2].GetCheesPieceName==CustomCTRL_PictureBox.enCheesPieces.Knight)
                {
                    if (_IsNotTypeColorSame(_PictureBoxGrid[Current_Pos.row+1, Current_Pos.col+2]))
                    {
                        //return IsKindInCheck
                        return true;
                    }
                }
            }
            else if (_IsNumberBetweenBoard(Current_Pos.col+2)&&_IsNumberBetweenBoard(Current_Pos.row-1))
            {
                if (_PictureBoxGrid[Current_Pos.row-1, Current_Pos.col+2].GetCheesPieceName==CustomCTRL_PictureBox.enCheesPieces.Knight)
                {
                    if (_IsNotTypeColorSame(_PictureBoxGrid[Current_Pos.row-1, Current_Pos.col+2]))
                    {
                        //return IsKindInCheck
                        return true;
                    }
                }
            }

            if (_IsNumberBetweenBoard(Current_Pos.col-2)&&_IsNumberBetweenBoard(Current_Pos.row+1))
            {
                if (_PictureBoxGrid[Current_Pos.row + 1, Current_Pos.col-2].GetCheesPieceName==CustomCTRL_PictureBox.enCheesPieces.Knight)
                {
                    if (_IsNotTypeColorSame(_PictureBoxGrid[Current_Pos.row + 1, Current_Pos.col-2]))
                    {
                        //return IsKindInCheck
                        return true;
                    }
                }
            }
            else if (_IsNumberBetweenBoard(Current_Pos.col-2)&&_IsNumberBetweenBoard(Current_Pos.row-1))
            {
                if (_PictureBoxGrid[Current_Pos.row -1, Current_Pos.col-2].GetCheesPieceName==CustomCTRL_PictureBox.enCheesPieces.Knight)
                {
                    if (_IsNotTypeColorSame(_PictureBoxGrid[Current_Pos.row - 1, Current_Pos.col-2]))
                    {
                        //return IsKindInCheck
                        return true;
                    }
                }
            }

            return false;
        }



        public static CustomCTRL_PictureBox GetKingPoistion(CustomCTRL_PictureBox[,] _PictureBoxGrid, CustomCTRL_PictureBox.enCheesPiecesType type)
        {
            foreach (CustomCTRL_PictureBox ccpb in _PictureBoxGrid)
            {
                if (ccpb.GetCheesPieceType==type&&ccpb.GetCheesPieceName==CustomCTRL_PictureBox.enCheesPieces.King)
                {
                    return ccpb;
                }

            }
            return null;
        }

        private bool _CheckDown(CustomCTRL_PictureBox[,] _PictureBoxGrid)
        {
           
            if (_Current_Pos.row>=0)
            {
                for (int i = _Current_Pos.row; i>=0; i++)
                {
                    if (i>7||i<0) return false;
                    if (_PictureBoxGrid[i, _Current_Pos.col].CheckIsPiece()&&!_PictureBoxGrid[i, _Current_Pos.col].IsKing())
                    {

                        if (!(_IsTypeColorSame(_PictureBoxGrid[i, _Current_Pos.col])))
                        {
                            if (_PictureBoxGrid[i, _Current_Pos.col].IsRook()||_PictureBoxGrid[i, _Current_Pos.col].IsQueen())
                            {
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                        else
                        {
                            return false;
                        }

                    }
                }
            }



            return false;
        }
        private bool _CheckUp(CustomCTRL_PictureBox[,] _PictureBoxGrid)
        {

            if (_Current_Pos.row<=7)
            {
                for (int i = _Current_Pos.row; i<=7; i--)
                {
                    if (i>7||i<0) return false;
                    if (_PictureBoxGrid[i, _Current_Pos.col].CheckIsPiece()&&!_PictureBoxGrid[i, _Current_Pos.col].IsKing())
                    {

                        if (!(_IsTypeColorSame(_PictureBoxGrid[i, _Current_Pos.col])))
                        {
                            if (_PictureBoxGrid[i, _Current_Pos.col].IsRook()||_PictureBoxGrid[i, _Current_Pos.col].IsQueen())
                            {
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                        else
                        {
                            return false;
                        }

                    }
                }
            }



            return false;
        }
        private bool _CheckRight(CustomCTRL_PictureBox[,] _PictureBoxGrid)
        {
            if (_Current_Pos.col>=0)
            {
                for (int i = _Current_Pos.col; i>=0; i++)
                {
                    if (i>7||i<0) return false;
                    if (_PictureBoxGrid[_Current_Pos.row, i].CheckIsPiece()&&!_PictureBoxGrid[i, _Current_Pos.col].IsKing())
                    {

                        if (!(_IsTypeColorSame(_PictureBoxGrid[_Current_Pos.row, i])))
                        {
                            if (_PictureBoxGrid[_Current_Pos.row, i].IsRook()||_PictureBoxGrid[_Current_Pos.row, i].IsQueen())
                            {
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                        else
                        {
                            return false;
                        }

                    }
                }
            }
            return false;
        }
        private bool _CheckLeft(CustomCTRL_PictureBox[,] _PictureBoxGrid)
        {
            if (_Current_Pos.col<=7)
            {
                for (int i = _Current_Pos.col; i<=7; i--)
                {
                    if (i>7||i<0) return false;
                    if (_PictureBoxGrid[_Current_Pos.row, i].CheckIsPiece()&&!_PictureBoxGrid[i, _Current_Pos.col].IsKing())
                    {

                        if (!(_IsTypeColorSame(_PictureBoxGrid[_Current_Pos.row, i])))
                        {
                            if (_PictureBoxGrid[_Current_Pos.row, i].IsRook()||_PictureBoxGrid[_Current_Pos.row, i].IsQueen())
                            {
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                        else
                        {
                            return false;
                        }

                    }
                }
            }
            return false;
        }
        private bool _IsVerticalKingInCheck(CustomCTRL_PictureBox[,] _PictureBoxGrid)
        {
            return (_CheckUp(_PictureBoxGrid)||_CheckDown(_PictureBoxGrid));
        }
        private bool _IsHorizontalKingInCheck(CustomCTRL_PictureBox[,] _PictureBoxGrid)
        {
            return (_CheckLeft(_PictureBoxGrid)||_CheckRight(_PictureBoxGrid));
        }

        private bool _CheckDiagonal_LiftUp(CustomCTRL_PictureBox[,] _PictureBoxGrid)
        {
            /*We take  all the corenr of Box grid 
             * first row :0 , col :0 &  row :0 , col :7
             *  Second row :7 && col :0 & row :7 && col :7

             */
            int TempRow = _Current_Pos.row;

            if (_Current_Pos.col<=7)
            {

                for (int i = _Current_Pos.col; i<=7; i--)
                {
                    if (TempRow>7||TempRow<0) return false;
                    if (i>7||i<0) return false;

                    if (_PictureBoxGrid[TempRow, i].CheckIsPiece()&&!_PictureBoxGrid[TempRow, _Current_Pos.col].IsKing())
                    {
                        if (!(_IsTypeColorSame(_PictureBoxGrid[TempRow, i])))
                        {
                            if (_PictureBoxGrid[TempRow, i].IsRook()||_PictureBoxGrid[TempRow, i].IsQueen())
                            {
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                        else
                        {
                            return false;
                        }
                    }

                    TempRow-=1;

                }
            }
            return false;
        }
        private bool _CheckDiagonal_RightUp(CustomCTRL_PictureBox[,] _PictureBoxGrid)
        {
            /*We take  all the corenr of Box grid 
             * first row :0 , col :0 &  row :0 , col :7
             *  Second row :7 && col :0 & row :7 && col :7

             */
            int TempRow = _Current_Pos.row;

            if (_Current_Pos.col<=7)
            {

                for (int i = _Current_Pos.col; i<=7; i++)
                {
                    if (TempRow>7||TempRow<0) return false;
                    if (i>7||i<0) return false;

                    if (_PictureBoxGrid[TempRow, i].CheckIsPiece()&&!_PictureBoxGrid[TempRow, _Current_Pos.col].IsKing())
                    {
                        if (!(_IsTypeColorSame(_PictureBoxGrid[TempRow, i])))
                        {
                            if (_PictureBoxGrid[TempRow, i].IsRook()||_PictureBoxGrid[TempRow, i].IsQueen())
                            {
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                        else
                        {
                            return false;
                        }
                    }

                    TempRow+=1;

                }
            }
            return false;
        }

        private bool _CheckDiagonal_RightDown(CustomCTRL_PictureBox[,] _PictureBoxGrid)
        {
            /*We take  all the corenr of Box grid 
             * first row :0 , col :0 &  row :0 , col :7
             *  Second row :7 && col :0 & row :7 && col :7

             */
            int TempRow = _Current_Pos.row;

            if (_Current_Pos.col<=7)
            {

                for (int i = _Current_Pos.col; i<=7; i++)
                {
                    if (TempRow>7||TempRow<0) return false;
                    if (i>7||i<0) return false;

                    if (_PictureBoxGrid[TempRow, i].CheckIsPiece()&&!_PictureBoxGrid[TempRow, _Current_Pos.col].IsKing())
                    {
                        if (!(_IsTypeColorSame(_PictureBoxGrid[TempRow, i])))
                        {
                            if (_PictureBoxGrid[TempRow, i].IsRook()||_PictureBoxGrid[TempRow, i].IsQueen())
                            {
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                        else
                        {
                            return false;
                        }
                    }

                    TempRow-=1;

                }
            }
            return false;
        }

        private bool _CheckDiagonal_LiftDown(CustomCTRL_PictureBox[,] _PictureBoxGrid)
        {
            /*We take  all the corenr of Box grid 
             * first row :0 , col :0 &  row :0 , col :7
             *  Second row :7 && col :0 & row :7 && col :7

             */
            int TempRow = _Current_Pos.row;

            if (_Current_Pos.col<=7)
            {

                for (int i = _Current_Pos.col; i<=7; i--)
                {
                    if (TempRow>7||TempRow<0) return false;
                    if (i>7||i<0) return false;

                    if (_PictureBoxGrid[TempRow, i].CheckIsPiece()&&!_PictureBoxGrid[TempRow, _Current_Pos.col].IsKing())
                    {
                        if (!(_IsTypeColorSame(_PictureBoxGrid[TempRow, i])))
                        {
                            if (_PictureBoxGrid[TempRow, i].IsRook()||_PictureBoxGrid[TempRow, i].IsQueen())
                            {
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                        else
                        {
                            return false;
                        }
                    }

                    TempRow+=1;

                }
            }
            return false;
        }
        private bool _IsDiagonalKingInCheck(CustomCTRL_PictureBox[,] _P)
        {
            return (_CheckDiagonal_LiftUp(_P)||_CheckDiagonal_RightUp(_P)||_CheckDiagonal_RightDown(_P)||_CheckDiagonal_LiftDown(_P));
        }

        public  bool IsKingInCheck(CustomCTRL_PictureBox[,] _P)
        {
            
            return (_IsVerticalKingInCheck(_P)||_IsHorizontalKingInCheck(_P)||_IsDiagonalKingInCheck(_P)||_IsHorizontalKnightAroundMe(_P)||_IsVerticalKnightAroundMe(_P));
        }

        public bool IsPiecesAroundMe(CustomCTRL_PictureBox[,] _PictureBoxGrid)
        {




            if (_IsNumberBetweenBoard(_Current_Pos.col+1)&&_IsNumberBetweenBoard(_Current_Pos.row+1))
            {
                if (_PictureBoxGrid[_Current_Pos.row+1, _Current_Pos.col+2].GetCheesPieceName==CustomCTRL_PictureBox.enCheesPieces.Knight)
                {
                    if (_IsNotTypeColorSame(_PictureBoxGrid[_Current_Pos.row+1, _Current_Pos.col+2]))
                    {
                        //return IsKindInCheck
                        return true;
                    }
                }
            }
            else if (_IsNumberBetweenBoard(_Current_Pos.col+2)&&_IsNumberBetweenBoard(_Current_Pos.row-1))
            {
                if (_PictureBoxGrid[_Current_Pos.row-1, _Current_Pos.col+2].GetCheesPieceName==CustomCTRL_PictureBox.enCheesPieces.Knight)
                {
                    if (_IsNotTypeColorSame(_PictureBoxGrid[_Current_Pos.row-1, _Current_Pos.col+2]))
                    {
                        //return IsKindInCheck
                        return true;
                    }
                }
            }

            if (_IsNumberBetweenBoard(_Current_Pos.col-2)&&_IsNumberBetweenBoard(_Current_Pos.row+1))
            {
                if (_PictureBoxGrid[_Current_Pos.row + 1, _Current_Pos.col-2].GetCheesPieceName==CustomCTRL_PictureBox.enCheesPieces.Knight)
                {
                    if (_IsNotTypeColorSame(_PictureBoxGrid[_Current_Pos.row + 1, _Current_Pos.col-2]))
                    {
                        //return IsKindInCheck
                        return true;
                    }
                }
            }
            else if (_IsNumberBetweenBoard(_Current_Pos.col-2)&&_IsNumberBetweenBoard(_Current_Pos.row-1))
            {
                if (_PictureBoxGrid[_Current_Pos.row -1, _Current_Pos.col-2].GetCheesPieceName==CustomCTRL_PictureBox.enCheesPieces.Knight)
                {
                    if (_IsNotTypeColorSame(_PictureBoxGrid[_Current_Pos.row - 1, _Current_Pos.col-2]))
                    {
                        //return IsKindInCheck
                        return true;
                    }
                }
            }

            return false;
        }

        

    }
}





