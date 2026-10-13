using System;

namespace Estructuras_de_Datos_Arboles
{
    // =========================================================================
    // BINARY SEARCH TREE (BST):
    // Hierarchical, non-linear data structure where:
    // - Smaller values are placed in the left subtree (current.Left)
    // - Larger values are placed in the right subtree (current.Right)
    // =========================================================================
    public class BinarySearchTree
    {
        public Node? Head; // Root reference / entry point of the tree
        public string LastCaseMessage = ""; // Stores the specific executed case description

        public BinarySearchTree()
        {
            Head = null; // Initially empty tree
        }

        // =====================================================================
        // METHOD: ADD (INSERTION)
        // =====================================================================
        public bool Add(int id)
        {
            Node newNode = new Node(id);

            // CASE 1: Tree is empty. Head points directly to newNode.
            if (Head == null)
            {
                Head = newNode;
                LastCaseMessage = $"[Case 1: Empty Tree] Assigned Head = newNode ({id})";
                return true;
            }

            // CASE 2 & 3: Traverse using 'current' to find an empty spot
            Node current = Head;

            while (true)
            {
                // CASE 2: New value is SMALLER -> go left
                if (newNode.Id < current.Id)
                {
                    if (current.Left == null)
                    {
                        current.Left = newNode; // Link as left child
                        LastCaseMessage = $"[Case 2: Less than {current.Id}] Inserted {id} at current.Left";
                        return true;
                    }
                    current = current.Left; // Move pointer to the left subtree
                }
                // CASE 3: New value is LARGER -> go right
                else if (newNode.Id > current.Id)
                {
                    if (current.Right == null)
                    {
                        current.Right = newNode; // Link as right child
                        LastCaseMessage = $"[Case 3: Greater than {current.Id}] Inserted {id} at current.Right";
                        return true;
                    }
                    current = current.Right; // Move pointer to the right subtree
                }
                // CASE 4: Duplicate key -> do not insert
                else
                {
                    LastCaseMessage = $"[Case 4: Duplicate Key] Id {id} already exists in the tree.";
                    return false;
                }
            }
        }

        // =====================================================================
        // METHOD: SEARCH (VERIFY EXISTENCE)
        // =====================================================================
        public bool Search(int id)
        {
            // CASE 1: Tree is empty
            if (Head == null)
            {
                LastCaseMessage = "[Case 1: Search] Tree is empty (Head == null).";
                return false;
            }

            Node? current = Head;

            while (current != null)
            {
                // CASE 2: Value found
                if (current.Id == id)
                {
                    LastCaseMessage = $"[Case 2: Search] Found! current.Id == {id}";
                    return true;
                }

                // CASE 3: Target value is smaller -> traverse left
                if (id < current.Id)
                {
                    current = current.Left;
                }
                // CASE 4: Target value is larger -> traverse right
                else
                {
                    current = current.Right;
                }
            }

            // CASE 5: Reached null -> target does not exist
            LastCaseMessage = $"[Case 5: Search] Id {id} not found in the tree.";
            return false;
        }

        // =====================================================================
        // METHOD: REMOVE (DELETION COVERING THE 3 CLASSICAL CASES)
        // =====================================================================
        public bool Remove(int id)
        {
            if (!Search(id))
            {
                LastCaseMessage = $"Cannot remove: Id {id} does not exist.";
                return false;
            }

            Head = RemoveRecursive(Head, id);
            return true;
        }

        private Node? RemoveRecursive(Node? current, int id)
        {
            if (current == null) return null;

            // Search phase
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
                // Node found! Evaluate the 3 deletion cases:

                // CASE 1: Leaf node (no children)
                if (current.Left == null && current.Right == null)
                {
                    LastCaseMessage = $"[Case 1: Delete Leaf] Removed leaf node ({id})";
                    return null; // Unlink node by returning null to parent
                }

                // CASE 2: Node with ONE child
                if (current.Left == null)
                {
                    LastCaseMessage = $"[Case 2: Delete with 1 Child] Node ({id}) replaced by right child ({current.Right!.Id})";
                    return current.Right; // Right child moves up
                }
                if (current.Right == null)
                {
                    LastCaseMessage = $"[Case 2: Delete with 1 Child] Node ({id}) replaced by left child ({current.Left!.Id})";
                    return current.Left; // Left child moves up
                }

                // CASE 3: Node with TWO children
                // Strategy: Find the Inorder Successor (smallest value in the right subtree)
                Node successor = current.Right!;
                while (successor.Left != null)
                {
                    successor = successor.Left;
                }

                LastCaseMessage = $"[Case 3: Delete with 2 Children] Node ({id}) replaced by inorder successor ({successor.Id})";

                // Copy successor value to current node
                current.Id = successor.Id;

                // Recursively delete the original successor node from right subtree
                current.Right = RemoveRecursive(current.Right, successor.Id);
            }

            return current;
        }

        // =====================================================================
        // METRICS
        // =====================================================================
        public int Count()
        {
            return CountRecursive(Head);
        }

        private int CountRecursive(Node? current)
        {
            if (current == null) return 0;
            return 1 + CountRecursive(current.Left) + CountRecursive(current.Right);
        }

        public int GetHeight()
        {
            return HeightRecursive(Head);
        }

        private int HeightRecursive(Node? current)
        {
            if (current == null) return 0;
            int leftHeight = HeightRecursive(current.Left);
            int rightHeight = HeightRecursive(current.Right);
            return Math.Max(leftHeight, rightHeight) + 1;
        }
    }
}
