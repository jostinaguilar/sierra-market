using SierraMarket.Estructuras.Listas;
using SierraMarket.Modelos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SierraMarket.UI
{
    public partial class FrmTurnos : Form
    {
        private ListaSimple<Turno> listaTurnos;

        public FrmTurnos()
        {
            InitializeComponent();

            listaTurnos = new ListaSimple<Turno>();
        }

        private void BtnAgregar_Click(object sender, EventArgs e)
        {
            string nombre = TxtNombre.Text.Trim();
            DateTime horaInicio = DtpHoraInicio.Value;
            DateTime horaFin = DtpHoraFin.Value;

            if (string.IsNullOrEmpty(nombre))
            {
                MessageBox.Show("Ingrese el nombre del turno", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int id = listaTurnos.Count + 1;
            Turno nuevo = new Turno(id, nombre, horaInicio, horaFin);
            listaTurnos.Agregar(nuevo);
            
            TxtNombre.Clear();
            ActualizarLista();

        }

        public void ActualizarLista()
        {
            LstTurnos.Items.Clear();
            foreach (var turno in listaTurnos.ObtenerTodos())
            {
                LstTurnos.Items.Add(turno);
            }
        }
    }
}
