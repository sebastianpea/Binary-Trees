namespace Estructuras_de_Datos_Arboles
{
    // =========================================================================
    // AVL NODE: Basic unit for height-balanced binary trees.
    // In addition to Left and Right, it maintains the Height of the node.
    // =========================================================================
    public class AVLNode
    {
        public int Id;          // Stored value
        public int Height;      // Node height used to calculate balance factors
        public AVLNode? Left;   // Pointer to left child
        public AVLNode? Right;  // Pointer to right child

        public AVLNode(int id)
        {
            Id = id;
            Height = 1;         // Every new node starts with height 1
            Left = null;
            Right = null;
        }
    }
}