using System;
using System.Drawing;
using System.Windows.Forms;

namespace Estructuras_de_Datos_Arboles
{
    // =========================================================================
    // FORM1 CLASS (USER INTERFACE):
    // Supports both standard Binary Search Tree (BST) and self-balancing AVL Tree.
    // Includes file loading with automatic AVL balancing.
    // =========================================================================
    public partial class Form1 : Form
    {
        private BinarySearchTree tree = new BinarySearchTree();
        private AVLTree avlTree = new AVLTree();
        private bool isAVLMode = false; // Toggles between BST and AVL display
        private int? highlightedNode = null; // Stored Id to highlight on search

        // Controls
        private Label lblId = null!;
        private TextBox txtId = null!;
        private Button btnAdd = null!;
        private Button btnSearch = null!;
        private Button btnRemove = null!;
        private Button btnClear = null!;
        private Button btnLoadFile = null!;
        private Button btnToggleMode = null!;

        private Button btnInOrder = null!;
        private Button btnPreOrder = null!;
        private Button btnPostOrder = null!;

        private Label lblStatus = null!;
        private Label lblTraversal = null!;
        private Label lblMetrics = null!;

        private DoubleBufferedPanel canvasPanel = null!;

        public Form1()
        {
            InitializeComponent();
            SetupInterface();
        }

        private void SetupInterface()
        {
            this.Text = "Binary Search Tree & AVL Tree Simulator";
            this.Size = new Size(1100, 680);
            this.StartPosition = FormStartPosition.CenterScreen;

            // Top control row
            lblId = new Label { Text = "Id:", Location = new Point(15, 20), AutoSize = true };
            txtId = new TextBox { Location = new Point(42, 17), Width = 65 };
            txtId.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    BtnAdd_Click(s, e);
                    e.SuppressKeyPress = true;
                }
            };

            btnAdd = new Button { Text = "Add", Location = new Point(115, 15), Width = 65, Height = 27, UseVisualStyleBackColor = true };
            btnAdd.Click += BtnAdd_Click;

            btnSearch = new Button { Text = "Search", Location = new Point(185, 15), Width = 65, Height = 27, UseVisualStyleBackColor = true };
            btnSearch.Click += BtnSearch_Click;

            btnRemove = new Button { Text = "Remove", Location = new Point(255, 15), Width = 65, Height = 27, UseVisualStyleBackColor = true };
            btnRemove.Click += BtnRemove_Click;

            btnClear = new Button { Text = "Clear", Location = new Point(325, 15), Width = 65, Height = 27, UseVisualStyleBackColor = true };
            btnClear.Click += BtnClear_Click;

            btnLoadFile = new Button { Text = "Load File (AVL)", Location = new Point(400, 15), Width = 110, Height = 27, UseVisualStyleBackColor = true };
            btnLoadFile.Click += BtnLoadFile_Click;

            btnToggleMode = new Button { Text = "Mode: BST", Location = new Point(520, 15), Width = 95, Height = 27, UseVisualStyleBackColor = true };
            btnToggleMode.Click += BtnToggleMode_Click;

            btnInOrder = new Button { Text = "InOrder", Location = new Point(630, 15), Width = 70, Height = 27, UseVisualStyleBackColor = true };
            btnInOrder.Click += (s, e) => ShowTraversal("InOrder", isAVLMode ? TreeTraversals.InOrder(avlTree.Root) : TreeTraversals.InOrder(tree.Root));

            btnPreOrder = new Button { Text = "PreOrder", Location = new Point(705, 15), Width = 70, Height = 27, UseVisualStyleBackColor = true };
            btnPreOrder.Click += (s, e) => ShowTraversal("PreOrder", isAVLMode ? TreeTraversals.PreOrder(avlTree.Root) : TreeTraversals.PreOrder(tree.Root));

            btnPostOrder = new Button { Text = "PostOrder", Location = new Point(780, 15), Width = 70, Height = 27, UseVisualStyleBackColor = true };
            btnPostOrder.Click += (s, e) => ShowTraversal("PostOrder", isAVLMode ? TreeTraversals.PostOrder(avlTree.Root) : TreeTraversals.PostOrder(tree.Root));

            // Informational status labels
            lblStatus = new Label { Text = "Status: BST initialized. Tree is empty (Root == null).", Location = new Point(15, 52), AutoSize = true };
            lblTraversal = new Label { Text = "Traversal: None", Location = new Point(450, 52), AutoSize = true };
            lblMetrics = new Label { Text = "Nodes: 0 | Height: 0", Location = new Point(850, 52), AutoSize = true };

            // Canvas drawing panel
            canvasPanel = new DoubleBufferedPanel
            {
                Location = new Point(15, 85),
                Size = new Size(1055, 540),
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right,
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };
            canvasPanel.Paint += CanvasPanel_Paint;

