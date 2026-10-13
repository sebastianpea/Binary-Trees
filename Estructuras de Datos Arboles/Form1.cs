using System;
using System.Drawing;
using System.Windows.Forms;

namespace Estructuras_de_Datos_Arboles
{
    // =========================================================================
    // FORM1 CLASS (VIEW / USER INTERFACE):
    // Responsibility: Manages user interface controls and events.
    // Delegates:
    // - Data structure logic to 'BinarySearchTree'
    // - Depth-first traversals to 'TreeTraversals'
    // - Visual rendering to 'TreeRenderer'
    // =========================================================================
    public partial class Form1 : Form
    {
        private BinarySearchTree tree = new BinarySearchTree();
        private int? highlightedNode = null; // Id of the node to highlight upon search

        // Interface controls
        private Label lblId = null!;
        private TextBox txtId = null!;
        private Button btnAdd = null!;
        private Button btnSearch = null!;
        private Button btnRemove = null!;
        private Button btnClear = null!;

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
            this.Text = "Binary Search Tree";
            this.Size = new Size(1000, 650);
            this.StartPosition = FormStartPosition.CenterScreen;

            // Top control row
            lblId = new Label { Text = "Id:", Location = new Point(20, 20), AutoSize = true };
            txtId = new TextBox { Location = new Point(50, 17), Width = 70 };
            txtId.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    BtnAdd_Click(s, e);
                    e.SuppressKeyPress = true;
                }
            };

            btnAdd = new Button { Text = "Add", Location = new Point(135, 15), Width = 75, Height = 27, UseVisualStyleBackColor = true };
            btnAdd.Click += BtnAdd_Click;

            btnSearch = new Button { Text = "Search", Location = new Point(220, 15), Width = 75, Height = 27, UseVisualStyleBackColor = true };
            btnSearch.Click += BtnSearch_Click;

            btnRemove = new Button { Text = "Remove", Location = new Point(305, 15), Width = 75, Height = 27, UseVisualStyleBackColor = true };
            btnRemove.Click += BtnRemove_Click;

            btnClear = new Button { Text = "Clear", Location = new Point(390, 15), Width = 75, Height = 27, UseVisualStyleBackColor = true };
            btnClear.Click += BtnClear_Click;

            btnInOrder = new Button { Text = "InOrder", Location = new Point(500, 15), Width = 75, Height = 27, UseVisualStyleBackColor = true };
            btnInOrder.Click += (s, e) => ShowTraversal("InOrder", TreeTraversals.InOrder(tree.Head));

            btnPreOrder = new Button { Text = "PreOrder", Location = new Point(585, 15), Width = 75, Height = 27, UseVisualStyleBackColor = true };
            btnPreOrder.Click += (s, e) => ShowTraversal("PreOrder", TreeTraversals.PreOrder(tree.Head));

            btnPostOrder = new Button { Text = "PostOrder", Location = new Point(670, 15), Width = 75, Height = 27, UseVisualStyleBackColor = true };
            btnPostOrder.Click += (s, e) => ShowTraversal("PostOrder", TreeTraversals.PostOrder(tree.Head));

            // Informational status labels
            lblStatus = new Label { Text = "Status: Tree is empty.", Location = new Point(20, 55), AutoSize = true };
            lblTraversal = new Label { Text = "Traversal: None", Location = new Point(400, 55), AutoSize = true };
            lblMetrics = new Label { Text = "Nodes: 0 | Height: 0", Location = new Point(800, 55), AutoSize = true };

            // Drawing canvas panel
            canvasPanel = new DoubleBufferedPanel
            {
                Location = new Point(20, 85),
                Size = new Size(945, 500),
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
            this.Controls.Add(btnInOrder);
            this.Controls.Add(btnPreOrder);
            this.Controls.Add(btnPostOrder);
            this.Controls.Add(lblStatus);
            this.Controls.Add(lblTraversal);
            this.Controls.Add(lblMetrics);
            this.Controls.Add(canvasPanel);
        }

        // =====================================================================
        // EVENT HANDLERS (DELEGATE TO APPROPRIATE CLASSES)
        // =====================================================================
        private void BtnAdd_Click(object? sender, EventArgs e)
        {
            if (int.TryParse(txtId.Text, out int id))
            {
                bool added = tree.Add(id);
                highlightedNode = added ? id : null;
                lblStatus.Text = "Status: " + tree.LastCaseMessage;
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
                bool found = tree.Search(id);
                highlightedNode = found ? id : null;
                lblStatus.Text = "Status: " + tree.LastCaseMessage;
                canvasPanel.Invalidate();
            }
        }

        private void BtnRemove_Click(object? sender, EventArgs e)
        {
            if (int.TryParse(txtId.Text, out int id))
            {
                tree.Remove(id);
                highlightedNode = null;
                lblStatus.Text = "Status: " + tree.LastCaseMessage;
                UpdateMetrics();
                txtId.Clear();
                canvasPanel.Invalidate();
            }
        }

        private void BtnClear_Click(object? sender, EventArgs e)
        {
            tree.Head = null;
            highlightedNode = null;
            lblStatus.Text = "Status: Tree cleared (Head = null).";
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
            lblMetrics.Text = $"Nodes: {tree.Count()} | Height: {tree.GetHeight()}";
        }

        // =====================================================================
        // RENDERING (DELEGATED TO TreeRenderer)
        // =====================================================================
        private void CanvasPanel_Paint(object? sender, PaintEventArgs e)
        {
            TreeRenderer.Draw(e.Graphics, tree.Head, highlightedNode, canvasPanel.Width, this.Font);
        }
    }
}
