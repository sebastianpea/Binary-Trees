using System;

namespace Estructuras_de_Datos_Arboles
{
    // =========================================================================
    // ÁRBOL BINARIO DE BÚSQUEDA (BST / ABB):
    // Estructura jerárquica no lineal donde:
    // - Los valores menores van a la izquierda (current.Left)
    // - Los valores mayores van a la derecha (current.Right)
    // =========================================================================
    public class BinarySearchTree
    {
        public Node? Root; // Puntero inicial / Raíz del árbol
        public string UltimoMensajeCaso = ""; // Almacena el caso ejecutado para la presentación

        public BinarySearchTree()
        {
            Root = null; // Árbol inicialmente vacío
        }

        // =====================================================================
        // MÉTODO ADD (INSERTAR)
        // =====================================================================
        public bool Add(int id)
        {
            Node newNode = new Node(id);

            // CASO 1: Árbol vacío. Head apunta a newNode.
            if (Root == null)
            {
                Root = newNode;
                UltimoMensajeCaso = $"[Caso 1: Árbol Vacío] Se asignó Head = newNode ({id})";
                return true;
            }

            // CASO 2 y 3: Recorrido con 'current' buscando posición libre
            Node current = Root;

            while (true)
            {
                // CASO 2: El nuevo dato es MENOR -> ir a la izquierda
                if (newNode.Id < current.Id)
                {
                    if (current.Left == null)
                    {
                        current.Left = newNode; // Se enlaza como hijo izquierdo
                        UltimoMensajeCaso = $"[Caso 2: Menor que {current.Id}] Se insertó {id} en current.Left";
                        return true;
                    }
                    current = current.Left; // Avanzar el puntero current hacia la izquierda
                }
                // CASO 3: El nuevo dato es MAYOR -> ir a la derecha
                else if (newNode.Id > current.Id)
                {
                    if (current.Right == null)
                    {
                        current.Right = newNode; // Se enlaza como hijo derecho
                        UltimoMensajeCaso = $"[Caso 3: Mayor que {current.Id}] Se insertó {id} en current.Right";
                        return true;
                    }
                    current = current.Right; // Avanzar el puntero current hacia la derecha
                }
                // CASO 4: Clave duplicada -> no se inserta
                else
                {
                    UltimoMensajeCaso = $"[Caso 4: Clave Duplicada] El Id {id} ya existe en el árbol.";
                    return false;
                }
            }
        }

        // =====================================================================
        // MÉTODO SEARCH (BUSCAR / VERIFICAR SI EXISTE)
        // =====================================================================
        public bool Search(int id)
        {
            // CASO 1: Árbol vacío
            if (Root == null)
            {
                UltimoMensajeCaso = "[Caso 1: Búsqueda] Árbol vacío (Head == null).";
                return false;
            }

            Node? current = Root;

            while (current != null)
            {
                // CASO 2: Dato encontrado
                if (current.Id == id)
                {
                    UltimoMensajeCaso = $"[Caso 2: Búsqueda] ¡Encontrado! current.Id == {id}";
                    return true;
                }

                // CASO 3: El buscado es menor -> ir a la izquierda
                if (id < current.Id)
                {
                    current = current.Left;
                }
                // CASO 4: El buscado es mayor -> ir a la derecha
                else
                {
                    current = current.Right;
                }
            }

            // CASO 5: Llegamos a null, no existe
            UltimoMensajeCaso = $"[Caso 5: Búsqueda] El Id {id} no existe en el árbol.";
            return false;
        }

        // =====================================================================
        // MÉTODO REMOVE (ELIMINAR) - CUBRE LOS 3 CASOS CLÁSICOS
        // =====================================================================
        public bool Remove(int id)
        {
            if (!Search(id))
            {
                UltimoMensajeCaso = $"No se puede eliminar: El Id {id} no existe.";
                return false;
            }

            Root = RemoveRecursive(Root, id);
            return true;
        }

        private Node? RemoveRecursive(Node? current, int id)
        {
            if (current == null) return null;

            // Fase de búsqueda
            if (id < current.Id)
            {
                current.Left = RemoveRecursive(current.Left, id);
            }
            else if (id > current.Id)
            {
                current.Right = RemoveRecursive(current.Right, id);
            }
            else
            {
                // ¡Nodo encontrado! Se analizan los 3 casos:

                // CASO 1: Nodo Hoja (0 hijos)
                if (current.Left == null && current.Right == null)
                {
                    UltimoMensajeCaso = $"[Caso 1: Eliminar Hoja] Se eliminó el nodo hoja ({id})";
                    return null; // Se desliga devolviendo null
                }

                // CASO 2: Nodo con UN solo hijo
                if (current.Left == null)
                {
                    UltimoMensajeCaso = $"[Caso 2: Eliminar con 1 Hijo] El nodo ({id}) fue reemplazado por su hijo derecho ({current.Right!.Id})";
                    return current.Right; // El hijo derecho sube
                }
                if (current.Right == null)
                {
                    UltimoMensajeCaso = $"[Caso 2: Eliminar con 1 Hijo] El nodo ({id}) fue reemplazado por su hijo izquierdo ({current.Left!.Id})";
                    return current.Left; // El hijo izquierdo sube
                }

                // CASO 3: Nodo con DOS hijos
                // Estrategia: Buscar el Sucesor InOrden (el menor valor del subárbol derecho)
                Node sucesor = current.Right!;
                while (sucesor.Left != null)
                {
                    sucesor = sucesor.Left;
                }

                UltimoMensajeCaso = $"[Caso 3: Eliminar con 2 Hijos] El nodo ({id}) fue reemplazado por su sucesor inorden ({sucesor.Id})";

                // Copiar el valor del sucesor al nodo actual
                current.Id = sucesor.Id;

                // Eliminar el nodo sucesor original de la derecha
                current.Right = RemoveRecursive(current.Right, sucesor.Id);
            }

            return current;
        }

        // =====================================================================
        // RECORRIDOS (InOrden, PreOrden, PostOrden)
        // =====================================================================
        public string GetInOrder()
        {
            return InOrderRecursive(Root).Trim();
        }

        private string InOrderRecursive(Node? current)
        {
            if (current == null) return "";
            return $"{InOrderRecursive(current.Left)}{current.Id} {InOrderRecursive(current.Right)}";
        }

        public string GetPreOrder()
        {
            return PreOrderRecursive(Root).Trim();
        }

        private string PreOrderRecursive(Node? current)
        {
            if (current == null) return "";
            return $"{current.Id} {PreOrderRecursive(current.Left)}{PreOrderRecursive(current.Right)}";
        }

        public string GetPostOrder()
        {
            return PostOrderRecursive(Root).Trim();
        }

        private string PostOrderRecursive(Node? current)
        {
            if (current == null) return "";
            return $"{PostOrderRecursive(current.Left)}{PostOrderRecursive(current.Right)}{current.Id} ";
        }

        // =====================================================================
        // MÉTRICAS DEL ÁRBOL
        // =====================================================================
        public int Count()
        {
            return CountRecursive(Root);
        }

        private int CountRecursive(Node? current)
        {
            if (current == null) return 0;
            return 1 + CountRecursive(current.Left) + CountRecursive(current.Right);
        }

        public int GetHeight()
        {
            return HeightRecursive(Root);
        }

        private int HeightRecursive(Node? current)
        {
            if (current == null) return 0;
            int hLeft = HeightRecursive(current.Left);
            int hRight = HeightRecursive(current.Right);
            return Math.Max(hLeft, hRight) + 1;
        }
    }
}
