/*
 * Criado por SharpDevelop.
 * Usuário: Adriano
 * Data: 22/12/2012
 * Hora: 14:28
 * 
 * Para alterar este modelo use Ferramentas | Opções | Codificação | Editar Cabeçalhos Padrão.
 */
using System;

namespace ModulosSocketServer
{
	public class Props{
		

		public String nome=null;
		public String valor=null;

		public Props(string textoProps){
			
			if(textoProps==null) return;
			
			string[] partes = textoProps.Split('=');
			
			if( partes.Length == 2 ){
				this.nome = partes[0];
				this.valor = partes[1];
			}
			
			//Console.Write( "\nProps {0}:={1}", this.nome, this.valor );
			
		}
		
	}
}
