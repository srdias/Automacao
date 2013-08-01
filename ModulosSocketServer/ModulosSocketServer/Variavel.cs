/*
 * Created by SharpDevelop.
 * User: Adriano
 * Date: 16/07/2013
 * Time: 20:05
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Windows.Forms;

namespace ModulosSocketServer
{
	/// <summary>
	/// Description of Variavel.
	/// </summary>
	public class Variavel
	{
		
		public int codigoModulo;
		public int codigoIndice;
		public int codigoVariavel;
		public int valor;
		public int valorModificado;
		public bool modificada;
		
		public Variavel(){
			codigoModulo=0;
			codigoIndice=0;
			codigoVariavel=0;
			valor=0;
			valorModificado=0;
			modificada=false;
		}
		
		public Variavel(int aModulo, int aIndice, int aVariavel, int aValor){
			codigoModulo=aModulo;
			codigoIndice=aIndice;
			codigoVariavel=aVariavel;
			valor=aValor;
			valorModificado=0;
			modificada=false;
		}
		
		public string getVariavelForModulo(){
			return codigoModulo.ToString("0")+";"+
				codigoIndice.ToString("0")+";"+
				codigoVariavel.ToString("0")+";"+
				valorModificado.ToString("0")+";";
		}

		public string getNomeVariavelForTV(){
			string lsRetorno;
			
			lsRetorno = getNomeVariavel() +"="+valor.ToString("0");
			
			if( modificada ){
				if(!valor.Equals(valorModificado)){
					lsRetorno = lsRetorno + "("+valorModificado.ToString("0")+")";
				}
				lsRetorno = lsRetorno + "*";
			}
			
			return lsRetorno;
		}
		
		public string getNomeModuloTV(){
			return getNomeModulo();
		}
		
		public string getNomeIndiceTV(){
			return codigoIndice.ToString("000")+"-Indice";
		}

		public string getNomeVariavelTV(){
			return getNomeVariavel();
		}
		
		public string getValorTV(){
			return valor.ToString();
		}
		
		public string getValorAlteradoTV(){
			return valorModificado.ToString();
		}
		
		public static string getCodigoModuloByTexto(string texto){
			return texto.Substring(0,2);
		}
		
		public static string getCodigoIndiceByTexto(string texto){
			return texto.Substring(0,3);
		}
		
		public static string getCodigoVariavelByTexto(string texto){
			return texto.Substring(0,2);
		}
		
		public static string getValorByTexto(string texto){
			string lsParte;
			lsParte=getIntervaloString(texto,"=","(","*");
			return lsParte;
		}
		
		public static string getValorAlteradoByTexto(string texto){
			string lsParte;
			lsParte=getIntervaloString(texto,"(",")","");
			return lsParte;
		}
		
		public void setCodigoModuloByTexto(string texto){
			string lsCodigo=getCodigoModuloByTexto(texto);
			codigoModulo=Convert.ToInt32(lsCodigo);
		}
		
		public void setCodigoIndiceByTexto(string texto){
			string lsCodigo=getCodigoIndiceByTexto(texto);
			codigoIndice=Convert.ToInt32(lsCodigo);
		}
		
		public void setCodigoVariavelByTexto(string texto){
			string lsCodigo=getCodigoVariavelByTexto(texto);
			codigoVariavel=Convert.ToInt32(lsCodigo);
		}
		
		public void setValorByTexto(string texto){
			string lsValor=getValorByTexto(texto);
			if(lsValor.Equals("")) lsValor="0";
			try{
				valor=Convert.ToInt32(lsValor);
			}catch (FormatException) {
				MessageBox.Show("Erro convertendo '"+lsValor.GetType().Name+ "' '" +lsValor+"'.");
			}
		}
		
		public void setValorAlteradoByTexto(string texto){
			string lsValor=getValorAlteradoByTexto(texto);
			if(!lsValor.Equals("")){
				try{
					valorModificado=Convert.ToInt32(lsValor);
				}catch (FormatException) {
					MessageBox.Show("Erro convertendo '"+lsValor.GetType().Name+ "' '" +lsValor+"'.");
				}
				modificada=true;
			}else{
				modificada=false;
			}
		}
		
		public static string getIntervaloString(string texto, string inicio, string fim, string fim2){
			
			int liPosInicial=texto.IndexOf(inicio);
			int liPosFinal=texto.IndexOf(fim);
			if(liPosFinal<0) liPosFinal=texto.IndexOf(fim2);
			
			if(liPosInicial<0){
				liPosInicial=0;
			}else{
				liPosInicial++;
			}
			if(liPosFinal<0){
				liPosFinal=texto.Length;
			}
			
			String lsValor = texto.Substring(liPosInicial,liPosFinal - liPosInicial);
			
			return lsValor;
		}

		public String getNomeModulo(){
			String lsNome;
			
			switch(codigoModulo){
					case 1: lsNome="TIPO_MODULO_BANCADA"; break;
					case 2: lsNome="TIPO_MODULO_CLIMA"; break;
					case 3: lsNome="TIPO_MODULO_TEMPO"; break;
					default: lsNome="Modulo nao definico"; break;
			};
			
			lsNome = codigoModulo.ToString("00")+"-"+lsNome;
			
			return lsNome;
		}

		public String getNomeVariavel(){
			String lsNome="Variavel sem nome";
			
			
			if(codigoModulo==1){
				switch(codigoVariavel){
						case 0: lsNome="BANCADA_NIVEL_AGUA"; break;
						case 1: lsNome="BANCADA_ESTADO_REPOSICAO_AGUA"; break;
						case 2: lsNome="BANCADA_ESTADO_FLUXO_AGUA"; break;
						case 3: lsNome="BANCADA_NIVEL_AGUA_LIMITE_INFERIOR"; break;
						case 4: lsNome="BANCADA_NIVEL_AGUA_LIMITE_SUPERIOR"; break;
						case 5: lsNome="BANCADA_NIVEL_AGUA_LIMITE_ACIONAMENTO"; break;
						case 6: lsNome="BANCADA_NIVEL_AGUA_LIMITE_DESACIONAMENTO"; break;
						case 7: lsNome="BANCADA_QTDE_VARIAVEIS"; break;
				};
			}else if(codigoModulo==2){
				switch(codigoVariavel){
						case 0: lsNome="CLIMA_UMIDADE"; break;
						case 1: lsNome="CLIMA_TEMPERATURA"; break;
						case 2: lsNome="CLIMA_LUMINOSIDADE"; break;
				};
			}else{
				switch(codigoVariavel){
						case 0: lsNome="TEMPO_HOR"; break;
						case 1: lsNome="TEMPO_MIN"; break;
						case 2: lsNome="TEMPO_SEG"; break;
				};
			};
			
			lsNome = codigoVariavel.ToString("00")+"-"+lsNome;
			
			return lsNome;
		}
		
	}
}
