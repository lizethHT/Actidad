using System.Data;
using System.Data.SqlClient;

namespace AppADONET
{
    public partial class frmTipoDocumento : Form
    {
        public frmTipoDocumento()
        {
            InitializeComponent();
        }

        private void btnDatos_Click(object sender, EventArgs e)
        {
            CargarDatos();
        }
        private void CargarDatos()
        {
            string cadenaConexion = "server=localhost; database=Financiera; Integrated Security = true";
            var conexion = new SqlConnection(cadenaConexion);
            var adaptador = new SqlDataAdapter("Select * from TipoDocumento", conexion);
            var datos = new DataSet();
            adaptador.Fill(datos, "TipoDocumento");

            dgvDatos.DataSource = datos.Tables["TipoDocumento"];
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            var frm = new frmTipoDocumentoEdit ();
            frm.ShowDialog();
            CargarDatos();
        }

        private int getId()
        {
            try
            {
                DataGridViewRow filaActual = dgvDatos.CurrentRow;
                if (filaActual == null)
                {
                    return 0;
                }
                return int.Parse(dgvDatos.Rows[filaActual.Index].Cells[0].Value.ToString());
            }
            catch (Exception)
            {

                return 0;
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            int id = getId();
            if (id > 0)
            {
                var frm = new frmTipoDocumentoEdit(id);
                frm.ShowDialog();
                CargarDatos();
            }
            else
            {
                MessageBox.Show("Seleccione un Id valido", "Sistema",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            int id = getId();
            if (id > 0)
            {
                DialogResult respuesta = MessageBox.Show("¿Realmente deaea eliminar el registro?", "Sistema",
               MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (respuesta == DialogResult.Yes)
                {
                    var adaptador = new dsAppTableAdapters.TipoDocumentoTableAdapter();
                    adaptador.Remove(id);

                    MessageBox.Show("Registro Eliminado", "Sistema",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                    CargarDatos();
                }
                else
                {
                    MessageBox.Show("Debe seleccionar un Id valido", "Sistemas",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
        }   }
    }
}