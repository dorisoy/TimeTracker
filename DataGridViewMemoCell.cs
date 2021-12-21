using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PersonalTimeTracker
{
    public class DataGridViewMemoColumn : DataGridViewTextBoxColumn
    {
        public DataGridViewMemoColumn()
        {
            CellTemplate = new DataGridViewMemoCell();
        }
    }

    public class MemoBox : DataGridViewTextBoxEditingControl
    {
        public override void ApplyCellStyleToEditingControl(DataGridViewCellStyle dataGridViewCellStyle)
        {
            base.ApplyCellStyleToEditingControl(dataGridViewCellStyle);
            // These override any other settings
            WordWrap = true;
            AcceptsReturn = true;
            Multiline = true;
            ScrollBars = ScrollBars.Both;
            BorderStyle = BorderStyle.FixedSingle;
        }
    }

    public class DataGridViewMemoCell : DataGridViewTextBoxCell
    {
        public override void InitializeEditingControl(int rowIndex, object initialFormattedValue, DataGridViewCellStyle dataGridViewCellStyle)
        {
            dataGridViewCellStyle.WrapMode = DataGridViewTriState.False;
            dataGridViewCellStyle.Alignment = DataGridViewContentAlignment.TopLeft;
            base.InitializeEditingControl(rowIndex, initialFormattedValue, dataGridViewCellStyle);
        }

        public override Type EditType
        {
            get { return typeof(MemoBox); }
        }

        /// <summary>
        /// Create larger text box area to edit in.
        /// </summary>
        /// <param name="cellBounds"></param>
        /// <param name="cellClip"></param>
        /// <param name="cellStyle"></param>
        /// <param name="singleVerticalBorderAdded"></param>
        /// <param name="singleHorizontalBorderAdded"></param>
        /// <param name="isFirstDisplayedColumn"></param>
        /// <param name="isFirstDisplayedRow"></param>
        /// <returns></returns>
        public override Rectangle PositionEditingPanel(Rectangle cellBounds, Rectangle cellClip, DataGridViewCellStyle cellStyle,
            bool singleVerticalBorderAdded, bool singleHorizontalBorderAdded, bool isFirstDisplayedColumn, bool isFirstDisplayedRow)
        {
            cellStyle.Alignment = DataGridViewContentAlignment.TopLeft;
            Rectangle newCellBounds = new Rectangle(cellBounds.X - 200 + cellBounds.Width, cellBounds.Y, 200, 150);
            Rectangle newCellClip = new Rectangle(cellClip.X - 200 + cellClip.Width, cellClip.Y, 200, 150);
            DataGridView grid = DataGridView;
            if (newCellBounds.Height > grid.ClientRectangle.Height)
            {
                newCellBounds.Y = grid.ClientRectangle.Top;
                newCellBounds.Height = grid.ClientRectangle.Height - 5;
                newCellClip.Y = grid.ClientRectangle.Top;
                newCellClip.Height = grid.ClientRectangle.Height - 5;
            }
            else if (newCellBounds.Bottom > grid.ClientRectangle.Bottom)
            {
                int adjAmount = grid.ClientRectangle.Bottom - newCellBounds.Bottom - 5;
                // Shift edit area up
                newCellBounds.Offset(0, adjAmount);
                newCellClip.Offset(0, adjAmount);
            }
            // Make sure top is not too high
            return base.PositionEditingPanel(newCellBounds, newCellClip, cellStyle, singleVerticalBorderAdded, singleHorizontalBorderAdded, isFirstDisplayedColumn, isFirstDisplayedRow);
        }
    }
}
