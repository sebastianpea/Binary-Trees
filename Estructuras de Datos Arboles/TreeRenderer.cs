using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Estructuras_de_Datos_Arboles
{
    // =========================================================================
    // TREE RENDERER CLASS:
    // Responsibility: Exclusively handles graphical rendering of the binary tree
    // onto a GDI+ Graphics surface (lines, node circles, and centered Id labels).
    // =========================================================================
    public static class TreeRenderer
    {
        private const int NodeRadius = 18;
        private const int VerticalDistance = 60;

        public static void Draw(Graphics g, Node? head, int? highlightedNode, int canvasWidth, Font font)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;

            if (head == null)
            {
                g.DrawString("Tree is empty.", font, Brushes.Black, 20, 20);
                return;
            }

            int rootX = canvasWidth / 2;
            int rootY = 35;
            int initialOffsetX = Math.Min(canvasWidth / 4, 230);

            DrawSubtree(g, head, rootX, rootY, initialOffsetX, highlightedNode, font);
        }

        private static void DrawSubtree(Graphics g, Node current, int x, int y, int offsetX, int? highlightedNode, Font font)
        {
            // 1. Draw left branch
            if (current.Left != null)
            {
                int childX = x - offsetX;
                int childY = y + VerticalDistance;
                g.DrawLine(Pens.Black, x, y, childX, childY);
                DrawSubtree(g, current.Left, childX, childY, Math.Max(offsetX / 2, 25), highlightedNode, font);
            }

            // 2. Draw right branch
            if (current.Right != null)
            {
                int childX = x + offsetX;
                int childY = y + VerticalDistance;
                g.DrawLine(Pens.Black, x, y, childX, childY);
                DrawSubtree(g, current.Right, childX, childY, Math.Max(offsetX / 2, 25), highlightedNode, font);
            }

            // 3. Draw node circle
            Rectangle rect = new Rectangle(x - NodeRadius, y - NodeRadius, NodeRadius * 2, NodeRadius * 2);

            Brush backgroundBrush = (highlightedNode.HasValue && highlightedNode.Value == current.Id)
                ? Brushes.LightGray
                : Brushes.White;

            g.FillEllipse(backgroundBrush, rect);
            g.DrawEllipse(Pens.Black, rect);

            // 4. Draw centered Id label
            string text = current.Id.ToString();
            SizeF size = g.MeasureString(text, font);
            g.DrawString(text, font, Brushes.Black, x - (size.Width / 2), y - (size.Height / 2));
        }
    }
}
