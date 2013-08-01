using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

using SocketServer;

public class ServerServer {
	
	// Incoming data from the client.
	public static string data = null;
	
	public static String lerDados(Socket handler){
		
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
	
//--sem-banco 
/*
	public static void atualizaDadosNoBancoDeDados(Parametros aParam, ConexaoBanco aConexaoBanco){
		
		string comandoSql = "insert into teste (parametro,ldr,sequencia,hora_arduino) "+
			"values(\""+ aParam.getPropValor("nome") +"\","+
			aParam.getPropValor("ldr") + ","+
			aParam.getPropValor("contador") + "," +
			"'" + aParam.getPropValor("Hora") + "'" + ")";
		aConexaoBanco.executar(comandoSql);
		
		comandoSql = ServerServer.atualizaEstadoRele( 1,
		                                             aParam.getPropValor("RELE1-inicioSituacao"),
		                                             aParam.getPropValor("RELE1-ctrlAtual") );
		aConexaoBanco.executar(comandoSql);
		
		comandoSql = ServerServer.atualizaEstadoRele( 2,
		                                             aParam.getPropValor("RELE2-inicioSituacao"),
		                                             aParam.getPropValor("RELE2-ctrlAtual") );
		aConexaoBanco.executar(comandoSql);
		
		comandoSql = ServerServer.atualizaEstadoRele( 3,
		                                             aParam.getPropValor("RELE3-inicioSituacao"),
		                                             aParam.getPropValor("RELE3-ctrlAtual") );
		aConexaoBanco.executar(comandoSql);
		
		comandoSql = ServerServer.atualizaEstadoRele( 4,
		                                             aParam.getPropValor("RELE4-inicioSituacao"),
		                                             aParam.getPropValor("RELE4-ctrlAtual") );
		aConexaoBanco.executar(comandoSql);
		
		return;

	}
*/
	public static void conexaoSocket(Socket aListener/*, ConexaoBanco aConexaoBanco*/){
		
		while (true) {
			
			gravaLog( "" );
			gravaLog( "Waiting for a connection...");

			Socket handler = aListener.Accept();
			data = null;
			
			gravaLog( "Depois accept...");
			
			String dadosLido = lerDados(handler);

			Parametros lParam = new Parametros();
			bool lbDadosIntegro=lParam.parse(dadosLido);
			lParam.listProps();
			
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

	}
	
	public static void encerrarConexoes(Socket handler){
		gravaLog( "Encerrando conexoes...");
		if( handler != null ){
			handler.Shutdown(SocketShutdown.Both);
			handler.Close();
			handler.Dispose();
		}
	}

//--sem-banco 
/*
	public static string atualizaEstadoRele(int aiRele, String asHora, String asEstado){
		
		String comandoSql;

		comandoSql = "call pc_rele_estados(" + aiRele + ",'" + asHora + "', " +
			asEstado + ")";
		
		return comandoSql;
	}
*/
	public static void StartListening() {
		
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

	public static int Main(String[] args) {
		while(true){
			StartListening();
		}
		return 0;
	}
	
	public static void gravaLog(String texto){
		
		string strCaminho = @"log.txt";

		StreamWriter arqSaida = new StreamWriter(strCaminho,true);

		arqSaida.Write("{0} - {1}\r\n",
		               System.DateTime.Now.ToString(),
		               texto);
		
		Console.Write("\r\n{0} - {1}",
		              System.DateTime.Now.ToString(),
		              texto);
		

		arqSaida.Close();

		arqSaida.Dispose();
		
	}
	
}