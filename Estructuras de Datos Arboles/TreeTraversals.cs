namespace Estructuras_de_Datos_Arboles
{
    // =========================================================================
    // TREE TRAVERSALS CLASS:
    // Responsibility: Performs depth-first tree traversals independently:
    // - InOrder   (Left -> Root -> Right) -> Yields values in sorted ascending order
    // - PreOrder  (Root -> Left -> Right)
    // - PostOrder (Left -> Right -> Root)
    // =========================================================================
    public static class TreeTraversals
    {
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
    }
}
