/*
 * Created by SharpDevelop.
 * User: Adriano
 * Date: 13/07/2013
 * Time: 16:11
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace ModulosSocketServer
{
	
	/// <summary>
	/// Description of ThreadSocket.
	/// </summary>
	public class ThreadSocket
	{
		
		System.Windows.Forms.Label ilabelTemeratura;
		public ThreadSocket(System.Windows.Forms.Label alabelTemeratura ){
			ilabelTemeratura=alabelTemeratura;
		}
		
		public void iniciar(){
			
			for(int i=0;i<10;i++){
				String teste;
				teste = i.ToString();
				ilabelTemeratura.Text = teste;
				System.Threading.Thread.Sleep(4000);;
			};
		}



	}
	
}
