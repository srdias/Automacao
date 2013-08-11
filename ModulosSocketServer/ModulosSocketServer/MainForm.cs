/*
 * Created by SharpDevelop.
 * User: Adriano
 * Date: 13/07/2013
 * Time: 15:54
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace ModulosSocketServer
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	/// 
	public partial class MainForm : Form
	{
		
		public static string data = null;
		public static Boolean ibEncerrar=false;
		public MainForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		void MainFormLoad(object sender, EventArgs e){
			
			inicializarComponentes();
			startThread();
			
		}
		
		public void inicializarComponentes(){
			textStatusSocket.Visible=false;
			valorTemperatura.Text="--";
			valorUmidade.Text="--";
			valorLuminosidade.Text="--";

		}
		
		public void startThread(){
			Thread trd = new Thread(new ThreadStart(this.ThreadTarefa));
			trd.IsBackground = true;
			trd.Start();
		}
		
		public void displayProp(String propname, String propValue){
			String lsValor;
			if(propname=="Temperatura"){
				SetControlPropertyValue(valorTemperatura,"Text",propValue);
			}
			if(propname=="ldr"){
				int liValor=Convert.ToInt32(propValue);
				liValor=liValor*100/1024;
				lsValor=liValor.ToString();
				SetControlPropertyValue(valorLuminosidade,"Text",lsValor);
			}
			if(propname=="Umidade"){
				SetControlPropertyValue(valorUmidade,"Text",propValue);
			}
			if(propname=="Hora"){
				SetControlPropertyValue(valorHora,"Text",propValue);
			}
		}
		
		private void ThreadTarefa(){

			gravaLog( "Iniciando thread...");
			while(!ibEncerrar){
				StartClient();
				Thread.Sleep(500);
			};
			Thread.Sleep(1000);
			gravaLog( "Thread finalizada..." );

		}
		
		delegate void SetControlValueCallback(Control oControl, string propName, object propValue);
		private void SetControlPropertyValue(Control oControl, string propName, object propValue)
		{
			if (oControl.InvokeRequired)
			{
				SetControlValueCallback d = new SetControlValueCallback(SetControlPropertyValue);
				oControl.Invoke(d, new object[] { oControl, propName, propValue });
			}
			else
			{
				Type t = oControl.GetType();
				PropertyInfo[] props = t.GetProperties();
				foreach (PropertyInfo p in props)
				{
					if (p.Name.ToUpper() == propName.ToUpper())
					{
						p.SetValue(oControl, propValue, null);
					}
				}
			}
		}

		public void dadosRecebidos(String texto){
			SetControlPropertyValue(textStatusSocket,"Text","data:"+texto);
		}
		
		public void gravaLog(String texto){
			
			string strCaminho = @"log.txt";

			StreamWriter arqSaida = new StreamWriter(strCaminho,true);

			arqSaida.Write("{0} - {1}\r\n",
			               System.DateTime.Now.ToString(),
			               texto);
			
			Console.Write("\r\n{0} - {1}",
			              System.DateTime.Now.ToString(),
			              texto);
			
			if( texto != null ){
//				String textoStatusSocket = System.DateTime.Now.ToString() + " " + texto;
//				SetControlPropertyValue(textStatusSocket,"Text",textoStatusSocket);
			};

			arqSaida.Close();

			arqSaida.Dispose();
			
		}
		
		void Button1Click(object sender, EventArgs e)
		{
			ibEncerrar=true;
			gravaLog( "Solicitando encerramento do socket.");
			button1.Enabled=false;
		}
		
		void TextStatusSocketTextChanged(object sender, EventArgs e)
		{
			
			if( textStatusSocket.Text.Substring(0,5).Equals("data:") ){
				String lsConteudo=textStatusSocket.Text.Substring(5);
				VariaveisTreeView lVarTv = new VariaveisTreeView(tvVariaveis,this);
				Parametros lParam = new Parametros();
				lParam.parseBase32(lsConteudo,lVarTv,this);
				tvVariaveis.ExpandAll();
			}else{
				textoMensagens.Items.Add(textStatusSocket.Text);
			}
			textoMensagens.SelectedIndex = textoMensagens.Items.Count -1;
		}
		
		void Button2Click(object sender, EventArgs e)
		{
			
			VariaveisTreeView lVarTv = new VariaveisTreeView(tvVariaveis,this);
			
			lVarTv.atualizaVariavel( new Variavel(1,1,0,334) );
			lVarTv.atualizaVariavel( new Variavel(1,1,1,234) );
			lVarTv.atualizaVariavel( new Variavel(1,1,3,334) );
			lVarTv.atualizaVariavel( new Variavel(1,1,4,334) );
			lVarTv.atualizaVariavel( new Variavel(1,1,5,334) );
			lVarTv.atualizaVariavel( new Variavel(1,1,6,334) );
			lVarTv.atualizaVariavel( new Variavel(1,2,1, 34) );
			lVarTv.atualizaVariavel( new Variavel(2,1,0,534) );
			lVarTv.atualizaVariavel( new Variavel(2,1,1,534) );
			lVarTv.atualizaVariavel( new Variavel(2,1,2,534) );
			lVarTv.atualizaVariavel( new Variavel(3,1,0,21) );
			lVarTv.atualizaVariavel( new Variavel(3,1,1,38) );
			lVarTv.atualizaVariavel( new Variavel(3,1,2,15) );
			lVarTv.atualizaVariavel( new Variavel(1,1,4,250) );

			tvVariaveis.ExpandAll();
			
		}
		
		void TvVariaveisDoubleClick(object sender, EventArgs e)
		{
			
			VariaveisTreeView lVarTv = new VariaveisTreeView(tvVariaveis,this);
			Variavel lVariavel = new Variavel();
			
			lVariavel= lVarTv.getVariavelByTreeNode();
			
			textVariavel.Text=lVariavel.getNomeVariavelTV();
			textIndice.Text=lVariavel.getNomeIndiceTV();
			textModulo.Text=lVariavel.getNomeModuloTV();
			textValor.Text=lVariavel.getValorTV();
			textValorAlterado.Text=lVariavel.getValorAlteradoTV();
			
		}
		
		void Button3Click(object sender, EventArgs e)
		{
			VariaveisTreeView lVarTv = new VariaveisTreeView(tvVariaveis,this);
			Variavel lVariavel = new Variavel();
			Variavel lVariavelTV;
			
			lVariavel.setCodigoModuloByTexto(textModulo.Text);
			lVariavel.setCodigoIndiceByTexto(textIndice.Text);
			lVariavel.setCodigoVariavelByTexto(textVariavel.Text);
			
			lVariavelTV=lVarTv.getVariavelTV(lVariavel);
			if(lVariavelTV!=null){
				lVariavel=lVariavelTV;
			}

			string lsValorAlterado="("+textValor.Text+")";
			lVariavel.setValorAlteradoByTexto(lsValorAlterado);
			
			lVarTv.atualizaVariavel( lVariavel );

		}
		
		void Button4Click(object sender, EventArgs e)
		{
			VariaveisTreeView lVarTv = new VariaveisTreeView(tvVariaveis,this);
			
			lVarTv.atualizaValorVariavel( new Variavel(3,1,0,20) );
		}
		
		void Button5Click(object sender, EventArgs e)
		{
			VariaveisTreeView lVarTv = new VariaveisTreeView(tvVariaveis,this);
			List<Variavel> variaveisList = new List<Variavel>();
			
			lVarTv.nodesVariaveisModificadas(variaveisList);
			
			for(int i=0;i<variaveisList.Count;i++){
				textoMensagens.Items.Add(variaveisList[i].getVariavelForModulo());
			}
			
		}
		
		
		public void StartClient() {
			byte[] bytes = new byte[1024];

			try {
				IPAddress ipAddress = IPAddress.Parse("192.168.0.119");
				IPEndPoint remoteEP = new IPEndPoint(ipAddress,80);

				Socket sender = new Socket(AddressFamily.InterNetwork,
				                           SocketType.Stream,
				                           ProtocolType.Tcp );

				try {
					sender.Connect(remoteEP);

					gravaLog("Socket connected to IP: "  + sender.RemoteEndPoint.ToString());

					byte[] msg = Encoding.ASCII.GetBytes("This is a test<EOF>");

					int bytesSent = sender.Send(msg);

					int bytesRec = sender.Receive(bytes);
					String dadosLido = Encoding.ASCII.GetString(bytes,0,bytesRec);
					
					dadosRecebidos(dadosLido);
					gravaLog(dadosLido);

					sender.Shutdown(SocketShutdown.Both);
					sender.Close();
					
					gravaLog("Conexão encerrada.");
					
				} catch (ArgumentNullException ane) {
					gravaLog("ArgumentNullException : " + ane.ToString());
				} catch (SocketException se) {
					gravaLog("SocketException : " + se.ToString());
				} catch (Exception e) {
					gravaLog("Unexpected exception : " + e.ToString());
				}

			} catch (Exception e) {
				Console.WriteLine( e.ToString());
			}
		}
		
		void Button6Click(object sender, EventArgs e)
		{
			VariaveisTreeView lVarTv = new VariaveisTreeView(tvVariaveis,this);
			Parametros lParam = new Parametros();
			String lsConteudo="[v:bbASAUb34S4LSP0|v:ccaaAU|v:ddab0Maaa|v:eeaE2|]FreeRam=151";
			lParam.parseBase32(lsConteudo,lVarTv,this);
		}
	}
}
