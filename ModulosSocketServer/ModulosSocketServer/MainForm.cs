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
		}
		
		private void ThreadTarefa(){

			StartListening();
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
		
		public String lerDados(Socket handler){
			
			int contadorSleep=0;
			int contadorSleepAposLeitura=0;
			int contadorLeituras=0;
			byte[] bytes = new Byte[1024];
			
			// An incoming connection needs to be processed.
			while (true) {
				bytes = new byte[1024];
				if(handler.Available<1){
					if( contadorSleep > 2000 || contadorSleepAposLeitura  > 60 ){
						break;
					}
					contadorSleep += 20;
					if(contadorLeituras > 0){
						contadorSleepAposLeitura += 20;
					}
					//data += ";";
					Thread.Sleep(20);
					
				}else{
					int bytesRec = handler.Receive(bytes);
					contadorLeituras++;
					contadorSleepAposLeitura=0;
					data += Encoding.ASCII.GetString(bytes,0,bytesRec);
				};
			}
			
			gravaLog( "Texto recebido: '" + data + "'" );
			gravaLog( "Tempo total de espera: " + contadorSleep );
			
			return data;
		}
		
		public void conexaoSocket(Socket aListener/*, ConexaoBanco aConexaoBanco*/){
			
			while (!ibEncerrar) {
				
				gravaLog( "" );
				gravaLog( "Waiting for a connection...");
				/*
				Thread.Sleep(2000);
				
				VariaveisTreeView lVarTv = new VariaveisTreeView(tvVariaveis);
				List<Variavel> variaveisList = new List<Variavel>();
				
				lVarTv.nodesVariaveisModificadas(variaveisList);
				
				for(int i=0;i<variaveisList.Count;i++){
					//textoMensagens.Items.Add(variaveisList[i].getVariavelForModulo());
					SetControlPropertyValue(textStatusSocket,"Text",variaveisList[i].getVariavelForModulo());
				}
				
				continue;
				 */
				Socket handler = aListener.Accept();
				data = null;
				
				gravaLog( "Depois accept...");
				
				String dadosLido = lerDados(handler);

				Parametros lParam = new Parametros();
				bool lbDadosIntegro=lParam.parse(dadosLido);
				lParam.listProps(this);
				
				if( lbDadosIntegro ){
					//--sem-banco atualizaDadosNoBancoDeDados(lParam, aConexaoBanco);
				}else{
					gravaLog( "Dados nao inseridos no banco de dados por nao estarem integros." );
				}

				String lsDateSend=String.Format("{0:HHmmssddMMyyyy}", System.DateTime.Now);

				data = "00001|HORA="+lsDateSend+";";
				byte[] msg = Encoding.ASCII.GetBytes(data);
				handler.Send(msg);

				encerrarConexoes(handler);
			}
			
			Thread.Sleep(2000);
			gravaLog( "Saindo do loop do servidor socket.");

		}
		
		public void encerrarConexoes(Socket handler){
			gravaLog( "Encerrando conexoes...");
			if( handler != null ){
				handler.Shutdown(SocketShutdown.Both);
				handler.Close();
				//handler.Dispose();
			}
		}

		public void StartListening() {
			
			//--sem-banco ConexaoBanco lConexaoBanco = new ConexaoBanco();
			
			//--sem-banco lConexaoBanco.conectar();

			gravaLog( "Nome da maquina: " + Dns.GetHostName() );

			IPHostEntry ipHostInfo = Dns.Resolve(Dns.GetHostName());
			IPAddress ipAddress = ipHostInfo.AddressList[0];
			IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 11000);

			Socket listener = new Socket(AddressFamily.InterNetwork,
			                             SocketType.Stream, ProtocolType.Tcp );
			
			foreach (IPAddress ip in ipHostInfo.AddressList){
				gravaLog( "   IP do servidor: " + ip);
			}
			
			gravaLog( "Socket criado." );

			try {
				listener.Bind(localEndPoint);
				listener.Listen(10);

				gravaLog( "Esperando conexoes." );
				
				conexaoSocket(listener/*, lConexaoBanco*/);
				
			} catch (SocketException e){
				
				Console.Write("\nErro de SocketException\n");
				Console.WriteLine(e.ToString());
				gravaLog( "Erro de Exception" );
				gravaLog( e.ToString() );
				
				Console.Write(". ok");
				
			} catch (Exception e) {
				Console.WriteLine(e.ToString());
				gravaLog( "Erro de Exception" );
				gravaLog( e.ToString() );
				Thread.Sleep(10000);
			}
			
			//--sem-banco lConexaoBanco.desconectar();
			
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
			
			String textoStatusSocket = System.DateTime.Now.ToString() + " " + texto;
			SetControlPropertyValue(textStatusSocket,"Text",textoStatusSocket);
			

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
			textoMensagens.Items.Add(textStatusSocket.Text);
			//textoMensagens.ScrollIntoView(textoMensagens.Items.Count -1);
			textoMensagens.SelectedIndex = textoMensagens.Items.Count -1;
		}
		
		void Button2Click(object sender, EventArgs e)
		{
			
			VariaveisTreeView lVarTv = new VariaveisTreeView(tvVariaveis);
			
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
			
			VariaveisTreeView lVarTv = new VariaveisTreeView(tvVariaveis);
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
			VariaveisTreeView lVarTv = new VariaveisTreeView(tvVariaveis);
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
			VariaveisTreeView lVarTv = new VariaveisTreeView(tvVariaveis);
			
			lVarTv.atualizaValorVariavel( new Variavel(3,1,0,20) );
		}
		
		void Button5Click(object sender, EventArgs e)
		{
			VariaveisTreeView lVarTv = new VariaveisTreeView(tvVariaveis);
			List<Variavel> variaveisList = new List<Variavel>();
			
			lVarTv.nodesVariaveisModificadas(variaveisList);
			
			for(int i=0;i<variaveisList.Count;i++){
				textoMensagens.Items.Add(variaveisList[i].getVariavelForModulo());
			}
			
		}
		
		void Button6Click(object sender, EventArgs e)
		{
			StartClient();
		}
		
		public void StartClient() {
			byte[] bytes = new byte[1024];

			try {
				IPHostEntry ipHostInfo = Dns.Resolve(Dns.GetHostName());
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
					gravaLog("Dados recebidos: '" + Encoding.ASCII.GetString(bytes,0,bytesRec) + "'");

					sender.Shutdown(SocketShutdown.Both);
					sender.Close();
					
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
	}
}