            this.Controls.Add(lblId);
            this.Controls.Add(txtId);
            this.Controls.Add(btnAdd);
            this.Controls.Add(btnSearch);
            this.Controls.Add(btnRemove);
            this.Controls.Add(btnClear);
            this.Controls.Add(btnLoadFile);
            this.Controls.Add(btnToggleMode);
            this.Controls.Add(btnInOrder);
            this.Controls.Add(btnPreOrder);
            this.Controls.Add(btnPostOrder);
            this.Controls.Add(lblStatus);
            this.Controls.Add(lblTraversal);
            this.Controls.Add(lblMetrics);
            this.Controls.Add(canvasPanel);
        }

        // =====================================================================
        // EVENT HANDLERS
        // =====================================================================
        private void BtnAdd_Click(object? sender, EventArgs e)
        {
            if (int.TryParse(txtId.Text, out int id))
            {
                if (isAVLMode)
                {
                    avlTree.Add(id);
                    highlightedNode = id;
                    lblStatus.Text = $"Status: [AVL] {avlTree.LastCaseMessage}";
                }
                else
                {
                    bool added = tree.Add(id);
                    highlightedNode = added ? id : null;
                    lblStatus.Text = $"Status: [BST] {tree.LastCaseMessage}";
                }

                UpdateMetrics();
                txtId.Clear();
                txtId.Focus();
                canvasPanel.Invalidate();
            }
        }

        private void BtnSearch_Click(object? sender, EventArgs e)
        {
            if (int.TryParse(txtId.Text, out int id))
            {
                bool found = isAVLMode ? avlTree.Search(id) : tree.Search(id);
                highlightedNode = found ? id : null;

                if (isAVLMode)
                {
                    lblStatus.Text = found ? $"Status: [AVL] Found! Node Id: {id}" : $"Status: [AVL] Node Id: {id} not found.";
                }
                else
                {
                    lblStatus.Text = "Status: [BST] " + tree.LastCaseMessage;
                }

                canvasPanel.Invalidate();
            }
        }

        private void BtnRemove_Click(object? sender, EventArgs e)
        {
            if (!isAVLMode)
            {
                if (int.TryParse(txtId.Text, out int id))
                {
                    tree.Remove(id);
                    highlightedNode = null;
                    lblStatus.Text = "Status: [BST] " + tree.LastCaseMessage;
                    UpdateMetrics();
                    txtId.Clear();
                    canvasPanel.Invalidate();
                }
            }
            else
            {
                MessageBox.Show("Deletion is implemented for standard Binary Search Tree mode.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void BtnClear_Click(object? sender, EventArgs e)
        {
            if (isAVLMode)
            {
                avlTree.Root = null;
                lblStatus.Text = "Status: AVL Tree cleared (Root = null).";
            }
            else
            {
                tree.Root = null;
                lblStatus.Text = "Status: BST cleared (Root = null).";
            }

            highlightedNode = null;
            lblTraversal.Text = "Traversal: None";
            UpdateMetrics();
            canvasPanel.Invalidate();
        }

        private void BtnLoadFile_Click(object? sender, EventArgs e)
        {
            using OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            openFileDialog.Title = "Select Text File with Data";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    avlTree.Root = null;
                    int count = avlTree.LoadFromFile(openFileDialog.FileName);
                    isAVLMode = true;
                    btnToggleMode.Text = "Mode: AVL";
                    lblStatus.Text = $"Status: Loaded and balanced {count} numbers from file using AVL.";
                    highlightedNode = null;
                    UpdateMetrics();
                    canvasPanel.Invalidate();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error reading file: {ex.Message}", "File Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void BtnToggleMode_Click(object? sender, EventArgs e)
        {
            isAVLMode = !isAVLMode;
            btnToggleMode.Text = isAVLMode ? "Mode: AVL" : "Mode: BST";
            lblStatus.Text = $"Status: Switched to {(isAVLMode ? "AVL Tree (Balanced)" : "Binary Search Tree (BST)")}.";
            highlightedNode = null;
            lblTraversal.Text = "Traversal: None";
            UpdateMetrics();
            canvasPanel.Invalidate();
        }

        private void ShowTraversal(string type, string result)
        {
            lblTraversal.Text = $"Traversal {type}: [ {(string.IsNullOrWhiteSpace(result) ? "Empty" : result.Trim())} ]";
        }

        private void UpdateMetrics()
        {
            int count = isAVLMode ? avlTree.Count() : tree.Count();
            int height = isAVLMode ? avlTree.GetHeight() : tree.GetHeight();
            lblMetrics.Text = $"Nodes: {count} | Height: {height}";
        }

        // =====================================================================
        // RENDERING
        // =====================================================================
        private void CanvasPanel_Paint(object? sender, PaintEventArgs e)
        {
            if (isAVLMode)
            {
                TreeRenderer.Draw(e.Graphics, avlTree.Root, highlightedNode, canvasPanel.Width, this.Font);
            }
            else
            {
                TreeRenderer.Draw(e.Graphics, tree.Root, highlightedNode, canvasPanel.Width, this.Font);
            }
        }
    }
}
