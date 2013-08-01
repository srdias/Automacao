/*
 * Created by SharpDevelop.
 * User: Adriano
 * Date: 13/07/2013
 * Time: 15:54
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace ModulosSocketServer
{
	partial class MainForm
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.label1 = new System.Windows.Forms.Label();
			this.valorTemperatura = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.label3 = new System.Windows.Forms.Label();
			this.valorUmidade = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.label6 = new System.Windows.Forms.Label();
			this.valorLuminosidade = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.textStatusSocket = new System.Windows.Forms.Label();
			this.button1 = new System.Windows.Forms.Button();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.textoMensagens = new System.Windows.Forms.ListBox();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.button4 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.groupBox4 = new System.Windows.Forms.GroupBox();
			this.textValorAlterado = new System.Windows.Forms.TextBox();
			this.textIndice = new System.Windows.Forms.TextBox();
			this.textModulo = new System.Windows.Forms.TextBox();
			this.label11 = new System.Windows.Forms.Label();
			this.label10 = new System.Windows.Forms.Label();
			this.button3 = new System.Windows.Forms.Button();
			this.textValor = new System.Windows.Forms.TextBox();
			this.textVariavel = new System.Windows.Forms.TextBox();
			this.label9 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.tvVariaveis = new System.Windows.Forms.TreeView();
			this.button5 = new System.Windows.Forms.Button();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.tabControl1.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.tabPage2.SuspendLayout();
			this.groupBox4.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.BackColor = System.Drawing.SystemColors.Control;
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.valorTemperatura);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.groupBox1.Location = new System.Drawing.Point(19, 20);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(269, 151);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Temperatura";
			// 
			// label1
			// 
			this.label1.BackColor = System.Drawing.SystemColors.Info;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(207, 44);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(44, 45);
			this.label1.TabIndex = 1;
			this.label1.Text = "c";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// valorTemperatura
			// 
			this.valorTemperatura.BackColor = System.Drawing.SystemColors.Info;
			this.valorTemperatura.Font = new System.Drawing.Font("Microsoft Sans Serif", 60F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.valorTemperatura.Location = new System.Drawing.Point(23, 44);
			this.valorTemperatura.Name = "valorTemperatura";
			this.valorTemperatura.Size = new System.Drawing.Size(182, 94);
			this.valorTemperatura.TabIndex = 0;
			this.valorTemperatura.Text = "999";
			this.valorTemperatura.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label2
			// 
			this.label2.BackColor = System.Drawing.SystemColors.Info;
			this.label2.Location = new System.Drawing.Point(23, 44);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(240, 94);
			this.label2.TabIndex = 5;
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.label3);
			this.groupBox2.Controls.Add(this.valorUmidade);
			this.groupBox2.Controls.Add(this.label5);
			this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.groupBox2.Location = new System.Drawing.Point(308, 20);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(269, 151);
			this.groupBox2.TabIndex = 1;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Umidade";
			// 
			// label3
			// 
			this.label3.BackColor = System.Drawing.SystemColors.Info;
			this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label3.Location = new System.Drawing.Point(198, 45);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(44, 45);
			this.label3.TabIndex = 7;
			this.label3.Text = "%";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// valorUmidade
			// 
			this.valorUmidade.BackColor = System.Drawing.SystemColors.Info;
			this.valorUmidade.Font = new System.Drawing.Font("Microsoft Sans Serif", 60F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.valorUmidade.Location = new System.Drawing.Point(14, 45);
			this.valorUmidade.Name = "valorUmidade";
			this.valorUmidade.Size = new System.Drawing.Size(182, 94);
			this.valorUmidade.TabIndex = 6;
			this.valorUmidade.Text = "999";
			this.valorUmidade.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label5
			// 
			this.label5.BackColor = System.Drawing.SystemColors.Info;
			this.label5.Location = new System.Drawing.Point(14, 45);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(240, 94);
			this.label5.TabIndex = 8;
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.label6);
			this.groupBox3.Controls.Add(this.valorLuminosidade);
			this.groupBox3.Controls.Add(this.label8);
			this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.groupBox3.Location = new System.Drawing.Point(598, 20);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(269, 151);
			this.groupBox3.TabIndex = 2;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Luminosidade";
			// 
			// label6
			// 
			this.label6.BackColor = System.Drawing.SystemColors.Info;
			this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label6.Location = new System.Drawing.Point(198, 45);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(44, 45);
			this.label6.TabIndex = 7;
			this.label6.Text = "%";
			this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// valorLuminosidade
			// 
			this.valorLuminosidade.BackColor = System.Drawing.SystemColors.Info;
			this.valorLuminosidade.Font = new System.Drawing.Font("Microsoft Sans Serif", 60F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.valorLuminosidade.Location = new System.Drawing.Point(14, 45);
			this.valorLuminosidade.Name = "valorLuminosidade";
			this.valorLuminosidade.Size = new System.Drawing.Size(182, 94);
			this.valorLuminosidade.TabIndex = 6;
			this.valorLuminosidade.Text = "999";
			this.valorLuminosidade.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label8
			// 
			this.label8.BackColor = System.Drawing.SystemColors.Info;
			this.label8.Location = new System.Drawing.Point(14, 45);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(240, 94);
			this.label8.TabIndex = 8;
			// 
			// textStatusSocket
			// 
			this.textStatusSocket.BackColor = System.Drawing.SystemColors.ActiveCaption;
			this.textStatusSocket.Location = new System.Drawing.Point(747, 174);
			this.textStatusSocket.Name = "textStatusSocket";
			this.textStatusSocket.Size = new System.Drawing.Size(120, 23);
			this.textStatusSocket.TabIndex = 3;
			this.textStatusSocket.Text = "Pronto...";
			this.textStatusSocket.TextChanged += new System.EventHandler(this.TextStatusSocketTextChanged);
			// 
			// button1
			// 
			this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.button1.Location = new System.Drawing.Point(891, 34);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(120, 137);
			this.button1.TabIndex = 4;
			this.button1.Text = "Sair";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.Button1Click);
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.tabPage1);
			this.tabControl1.Controls.Add(this.tabPage2);
			this.tabControl1.Location = new System.Drawing.Point(19, 177);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(992, 467);
			this.tabControl1.TabIndex = 6;
			// 
			// tabPage1
			// 
			this.tabPage1.Controls.Add(this.textoMensagens);
			this.tabPage1.Location = new System.Drawing.Point(4, 22);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage1.Size = new System.Drawing.Size(984, 441);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "Registros";
			this.tabPage1.UseVisualStyleBackColor = true;
			// 
			// textoMensagens
			// 
			this.textoMensagens.FormattingEnabled = true;
			this.textoMensagens.Location = new System.Drawing.Point(6, 5);
			this.textoMensagens.Name = "textoMensagens";
			this.textoMensagens.Size = new System.Drawing.Size(972, 420);
			this.textoMensagens.TabIndex = 6;
			// 
			// tabPage2
			// 
			this.tabPage2.Controls.Add(this.button5);
			this.tabPage2.Controls.Add(this.button4);
			this.tabPage2.Controls.Add(this.button2);
			this.tabPage2.Controls.Add(this.groupBox4);
			this.tabPage2.Controls.Add(this.tvVariaveis);
			this.tabPage2.Location = new System.Drawing.Point(4, 22);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage2.Size = new System.Drawing.Size(984, 441);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "Variáveis";
			this.tabPage2.UseVisualStyleBackColor = true;
			// 
			// button4
			// 
			this.button4.Location = new System.Drawing.Point(632, 191);
			this.button4.Name = "button4";
			this.button4.Size = new System.Drawing.Size(75, 23);
			this.button4.TabIndex = 3;
			this.button4.Text = "Altera Valor";
			this.button4.UseVisualStyleBackColor = true;
			this.button4.Click += new System.EventHandler(this.Button4Click);
			// 
			// button2
			// 
			this.button2.Location = new System.Drawing.Point(551, 191);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(75, 23);
			this.button2.TabIndex = 1;
			this.button2.Text = "Carga Inicial";
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new System.EventHandler(this.Button2Click);
			// 
			// groupBox4
			// 
			this.groupBox4.Controls.Add(this.textValorAlterado);
			this.groupBox4.Controls.Add(this.textIndice);
			this.groupBox4.Controls.Add(this.textModulo);
			this.groupBox4.Controls.Add(this.label11);
			this.groupBox4.Controls.Add(this.label10);
			this.groupBox4.Controls.Add(this.button3);
			this.groupBox4.Controls.Add(this.textValor);
			this.groupBox4.Controls.Add(this.textVariavel);
			this.groupBox4.Controls.Add(this.label9);
			this.groupBox4.Controls.Add(this.label7);
			this.groupBox4.Location = new System.Drawing.Point(544, 7);
			this.groupBox4.Name = "groupBox4";
			this.groupBox4.Size = new System.Drawing.Size(427, 169);
			this.groupBox4.TabIndex = 2;
			this.groupBox4.TabStop = false;
			this.groupBox4.Text = "Alterar valor:";
			// 
			// textValorAlterado
			// 
			this.textValorAlterado.Location = new System.Drawing.Point(229, 127);
			this.textValorAlterado.Name = "textValorAlterado";
			this.textValorAlterado.ReadOnly = true;
			this.textValorAlterado.Size = new System.Drawing.Size(71, 20);
			this.textValorAlterado.TabIndex = 11;
			// 
			// textIndice
			// 
			this.textIndice.Location = new System.Drawing.Point(69, 58);
			this.textIndice.Name = "textIndice";
			this.textIndice.ReadOnly = true;
			this.textIndice.Size = new System.Drawing.Size(340, 20);
			this.textIndice.TabIndex = 10;
			// 
			// textModulo
			// 
			this.textModulo.Location = new System.Drawing.Point(69, 24);
			this.textModulo.Name = "textModulo";
			this.textModulo.ReadOnly = true;
			this.textModulo.Size = new System.Drawing.Size(340, 20);
			this.textModulo.TabIndex = 9;
			// 
			// label11
			// 
			this.label11.Location = new System.Drawing.Point(7, 61);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(59, 17);
			this.label11.TabIndex = 8;
			this.label11.Text = "Sequência:";
			// 
			// label10
			// 
			this.label10.Location = new System.Drawing.Point(7, 27);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(59, 17);
			this.label10.TabIndex = 7;
			this.label10.Text = "Módulo:";
			// 
			// button3
			// 
			this.button3.Location = new System.Drawing.Point(333, 126);
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size(75, 23);
			this.button3.TabIndex = 6;
			this.button3.Text = "Alterar";
			this.button3.UseVisualStyleBackColor = true;
			this.button3.Click += new System.EventHandler(this.Button3Click);
			// 
			// textValor
			// 
			this.textValor.Location = new System.Drawing.Point(68, 127);
			this.textValor.Name = "textValor";
			this.textValor.Size = new System.Drawing.Size(134, 20);
			this.textValor.TabIndex = 5;
			// 
			// textVariavel
			// 
			this.textVariavel.Location = new System.Drawing.Point(68, 93);
			this.textVariavel.Name = "textVariavel";
			this.textVariavel.ReadOnly = true;
			this.textVariavel.Size = new System.Drawing.Size(340, 20);
			this.textVariavel.TabIndex = 4;
			// 
			// label9
			// 
			this.label9.Location = new System.Drawing.Point(7, 127);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(59, 17);
			this.label9.TabIndex = 2;
			this.label9.Text = "Valor:";
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(7, 93);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(59, 17);
			this.label7.TabIndex = 1;
			this.label7.Text = "Variável:";
			// 
			// tvVariaveis
			// 
			this.tvVariaveis.Location = new System.Drawing.Point(6, 13);
			this.tvVariaveis.Name = "tvVariaveis";
			this.tvVariaveis.Size = new System.Drawing.Size(532, 413);
			this.tvVariaveis.TabIndex = 0;
			this.tvVariaveis.DoubleClick += new System.EventHandler(this.TvVariaveisDoubleClick);
			// 
			// button5
			// 
			this.button5.Location = new System.Drawing.Point(715, 191);
			this.button5.Name = "button5";
			this.button5.Size = new System.Drawing.Size(114, 23);
			this.button5.TabIndex = 4;
			this.button5.Text = "Localizar alterados";
			this.button5.UseVisualStyleBackColor = true;
			this.button5.Click += new System.EventHandler(this.Button5Click);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1034, 668);
			this.Controls.Add(this.tabControl1);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.textStatusSocket);
			this.Controls.Add(this.groupBox3);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.Location = new System.Drawing.Point(10, 10);
			this.Name = "MainForm";
			this.Text = "ModulosSocketServer";
			this.Load += new System.EventHandler(this.MainFormLoad);
			this.groupBox1.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.groupBox3.ResumeLayout(false);
			this.tabControl1.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.tabPage2.ResumeLayout(false);
			this.groupBox4.ResumeLayout(false);
			this.groupBox4.PerformLayout();
			this.ResumeLayout(false);
		}
		private System.Windows.Forms.Button button5;
		private System.Windows.Forms.Button button4;
		private System.Windows.Forms.TextBox textValorAlterado;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.TextBox textModulo;
		private System.Windows.Forms.TextBox textIndice;
		private System.Windows.Forms.Button button3;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.TextBox textVariavel;
		private System.Windows.Forms.TextBox textValor;
		private System.Windows.Forms.GroupBox groupBox4;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.TreeView tvVariaveis;
		private System.Windows.Forms.TabPage tabPage2;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.ListBox textoMensagens;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Label textStatusSocket;
		private System.Windows.Forms.Label valorLuminosidade;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.Label valorUmidade;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Label valorTemperatura;
		private System.Windows.Forms.GroupBox groupBox1;
	}
}
