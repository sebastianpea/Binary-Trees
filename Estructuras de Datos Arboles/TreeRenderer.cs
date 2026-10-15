using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Estructuras_de_Datos_Arboles
{
    // =========================================================================
    // TREE RENDERER CLASS:
    // Responsibility: Exclusively handles graphical rendering of the binary tree
    // onto a GDI+ Graphics surface. Supports both Node and AVLNode.
    // =========================================================================
    public static class TreeRenderer
    {
        private const int NodeRadius = 18;
        private const int VerticalDistance = 60;

        // ---------------------------------------------------------------------
        // RENDERING FOR STANDARD BINARY SEARCH TREE (Node)
        // ---------------------------------------------------------------------
        public static void Draw(Graphics g, Node? root, int? highlightedNode, int canvasWidth, Font font)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;

            if (root == null)
            {
                g.DrawString("Tree is empty.", font, Brushes.Black, 20, 20);
                return;
            }

            int rootX = canvasWidth / 2;
            int rootY = 35;
            int initialOffsetX = Math.Min(canvasWidth / 4, 230);

            DrawSubtree(g, root, rootX, rootY, initialOffsetX, highlightedNode, font);
        }

        private static void DrawSubtree(Graphics g, Node current, int x, int y, int offsetX, int? highlightedNode, Font font)
        {
            if (current.Left != null)
            {
                int childX = x - offsetX;
                int childY = y + VerticalDistance;
                g.DrawLine(Pens.Black, x, y, childX, childY);
                DrawSubtree(g, current.Left, childX, childY, Math.Max(offsetX / 2, 25), highlightedNode, font);
            }

            if (current.Right != null)
            {
                int childX = x + offsetX;
                int childY = y + VerticalDistance;
                g.DrawLine(Pens.Black, x, y, childX, childY);
                DrawSubtree(g, current.Right, childX, childY, Math.Max(offsetX / 2, 25), highlightedNode, font);
            }

            Rectangle rect = new Rectangle(x - NodeRadius, y - NodeRadius, NodeRadius * 2, NodeRadius * 2);
            Brush backgroundBrush = (highlightedNode.HasValue && highlightedNode.Value == current.Id)
                ? Brushes.LightGray
                : Brushes.White;

            g.FillEllipse(backgroundBrush, rect);
            g.DrawEllipse(Pens.Black, rect);

            string text = current.Id.ToString();
            SizeF size = g.MeasureString(text, font);
            g.DrawString(text, font, Brushes.Black, x - (size.Width / 2), y - (size.Height / 2));
        }

        // ---------------------------------------------------------------------
        // RENDERING FOR AVL TREE (AVLNode)
        // ---------------------------------------------------------------------
        public static void Draw(Graphics g, AVLNode? root, int? highlightedNode, int canvasWidth, Font font)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;

            if (root == null)
            {
                g.DrawString("Tree is empty.", font, Brushes.Black, 20, 20);
                return;
            }

            int rootX = canvasWidth / 2;
            int rootY = 35;
            int initialOffsetX = Math.Min(canvasWidth / 4, 230);

            DrawSubtreeAVL(g, root, rootX, rootY, initialOffsetX, highlightedNode, font);
        }

        private static void DrawSubtreeAVL(Graphics g, AVLNode current, int x, int y, int offsetX, int? highlightedNode, Font font)
        {
            if (current.Left != null)
            {
                int childX = x - offsetX;
                int childY = y + VerticalDistance;
                g.DrawLine(Pens.Black, x, y, childX, childY);
                DrawSubtreeAVL(g, current.Left, childX, childY, Math.Max(offsetX / 2, 25), highlightedNode, font);
            }

            if (current.Right != null)
            {
                int childX = x + offsetX;
                int childY = y + VerticalDistance;
                g.DrawLine(Pens.Black, x, y, childX, childY);
                DrawSubtreeAVL(g, current.Right, childX, childY, Math.Max(offsetX / 2, 25), highlightedNode, font);
            }

            Rectangle rect = new Rectangle(x - NodeRadius, y - NodeRadius, NodeRadius * 2, NodeRadius * 2);
            Brush backgroundBrush = (highlightedNode.HasValue && highlightedNode.Value == current.Id)
                ? Brushes.LightGray
                : Brushes.White;

            g.FillEllipse(backgroundBrush, rect);
            g.DrawEllipse(Pens.Black, rect);

            string text = current.Id.ToString();
            SizeF size = g.MeasureString(text, font);
            g.DrawString(text, font, Brushes.Black, x - (size.Width / 2), y - (size.Height / 2));
        }
    }
}
