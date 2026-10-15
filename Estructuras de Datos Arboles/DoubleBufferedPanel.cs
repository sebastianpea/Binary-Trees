using System.Windows.Forms;

namespace Estructuras_de_Datos_Arboles
{
    // =========================================================================
    // CUSTOM CONTROL: DoubleBufferedPanel
    // Responsibility: Custom Panel with optimized double buffering enabled to
    // prevent screen flickering during tree repainting.
    // =========================================================================
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
