namespace Estructuras_de_Datos_Arboles
{
    // =========================================================================
    // BINARY NODE: Fundamental unit of storage for binary trees.
    // Unlike a singly linked or circular list (which only has Next),
    // a tree node branches in two directions: Left and Right.
    // =========================================================================
    public class Node
    {
        public int Id;          // Stored data / numerical identifier
        public Node? Left;      // Pointer/reference to left child (smaller values)
        public Node? Right;     // Pointer/reference to right child (larger values)

        public Node(int id)
        {
            Id = id;
            Left = null;
            Right = null;
        }
    }
}
