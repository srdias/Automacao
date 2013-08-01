/*
 * Criado por SharpDevelop.
 * Usuário: Adriano
 * Data: 22/12/2012
 * Hora: 14:12
 * 
 * Para alterar este modelo use Ferramentas | Opções | Codificação | Editar Cabeçalhos Padrão.
 */
using System;
using System.Collections.Generic;

namespace SocketServer
{
	/// <summary>
	/// Description of Parametros.
	/// </summary>
	public class Parametros
	{
		
		List<Props> list = new List<Props>();
		
		public Parametros()
		{
		}
		
		public bool parse(String texto){
			
			string[] partes;
			bool lbSucesso=true;
			
			if(texto==null) return false;
			
			partes = texto.Split('&');
			
			foreach (string parametro in partes){
				Props lProp = new Props(parametro);
				if( getProp(lProp.nome) == null ){
					list.Add( lProp );
				}else{
					lbSucesso=false;
					Console.Write("\n*****Propriedade nula***;{0};", parametro);
					list.Clear();
					break;
				}
			}
			return lbSucesso;
		}
		
		public Props getProp(string asNome){
			Props findProp = null;
			foreach (Props prop in list){
				if( prop.nome == asNome ){
					findProp=prop;
				}
			}
			return findProp;
		}
		
		public void listProps(){
			
			foreach (Props prop in list){
				ServerServer.gravaLog( "     Prop: " + prop.nome + ":=" + prop.valor );
			}

		}
		public string getPropValor(String asNome){
			string lsValor="";
			Props lProp = getProp(asNome);
			if( lProp != null ) lsValor = lProp.valor;
			return lsValor;
		}
	}
}