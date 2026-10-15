using System;
using System.IO;

namespace Estructuras_de_Datos_Arboles
{
    // =========================================================================
    // AVL TREE: Self-balancing binary search tree.
    // Uses Root, current, and newNode. Automatically balances via rotations.
    // Includes LoadFromFile to read data and balance before displaying.
    // =========================================================================
    public class AVLTree
    {
        public AVLNode? Root; // Root reference of the tree
        public string LastCaseMessage = "";

        public AVLTree()
        {
            Root = null;
        }

        // ---------------------------------------------------------------------
        // HEIGHT AND BALANCE HELPERS
        // ---------------------------------------------------------------------
        public int GetHeight(AVLNode? current) => current?.Height ?? 0;

        public int GetBalance(AVLNode? current)
        {
            if (current == null) return 0;
            return GetHeight(current.Left) - GetHeight(current.Right);
        }

        // ---------------------------------------------------------------------
        // ROTATIONS
        // ---------------------------------------------------------------------
        private AVLNode RotateRight(AVLNode y)
        {
            AVLNode x = y.Left!;
            AVLNode t2 = x.Right!;

            x.Right = y;
            y.Left = t2;

            y.Height = Math.Max(GetHeight(y.Left), GetHeight(y.Right)) + 1;
            x.Height = Math.Max(GetHeight(x.Left), GetHeight(x.Right)) + 1;

            return x;
        }

        private AVLNode RotateLeft(AVLNode x)
        {
            AVLNode y = x.Right!;
            AVLNode t2 = y.Left!;

            y.Left = x;
            x.Right = t2;

            x.Height = Math.Max(GetHeight(x.Left), GetHeight(x.Right)) + 1;
            y.Height = Math.Max(GetHeight(y.Left), GetHeight(y.Right)) + 1;

            return y;
        }

        // ---------------------------------------------------------------------
        // ADD (INSERT WITH AUTOMATIC BALANCING)
        // ---------------------------------------------------------------------
        public void Add(int id)
        {
            Root = AddRecursive(Root, id);
        }

        private AVLNode AddRecursive(AVLNode? current, int id)
        {
            if (current == null)
            {
                LastCaseMessage = $"[AVL Insert] Created new node ({id})";
                return new AVLNode(id);
            }

            if (id < current.Id)
            {
                current.Left = AddRecursive(current.Left, id);
            }
            else if (id > current.Id)
            {
                current.Right = AddRecursive(current.Right, id);
            }
            else
            {
                LastCaseMessage = $"[AVL Duplicate] Id {id} already exists.";
                return current;
            }

            current.Height = 1 + Math.Max(GetHeight(current.Left), GetHeight(current.Right));
            int balance = GetBalance(current);

            // Left-Left -> Rotate Right
            if (balance > 1 && id < current.Left!.Id)
            {
                LastCaseMessage = $"[AVL Rotation] Left-Left resolved with RotateRight on {current.Id}";
                return RotateRight(current);
            }

            // Right-Right -> Rotate Left
            if (balance < -1 && id > current.Right!.Id)
            {
                LastCaseMessage = $"[AVL Rotation] Right-Right resolved with RotateLeft on {current.Id}";
                return RotateLeft(current);
            }

            // Left-Right -> Rotate Left then Right
            if (balance > 1 && id > current.Left!.Id)
            {
                LastCaseMessage = $"[AVL Double Rotation] Left-Right resolved on {current.Id}";
                current.Left = RotateLeft(current.Left!);
                return RotateRight(current);
            }

            // Right-Left -> Rotate Right then Left
            if (balance < -1 && id < current.Right!.Id)
            {
                LastCaseMessage = $"[AVL Double Rotation] Right-Left resolved on {current.Id}";
                current.Right = RotateRight(current.Right!);
                return RotateLeft(current);
            }

            return current;
        }

        public bool Search(int id)
        {
            AVLNode? current = Root;
            while (current != null)
            {
                if (current.Id == id) return true;
                if (id < current.Id) current = current.Left;
                else current = current.Right;
            }
            return false;
        }

        public int Count() => CountRecursive(Root);
        private int CountRecursive(AVLNode? current) => current == null ? 0 : 1 + CountRecursive(current.Left) + CountRecursive(current.Right);

        public int GetHeight() => GetHeight(Root);

        // ---------------------------------------------------------------------
        // LOAD FROM FILE AND BALANCE
        // ---------------------------------------------------------------------
        public int LoadFromFile(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"The file was not found: {filePath}");
            }

            string content = File.ReadAllText(filePath);
            char[] delimiters = new char[] { ' ', ',', ';', '\r', '\n', '\t' };
            string[] tokens = content.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);

            int loadedCount = 0;
            foreach (string token in tokens)
            {
                if (int.TryParse(token.Trim(), out int id))
                {
                    Add(id);
                    loadedCount++;
                }
            }

            return loadedCount;
        }
    }
}