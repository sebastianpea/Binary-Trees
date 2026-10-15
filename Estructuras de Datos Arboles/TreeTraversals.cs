namespace Estructuras_de_Datos_Arboles
{
    // =========================================================================
    // TREE TRAVERSALS CLASS:
    // Responsibility: Performs depth-first tree traversals independently for both
    // standard binary trees (Node) and AVL trees (AVLNode):
    // - InOrder   (Left -> Root -> Right) -> Yields values in sorted order
    // - PreOrder  (Root -> Left -> Right)
    // - PostOrder (Left -> Right -> Root)
    // =========================================================================
    public static class TreeTraversals
    {
        // Traversals for Node (BinarySearchTree)
        public static string InOrder(Node? current)
        {
            if (current == null) return "";
            return $"{InOrder(current.Left)}{current.Id} {InOrder(current.Right)}";
        }

        public static string PreOrder(Node? current)
        {
            if (current == null) return "";
            return $"{current.Id} {PreOrder(current.Left)}{PreOrder(current.Right)}";
        }

        public static string PostOrder(Node? current)
        {
            if (current == null) return "";
            return $"{PostOrder(current.Left)}{PostOrder(current.Right)}{current.Id} ";
        }

        // Overloads for AVLNode (AVLTree)
        public static string InOrder(AVLNode? current)
        {
            if (current == null) return "";
            return $"{InOrder(current.Left)}{current.Id} {InOrder(current.Right)}";
        }

        public static string PreOrder(AVLNode? current)
        {
            if (current == null) return "";
            return $"{current.Id} {PreOrder(current.Left)}{PreOrder(current.Right)}";
        }

        public static string PostOrder(AVLNode? current)
        {
            if (current == null) return "";
            return $"{PostOrder(current.Left)}{PostOrder(current.Right)}{current.Id} ";
        }
    }
}
