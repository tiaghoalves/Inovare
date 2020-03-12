namespace Inovare
{
    partial class frmSupervisorio
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
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtFlags = new System.Windows.Forms.TextBox();
            this.cbLeituraPedente = new System.Windows.Forms.CheckBox();
            this.cbImpressaoPendente = new System.Windows.Forms.CheckBox();
            this.cbImpressaoConcluida = new System.Windows.Forms.CheckBox();
            this.cbLoteFinalizado = new System.Windows.Forms.CheckBox();
            this.cbSistemaEmManual = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblLote1 = new System.Windows.Forms.Label();
            this.lblLote2 = new System.Windows.Forms.Label();
            this.lblLote3 = new System.Windows.Forms.Label();
            this.lblLote3Status = new System.Windows.Forms.Label();
            this.lblLote2Status = new System.Windows.Forms.Label();
            this.lblLote1Status = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(484, 197);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(139, 58);
            this.button1.TabIndex = 0;
            this.button1.Text = "Iniciar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(484, 270);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(139, 58);
            this.button2.TabIndex = 1;
            this.button2.Text = "Parar";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "label1";
            // 
            // txtFlags
            // 
            this.txtFlags.Location = new System.Drawing.Point(12, 48);
            this.txtFlags.Multiline = true;
            this.txtFlags.Name = "txtFlags";
            this.txtFlags.Size = new System.Drawing.Size(472, 143);
            this.txtFlags.TabIndex = 3;
            // 
            // cbLeituraPedente
            // 
            this.cbLeituraPedente.AutoSize = true;
            this.cbLeituraPedente.Location = new System.Drawing.Point(15, 265);
            this.cbLeituraPedente.Name = "cbLeituraPedente";
            this.cbLeituraPedente.Size = new System.Drawing.Size(106, 17);
            this.cbLeituraPedente.TabIndex = 4;
            this.cbLeituraPedente.Text = "Leitura pendente";
            this.cbLeituraPedente.UseVisualStyleBackColor = true;
            // 
            // cbImpressaoPendente
            // 
            this.cbImpressaoPendente.AutoSize = true;
            this.cbImpressaoPendente.Location = new System.Drawing.Point(15, 219);
            this.cbImpressaoPendente.Name = "cbImpressaoPendente";
            this.cbImpressaoPendente.Size = new System.Drawing.Size(122, 17);
            this.cbImpressaoPendente.TabIndex = 5;
            this.cbImpressaoPendente.Text = "Impressão pendente";
            this.cbImpressaoPendente.UseVisualStyleBackColor = true;
            this.cbImpressaoPendente.CheckedChanged += new System.EventHandler(this.cbImpressaoPendente_CheckedChanged);
            // 
            // cbImpressaoConcluida
            // 
            this.cbImpressaoConcluida.AutoSize = true;
            this.cbImpressaoConcluida.Location = new System.Drawing.Point(15, 242);
            this.cbImpressaoConcluida.Name = "cbImpressaoConcluida";
            this.cbImpressaoConcluida.Size = new System.Drawing.Size(125, 17);
            this.cbImpressaoConcluida.TabIndex = 6;
            this.cbImpressaoConcluida.Text = "Impressão concluída";
            this.cbImpressaoConcluida.UseVisualStyleBackColor = true;
            this.cbImpressaoConcluida.CheckedChanged += new System.EventHandler(this.cbImpressaoConcluida_CheckedChanged);
            // 
            // cbLoteFinalizado
            // 
            this.cbLoteFinalizado.AutoSize = true;
            this.cbLoteFinalizado.Location = new System.Drawing.Point(15, 288);
            this.cbLoteFinalizado.Name = "cbLoteFinalizado";
            this.cbLoteFinalizado.Size = new System.Drawing.Size(94, 17);
            this.cbLoteFinalizado.TabIndex = 7;
            this.cbLoteFinalizado.Text = "Lote finalizado";
            this.cbLoteFinalizado.UseVisualStyleBackColor = true;
            this.cbLoteFinalizado.CheckedChanged += new System.EventHandler(this.cbLoteFinalizado_CheckedChanged);
            // 
            // cbSistemaEmManual
            // 
            this.cbSistemaEmManual.AutoSize = true;
            this.cbSistemaEmManual.Location = new System.Drawing.Point(15, 311);
            this.cbSistemaEmManual.Name = "cbSistemaEmManual";
            this.cbSistemaEmManual.Size = new System.Drawing.Size(146, 17);
            this.cbSistemaEmManual.TabIndex = 8;
            this.cbSistemaEmManual.Text = "Sistema em modo manual";
            this.cbSistemaEmManual.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(503, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(36, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Lotes:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(503, 76);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(36, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "Lotes:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(503, 101);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(36, 13);
            this.label4.TabIndex = 12;
            this.label4.Text = "Lotes:";
            // 
            // lblLote1
            // 
            this.lblLote1.AutoSize = true;
            this.lblLote1.Location = new System.Drawing.Point(545, 54);
            this.lblLote1.Name = "lblLote1";
            this.lblLote1.Size = new System.Drawing.Size(10, 13);
            this.lblLote1.TabIndex = 13;
            this.lblLote1.Text = " ";
            // 
            // lblLote2
            // 
            this.lblLote2.AutoSize = true;
            this.lblLote2.Location = new System.Drawing.Point(545, 76);
            this.lblLote2.Name = "lblLote2";
            this.lblLote2.Size = new System.Drawing.Size(10, 13);
            this.lblLote2.TabIndex = 14;
            this.lblLote2.Text = " ";
            // 
            // lblLote3
            // 
            this.lblLote3.AutoSize = true;
            this.lblLote3.Location = new System.Drawing.Point(545, 101);
            this.lblLote3.Name = "lblLote3";
            this.lblLote3.Size = new System.Drawing.Size(10, 13);
            this.lblLote3.TabIndex = 15;
            this.lblLote3.Text = " ";
            // 
            // lblLote3Status
            // 
            this.lblLote3Status.AutoSize = true;
            this.lblLote3Status.Location = new System.Drawing.Point(625, 101);
            this.lblLote3Status.Name = "lblLote3Status";
            this.lblLote3Status.Size = new System.Drawing.Size(10, 13);
            this.lblLote3Status.TabIndex = 18;
            this.lblLote3Status.Text = " ";
            // 
            // lblLote2Status
            // 
            this.lblLote2Status.AutoSize = true;
            this.lblLote2Status.Location = new System.Drawing.Point(625, 76);
            this.lblLote2Status.Name = "lblLote2Status";
            this.lblLote2Status.Size = new System.Drawing.Size(10, 13);
            this.lblLote2Status.TabIndex = 17;
            this.lblLote2Status.Text = " ";
            // 
            // lblLote1Status
            // 
            this.lblLote1Status.AutoSize = true;
            this.lblLote1Status.Location = new System.Drawing.Point(625, 54);
            this.lblLote1Status.Name = "lblLote1Status";
            this.lblLote1Status.Size = new System.Drawing.Size(10, 13);
            this.lblLote1Status.TabIndex = 16;
            this.lblLote1Status.Text = " ";
            // 
            // frmSupervisorio
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(693, 364);
            this.Controls.Add(this.lblLote3Status);
            this.Controls.Add(this.lblLote2Status);
            this.Controls.Add(this.lblLote1Status);
            this.Controls.Add(this.lblLote3);
            this.Controls.Add(this.lblLote2);
            this.Controls.Add(this.lblLote1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cbSistemaEmManual);
            this.Controls.Add(this.cbLoteFinalizado);
            this.Controls.Add(this.cbImpressaoConcluida);
            this.Controls.Add(this.cbImpressaoPendente);
            this.Controls.Add(this.cbLeituraPedente);
            this.Controls.Add(this.txtFlags);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Name = "frmSupervisorio";
            this.Text = "Embala Placas - Supervisorio";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmSupervisorio_FormClosed);
            this.Load += new System.EventHandler(this.frmSupervisorio_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtFlags;
        private System.Windows.Forms.CheckBox cbLeituraPedente;
        private System.Windows.Forms.CheckBox cbImpressaoPendente;
        private System.Windows.Forms.CheckBox cbImpressaoConcluida;
        private System.Windows.Forms.CheckBox cbLoteFinalizado;
        private System.Windows.Forms.CheckBox cbSistemaEmManual;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblLote1;
        private System.Windows.Forms.Label lblLote2;
        private System.Windows.Forms.Label lblLote3;
        private System.Windows.Forms.Label lblLote3Status;
        private System.Windows.Forms.Label lblLote2Status;
        private System.Windows.Forms.Label lblLote1Status;
    }
}

