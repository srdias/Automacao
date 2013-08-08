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

namespace ModulosSocketServer
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
		
		public int calcValorAsc(int valor){
			int liBase;
			int adicionar=0;
			if(valor<48){
				liBase=0;
			}else if(valor<65){
				liBase=48;
			}else if(valor<97){
				liBase=65;
				adicionar=10;
			}else{
				liBase=97;
			}
			valor = valor - liBase + adicionar;
			return valor;
		}
		
		public int calcAsc(String str, int pos){
			char lc = Convert.ToChar(str.Substring(pos,1));
			return (int)lc;
		}
		
		public List<int> parseBase32Partes(String texto){
			List<int> partes = new List<int>();
			int valor1;
			int valor2;
			int valor;
			
			for(int i=0; i<texto.Length;i++){
				valor1=calcAsc(texto,i);
				if(valor1<97){
					valor2=calcAsc(texto,++i);
				}else{
					valor2=valor1;
					valor1=0;
				}
				valor1=calcValorAsc(valor1);
				valor2=calcValorAsc(valor2);
				valor=(valor1*32)+valor2;
				partes.Add(valor);
			}
			
			return partes;
		}
		
		public bool parseBase32(String texto, VariaveisTreeView aVarTv,MainForm aMainForm){
			
			String modulosAdd;
			if(texto==null) return false;
			
			//aMainForm.gravaLog(texto);
			
			String[] partes = texto.Split('|');

			foreach (string modulos in partes){
				if(modulos.Substring(0,3).Equals( "[v:" ) ){
					modulosAdd = modulos.Substring(3);
				}else if(modulos.Substring(0,2).Equals( "v:" ) ){
					modulosAdd = modulos.Substring(2);
				}else if(modulos.Substring(0,1).Equals( "]" )) {
					break;
				}else{
					modulosAdd = modulos;
				}
				
				List<int> lista = parseBase32Partes(modulosAdd);
				for(int i=2;i<lista.Count;i++){
					int tipoModulo=lista[0];
					int tipoSequencia=lista[1];
					int valor=lista[i];

					Variavel lVariavel = new Variavel( tipoModulo,
					                                  tipoSequencia,
					                                  i -2,
					                                  valor );
					aVarTv.atualizaValorVariavel( lVariavel );
				}
			}
			
			return true;
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
		
		public void listProps(MainForm aMainForm){
			
			foreach (Props prop in list){
				aMainForm.displayProp(prop.nome,prop.valor );
				aMainForm.gravaLog( "     Prop: " + prop.nome + ":=" + prop.valor );
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