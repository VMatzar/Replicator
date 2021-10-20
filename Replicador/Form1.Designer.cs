namespace Replicador
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.BTNReplicar = new System.Windows.Forms.Button();
            this.pgProcesamiento = new System.Windows.Forms.ProgressBar();
            this.LBLInstruccion = new System.Windows.Forms.Label();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.lblCarga = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // BTNReplicar
            // 
            this.BTNReplicar.BackColor = System.Drawing.SystemColors.HighlightText;
            this.BTNReplicar.Font = new System.Drawing.Font("Lato", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BTNReplicar.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.BTNReplicar.Location = new System.Drawing.Point(248, 186);
            this.BTNReplicar.Name = "BTNReplicar";
            this.BTNReplicar.Size = new System.Drawing.Size(310, 81);
            this.BTNReplicar.TabIndex = 0;
            this.BTNReplicar.Text = "Comenzar a Replicar";
            this.BTNReplicar.UseVisualStyleBackColor = false;
            this.BTNReplicar.Click += new System.EventHandler(this.BTNReplicar_Click);
            // 
            // pgProcesamiento
            // 
            this.pgProcesamiento.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.pgProcesamiento.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.pgProcesamiento.Location = new System.Drawing.Point(140, 66);
            this.pgProcesamiento.Maximum = 270;
            this.pgProcesamiento.Name = "pgProcesamiento";
            this.pgProcesamiento.Size = new System.Drawing.Size(549, 49);
            this.pgProcesamiento.TabIndex = 1;
            this.pgProcesamiento.Visible = false;
            // 
            // LBLInstruccion
            // 
            this.LBLInstruccion.AutoSize = true;
            this.LBLInstruccion.Font = new System.Drawing.Font("Lato", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LBLInstruccion.Location = new System.Drawing.Point(141, 150);
            this.LBLInstruccion.Name = "LBLInstruccion";
            this.LBLInstruccion.Size = new System.Drawing.Size(548, 24);
            this.LBLInstruccion.TabIndex = 2;
            this.LBLInstruccion.Text = "Presiona para replicar los datos en una base de datos Destino";
            // 
            // timer
            // 
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // lblCarga
            // 
            this.lblCarga.AutoSize = true;
            this.lblCarga.Font = new System.Drawing.Font("Lato", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCarga.Location = new System.Drawing.Point(309, 118);
            this.lblCarga.Name = "lblCarga";
            this.lblCarga.Size = new System.Drawing.Size(0, 21);
            this.lblCarga.TabIndex = 3;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(848, 312);
            this.Controls.Add(this.lblCarga);
            this.Controls.Add(this.LBLInstruccion);
            this.Controls.Add(this.pgProcesamiento);
            this.Controls.Add(this.BTNReplicar);
            this.Name = "Form1";
            this.Text = "Replicator";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button BTNReplicar;
        private System.Windows.Forms.ProgressBar pgProcesamiento;
        private System.Windows.Forms.Label LBLInstruccion;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.Label lblCarga;
    }
}

