namespace Estructuras_de_Datos_Arboles
{
    // =========================================================================
    // NODO BINARIO: Unidad básica de almacenamiento para árboles binarios.
    // A diferencia de la lista circular (que solo tiene Next), aquí bifurca
    // en dos caminos: Left (hijo izquierdo) y Right (hijo derecho).
    // =========================================================================
    public class Node
    {
        public int Id;          // Dato o identificador numérico
        public Node? Left;      // Puntero/referencia al hijo izquierdo (menores)
        public Node? Right;     // Puntero/referencia al hijo derecho (mayores)

        public Node(int id)
        {
            Id = id;
            Left = null;
            Right = null;
        }
    }
}
