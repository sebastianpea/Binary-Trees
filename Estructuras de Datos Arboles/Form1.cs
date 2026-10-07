using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Estructuras_de_Datos_Arboles
{
    public partial class Form1 : Form
    {
        private BinarySearchTree arbol = new BinarySearchTree();
        private int? nodoResaltado = null; // Id del nodo encontrado al buscar

        // Controles estándar de Windows Forms (sin estilos personalizados)
        private Label lblId = null!;
        private TextBox txtId = null!;
        private Button btnAdd = null!;
        private Button btnSearch = null!;
        private Button btnRemove = null!;
        private Button btnClear = null!;

        private Button btnInOrder = null!;
        private Button btnPreOrder = null!;
        private Button btnPostOrder = null!;

        private Label lblCaso = null!;
        private Label lblRecorrido = null!;
        private Label lblInfo = null!;

        private DoubleBufferedPanel panelLienzo = null!;

        public Form1()
        {
            InitializeComponent();
            ConfigurarInterfazSimple();
        }

        private void ConfigurarInterfazSimple()
        {
            // Propiedades nativas del formulario
            this.Text = "Árbol Binario de Búsqueda";
            this.Size = new Size(1000, 650);
            this.StartPosition = FormStartPosition.CenterScreen;

            // Fila superior de controles nativos
            lblId = new Label
            {
                Text = "Id:",
                Location = new Point(20, 20),
                AutoSize = true
            };

            txtId = new TextBox
            {
                Location = new Point(50, 17),
                Width = 70
            };
            txtId.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    BtnAdd_Click(s, e);
                    e.SuppressKeyPress = true;
                }
            };

            // Botones nativos estándar (estilo clásico de Windows)
            btnAdd = new Button { Text = "Agregar", Location = new Point(135, 15), Width = 75, Height = 27, UseVisualStyleBackColor = true };
            btnAdd.Click += BtnAdd_Click;

            btnSearch = new Button { Text = "Buscar", Location = new Point(220, 15), Width = 75, Height = 27, UseVisualStyleBackColor = true };
            btnSearch.Click += BtnSearch_Click;

            btnRemove = new Button { Text = "Eliminar", Location = new Point(305, 15), Width = 75, Height = 27, UseVisualStyleBackColor = true };
            btnRemove.Click += BtnRemove_Click;

            btnClear = new Button { Text = "Limpiar", Location = new Point(390, 15), Width = 75, Height = 27, UseVisualStyleBackColor = true };
            btnClear.Click += BtnClear_Click;

            btnInOrder = new Button { Text = "InOrden", Location = new Point(500, 15), Width = 75, Height = 27, UseVisualStyleBackColor = true };
            btnInOrder.Click += (s, e) => MostrarRecorrido("InOrden", arbol.GetInOrder());

            btnPreOrder = new Button { Text = "PreOrden", Location = new Point(585, 15), Width = 75, Height = 27, UseVisualStyleBackColor = true };
            btnPreOrder.Click += (s, e) => MostrarRecorrido("PreOrden", arbol.GetPreOrder());

            btnPostOrder = new Button { Text = "PostOrden", Location = new Point(670, 15), Width = 75, Height = 27, UseVisualStyleBackColor = true };
            btnPostOrder.Click += (s, e) => MostrarRecorrido("PostOrden", arbol.GetPostOrder());

            // Etiquetas de información y casos
            lblCaso = new Label
            {
                Text = "Estado: Árbol vacío.",
                Location = new Point(20, 55),
                AutoSize = true
            };

            lblRecorrido = new Label
            {
                Text = "Recorrido: Ninguno",
                Location = new Point(400, 55),
                AutoSize = true
            };

            lblInfo = new Label
            {
                Text = "Nodos: 0 | Altura: 0",
                Location = new Point(800, 55),
                AutoSize = true
            };

            // Área de dibujo (fondo blanco estándar con borde simple)
            panelLienzo = new DoubleBufferedPanel
            {
                Location = new Point(20, 85),
                Size = new Size(945, 500),
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right,
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };
            panelLienzo.Paint += PanelLienzo_Paint;

            // Agregar los controles a la ventana
            this.Controls.Add(lblId);
            this.Controls.Add(txtId);
            this.Controls.Add(btnAdd);
            this.Controls.Add(btnSearch);
            this.Controls.Add(btnRemove);
            this.Controls.Add(btnClear);
            this.Controls.Add(btnInOrder);
            this.Controls.Add(btnPreOrder);
            this.Controls.Add(btnPostOrder);
            this.Controls.Add(lblCaso);
            this.Controls.Add(lblRecorrido);
            this.Controls.Add(lblInfo);
            this.Controls.Add(panelLienzo);
        }

        // =====================================================================
        // EVENTOS DE BOTONES
        // =====================================================================
        private void BtnAdd_Click(object? sender, EventArgs e)
        {
            if (int.TryParse(txtId.Text, out int id))
            {
                bool agregado = arbol.Add(id);
                nodoResaltado = agregado ? id : null;
                lblCaso.Text = "Estado: " + arbol.UltimoMensajeCaso;
                ActualizarInfo();
                txtId.Clear();
                txtId.Focus();
                panelLienzo.Invalidate();
            }
        }

        private void BtnSearch_Click(object? sender, EventArgs e)
        {
            if (int.TryParse(txtId.Text, out int id))
            {
                bool encontrado = arbol.Search(id);
                nodoResaltado = encontrado ? id : null;
                lblCaso.Text = "Estado: " + arbol.UltimoMensajeCaso;
                panelLienzo.Invalidate();
            }
        }

        private void BtnRemove_Click(object? sender, EventArgs e)
        {
            if (int.TryParse(txtId.Text, out int id))
            {
                arbol.Remove(id);
                nodoResaltado = null;
                lblCaso.Text = "Estado: " + arbol.UltimoMensajeCaso;
                ActualizarInfo();
                txtId.Clear();
                panelLienzo.Invalidate();
            }
        }

        private void BtnClear_Click(object? sender, EventArgs e)
        {
            arbol.Head = null;
            nodoResaltado = null;
            lblCaso.Text = "Estado: Árbol vaciado.";
            lblRecorrido.Text = "Recorrido: Ninguno";
            ActualizarInfo();
            panelLienzo.Invalidate();
        }

        private void MostrarRecorrido(string tipo, string resultado)
        {
            lblRecorrido.Text = $"Recorrido {tipo}: [ {resultado} ]";
        }

        private void ActualizarInfo()
        {
            lblInfo.Text = $"Nodos: {arbol.Count()} | Altura: {arbol.GetHeight()}";
        }

        // =====================================================================
        // DIBUJO SIMPLE DEL ÁRBOL (Líneas negras y círculos blancos estándar)
        // =====================================================================
        private void PanelLienzo_Paint(object? sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            if (arbol.Head == null)
            {
                g.DrawString("Árbol vacío.", this.Font, Brushes.Black, 20, 20);
                return;
            }

            int rootX = panelLienzo.Width / 2;
            int rootY = 35;
            int separacionX = Math.Min(panelLienzo.Width / 4, 230);

            DibujarSubarbol(g, arbol.Head, rootX, rootY, separacionX);
        }

        private void DibujarSubarbol(Graphics g, Node current, int x, int y, int offsetX)
        {
            int radio = 18;
            int offsetY = 60;

            // Dibujar rama izquierda
            if (current.Left != null)
            {
                int hijoX = x - offsetX;
                int hijoY = y + offsetY;
                g.DrawLine(Pens.Black, x, y, hijoX, hijoY);
                DibujarSubarbol(g, current.Left, hijoX, hijoY, Math.Max(offsetX / 2, 25));
            }

            // Dibujar rama derecha
            if (current.Right != null)
            {
                int hijoX = x + offsetX;
                int hijoY = y + offsetY;
                g.DrawLine(Pens.Black, x, y, hijoX, hijoY);
                DibujarSubarbol(g, current.Right, hijoX, hijoY, Math.Max(offsetX / 2, 25));
            }

            // Dibujar círculo del nodo
            Rectangle rect = new Rectangle(x - radio, y - radio, radio * 2, radio * 2);

            // Si está resaltado se colorea en gris claro con borde más marcado, si no, blanco normal
            Brush fondo = (nodoResaltado.HasValue && nodoResaltado.Value == current.Id)
                ? Brushes.LightGray
                : Brushes.White;

            g.FillEllipse(fondo, rect);
            g.DrawEllipse(Pens.Black, rect);

            // Dibujar texto del Id
            string texto = current.Id.ToString();
            SizeF size = g.MeasureString(texto, this.Font);
            g.DrawString(texto, this.Font, Brushes.Black, x - (size.Width / 2), y - (size.Height / 2));
        }
    }

    // Panel con buffer doble para evitar parpadeo
    public class DoubleBufferedPanel : Panel
    {
        public DoubleBufferedPanel()
        {
            this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer, true);
            this.UpdateStyles();
        }
    }
}
