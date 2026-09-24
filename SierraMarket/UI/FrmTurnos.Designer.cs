namespace SierraMarket.UI
{
    partial class FrmTurnos
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            TxtNombre = new TextBox();
            label3 = new Label();
            label4 = new Label();
            BtnAgregar = new Button();
            LstTurnos = new ListBox();
            DtpHoraInicio = new DateTimePicker();
            DtpHoraFin = new DateTimePicker();
            label2 = new Label();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Geist Mono", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(14, 13);
            label1.Name = "label1";
            label1.Size = new Size(180, 23);
            label1.TabIndex = 1;
            label1.Text = "Gestión de turnos";
            // 
            // TxtNombre
            // 
            TxtNombre.Location = new Point(17, 70);
            TxtNombre.Name = "TxtNombre";
            TxtNombre.Size = new Size(109, 23);
            TxtNombre.TabIndex = 3;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Geist Mono Medium", 6F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.Location = new Point(17, 55);
            label3.Name = "label3";
            label3.Size = new Size(41, 12);
            label3.TabIndex = 4;
            label3.Text = "Nombre";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Geist Mono Medium", 6F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.Location = new Point(146, 55);
            label4.Name = "label4";
            label4.Size = new Size(71, 12);
            label4.TabIndex = 5;
            label4.Text = "Hora Inicio";
            // 
            // BtnAgregar
            // 
            BtnAgregar.Location = new Point(262, 116);
            BtnAgregar.Name = "BtnAgregar";
            BtnAgregar.Size = new Size(75, 23);
            BtnAgregar.TabIndex = 7;
            BtnAgregar.Text = "Agregar";
            BtnAgregar.UseVisualStyleBackColor = true;
            BtnAgregar.Click += BtnAgregar_Click;
            // 
            // LstTurnos
            // 
            LstTurnos.FormattingEnabled = true;
            LstTurnos.Location = new Point(17, 145);
            LstTurnos.Name = "LstTurnos";
            LstTurnos.Size = new Size(320, 289);
            LstTurnos.TabIndex = 8;
            // 
            // DtpHoraInicio
            // 
            DtpHoraInicio.CustomFormat = "HH:mm tt";
            DtpHoraInicio.Format = DateTimePickerFormat.Custom;
            DtpHoraInicio.Location = new Point(147, 70);
            DtpHoraInicio.Name = "DtpHoraInicio";
            DtpHoraInicio.Size = new Size(88, 23);
            DtpHoraInicio.TabIndex = 9;
            // 
            // DtpHoraFin
            // 
            DtpHoraFin.CustomFormat = "HH:mm tt";
            DtpHoraFin.Format = DateTimePickerFormat.Custom;
            DtpHoraFin.Location = new Point(249, 70);
            DtpHoraFin.Name = "DtpHoraFin";
            DtpHoraFin.Size = new Size(88, 23);
            DtpHoraFin.TabIndex = 10;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Geist Mono Medium", 6F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(249, 55);
            label2.Name = "label2";
            label2.Size = new Size(53, 12);
            label2.TabIndex = 11;
            label2.Text = "Hora Fin";
            // 
            // FrmTurnos
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(359, 450);
            Controls.Add(label2);
            Controls.Add(DtpHoraFin);
            Controls.Add(DtpHoraInicio);
            Controls.Add(LstTurnos);
            Controls.Add(BtnAgregar);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(TxtNombre);
            Controls.Add(label1);
            Name = "FrmTurnos";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Turnos | Sierra Market";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox TxtNombre;
        private Label label3;
        private Label label4;
        private Button BtnAgregar;
        private ListBox LstTurnos;
        private DateTimePicker DtpHoraInicio;
        private DateTimePicker DtpHoraFin;
        private Label label2;
    }
}