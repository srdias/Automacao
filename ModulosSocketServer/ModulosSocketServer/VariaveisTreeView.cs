/*
 * Created by SharpDevelop.
 * User: Adriano
 * Date: 14/07/2013
 * Time: 18:53
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ModulosSocketServer
{
	/// <summary>
	/// Description of VariaveisTreeView.
	/// </summary>
	public class VariaveisTreeView
	{
		
		const int NO_MODULO = 1;
		const int NO_INDICE = 2;
		const int NO_VARIAVEL = 3;
		
		public MainForm iMainForm;
		
		public TreeView tv;
		public VariaveisTreeView(TreeView aTv, MainForm aMainForm)
		{
			tv=aTv;
			iMainForm = aMainForm;
		}
		
		public TreeNode findTreeNodeByVariavel(Variavel aVariavel){
			
			TreeNode ltn = null;
			if( tv.Nodes.Count == 0 ) return ltn;
			
			ltn=tv.Nodes[0];
			if(ltn != null ) ltn = findTreeNode( ltn, aVariavel.getNomeModuloTV(), NO_MODULO );
			if(ltn != null ) ltn = findTreeNode( ltn, aVariavel.getNomeIndiceTV(), NO_INDICE );
			if(ltn != null ) ltn = findTreeNode( ltn, aVariavel.getNomeVariavelForTV(), NO_VARIAVEL );
			
			return ltn;
			
		}
		
		public Variavel getVariavelByTreeNode(TreeNode atn){
			
			Variavel lVariavel=null;
			
			if(atn==null) return lVariavel;
			
			lVariavel = new Variavel();
			lVariavel.setCodigoModuloByTexto(atn.Parent.Parent.Text);
			lVariavel.setCodigoIndiceByTexto(atn.Parent.Text);
			lVariavel.setCodigoVariavelByTexto(atn.Text);
			lVariavel.setValorByTexto(atn.Text);
			lVariavel.setValorAlteradoByTexto(atn.Text);
			return lVariavel;
		}
		
		public Variavel getVariavelByTreeNode(){
			return getVariavelByTreeNode(tv.SelectedNode);
		}

		public Variavel getVariavelTV(Variavel aVariavel){
			TreeNode ltn = findTreeNodeByVariavel(aVariavel);
			Variavel lVariavel = getVariavelByTreeNode(ltn);
			return lVariavel;
		}
		
		public void atualizaValorVariavel(Variavel aVariavel){
			
			Variavel lVariavel=getVariavelTV(aVariavel);
			if(lVariavel!=null){
				int salvaValor=aVariavel.valor;
				aVariavel = lVariavel;
				aVariavel.valor=salvaValor;
			}
			atualizaVariavel(aVariavel);
		}
		
		public void atualizaVariavel(Variavel aVariavel){
			
			TreeNode ltn;

			if( tv.Nodes.Count == 0 ){
				ltn = tv.Nodes.Add( "Variáveis" );
			}else{
				ltn = tv.Nodes[0];
			}
			
			ltn = insertItem( ltn, aVariavel.getNomeModuloTV(), NO_MODULO );
			ltn = insertItem( ltn, aVariavel.getNomeIndiceTV(), NO_INDICE );
			ltn = insertItem( ltn, aVariavel.getNomeVariavelForTV(), NO_VARIAVEL );
			
			String lsNomeModulo=aVariavel.getNomeModulo();
			String lsNomeVariavel=aVariavel.getNomeVariavel();
			if( lsNomeModulo.Equals("02-TIPO_MODULO_CLIMA") ){
				if(lsNomeVariavel.Equals("00-CLIMA_UMIDADE")){
					iMainForm.displayProp("Umidade", aVariavel.getValorTV() );
				}else if(lsNomeVariavel.Equals("01-CLIMA_TEMPERATURA")){
					iMainForm.displayProp("Temperatura", aVariavel.getValorTV() );
				}else if(lsNomeVariavel.Equals("02-CLIMA_LUMINOSIDADE")){
					iMainForm.displayProp("ldr", aVariavel.getValorTV() );
				}
			}

		}
		
		public TreeNode insertItem(TreeNode atnPai, String asItem, int aTipo){
			TreeNode ltn;
			
			ltn = findTreeNode(atnPai,asItem,aTipo);
			
			if(ltn==null){
				ltn = atnPai.Nodes.Add( asItem );
			}else{
				if(ltn.Text!=asItem) ltn.Text=asItem;
			}
			
			return ltn;
		}
		
		public TreeNode findTreeNode(TreeNode atnFind, String asItem, int aTipo){
			
			TreeNode ltnLocalizada=null;
			String lsCodigoNode=getIdNode(aTipo,asItem);
			
			if(atnFind==null) return ltnLocalizada;
			
			for (int i = 0; i < atnFind.Nodes.Count; i++){
				String lsNomeItem=getIdNode(aTipo,atnFind.Nodes[i].Text);
				if(lsCodigoNode.Equals(lsNomeItem)){
					ltnLocalizada=atnFind.Nodes[i];
					break;
				}
			}
			return ltnLocalizada;
		}
		
		public string getIdNode(int tipo, string texto){
			string lsRetorno="";
			
			switch(tipo){
					case NO_INDICE: lsRetorno=Variavel.getCodigoIndiceByTexto(texto); break;
					case NO_MODULO: lsRetorno=Variavel.getCodigoModuloByTexto(texto); break;
					case NO_VARIAVEL: lsRetorno=Variavel.getCodigoVariavelByTexto(texto); break;
			}
			
			return lsRetorno;
		}
		

		public void percorrerNodes(TreeNode atn, int aiNivel, List<Variavel> aVariaveisList){
			
			for (int i = 0; i < atn.Nodes.Count; i++){
				if(aiNivel==2){
					aVariaveisList.Add(getVariavelByTreeNode(atn.Nodes[i]));
				}else{
					percorrerNodes(atn.Nodes[i],aiNivel+1,aVariaveisList);
				}
			}
		}
		
		public void nodesVariaveisModificadas(List<Variavel> aVariaveisList){
			
			List<Variavel> variaveisList =  new List<Variavel>();
			percorrerNodes(tv.Nodes[0],0,variaveisList);
			
			for(int i=0;i<variaveisList.Count;i++){
				if( variaveisList[i].modificada ){
					aVariaveisList.Add(variaveisList[i]);
				}
			}

		}
	}

}

