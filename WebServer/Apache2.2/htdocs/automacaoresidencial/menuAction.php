	
<?php

if (!empty($_GET['parametro'])){
	$parametro=$_GET['parametro'];
}else{
	$parametro='inicio';
}

if($parametro=='inicio') $parametro=$parametro . ';casa';

function setOpcao(&$opcoes,$sessao,$nome,$codigo){
	$opcoes[$sessao][$codigo][0] = $nome;
	$opcoes[$sessao][$codigo][1] = $codigo;
}

function acaoLink($parametro,$id_menu){
	if( substr($id_menu,0,6) == "_acao_" ){
		$retorno = "loadAcao('$id_menu')";
	}else{
		$retorno = "loadContent('$parametro;$id_menu')";
	}
	return $retorno;
}

function myLink($descricao,$opcoes_menu,$parametro_retorno){
	return "<a class=\"linkCaminhoMenu\" href=\"#\" onclick=\"loadContent('$parametro_retorno')\"> $descricao</a>";
}

function myButtonControleRemoto($texto,$acao){
	echo "<td class=\"controle\" onclick=\"" . acaoLink($parametro,$acao) . ";\">$texto</td>";
}

$opcoes = array();

setOpcao($opcoes,"inicio","Casa","casa");

setOpcao($opcoes,"casa","Cozinha","c1");
setOpcao($opcoes,"casa","Quarto Casal","q1");
setOpcao($opcoes,"casa","Quarto Diana","q2");
setOpcao($opcoes,"casa","Quarto Lucas","q3");
setOpcao($opcoes,"casa","Banheiro Social","b1");
setOpcao($opcoes,"casa","Banheiro Suite Casal","b2");

setOpcao($opcoes,"c1","Lampadas Setor 1","_acao_c1-lampadas-s1");
setOpcao($opcoes,"c1","Lampadas Setor 2","_acao_c1-lampadas-s2");
setOpcao($opcoes,"c1","Micro ondas","_acao_c1-micro-ondas");
setOpcao($opcoes,"c1","TV","_controle_tv_philco");

setOpcao($opcoes,"q1","Lampadas","_acao_rele_q1-lampadas");
setOpcao($opcoes,"q1","Ar condicionado","_acao_q1-ar-condicionado");
setOpcao($opcoes,"q1","TV","_acao_q1-tv");

setOpcao($opcoes,"q2","Google","_url_teste");

/*
echo "Teste<br/>";
echo "Parametro: '$parametro'<br/>";
echo "Parametro retorno: '$parametro_retorno'<br/>";
echo "Menu: $menu, Indice: $indice<br/>";
echo "Menu: $menu_antes, Indice: $indice_antes<br/>";
*/

$partes_parametro = explode (";",$parametro);

$ultimo=count($partes_parametro);

$menu=$partes_parametro[$ultimo -1];
$menu_antes=$partes_parametro[$ultimo -2];

$parametro_retorno="";
$anterior="";
$caminho="";
for($i=0;$i<$ultimo -1;$i++){

	if($i>0) $parametro_retorno=$parametro_retorno . ";";
	$parametro_retorno=$parametro_retorno . $partes_parametro[$i];
	
	$menu_item_1=$partes_parametro[$anterior];
	$menu_item_2=$partes_parametro[$i];
	
	if($caminho_link!="") $caminho_link = $caminho_link . ";"; 
	$caminho_link=$caminho_link . $partes_parametro[$i];

	if($i>0){
		$descricao_opcao=$opcoes[$menu_item_1][$menu_item_2][0];
		if($caminho!="") $descricao_opcao = "\\" . $descricao_opcao; 
		$caminho=$caminho . myLink( $descricao_opcao, 
									$opcoes[$menu_item_1][$menu_item_2][1], 
									$caminho_link );
	}
	$anterior=$i;
};

$titulo = $opcoes[$menu_antes][$menu][0];

?>

<table>
<tr> 
	<td class="opcaoTitulo" >
		<table border=1 width="100%" >
			<tr>
				<td class="opcaoTitulo" width="3%" >
					<img src="seta.png" width="40" height="40" onclick="loadContent(<?php echo "'$parametro_retorno'"; ?>);">
				</td>
				<td class="opcaoTitulo" width="94%" >
					<?php echo "$titulo<br>$caminho" ?>
					<div id="DivAcao"></div>
				</td>
				<td class="opcaoTitulo" width="3%" >
				</td>
			</tr>
		</table>
	</td>
</tr>


<?php
//echo "acaorele:" . substr($menu,0,10) . ";";
if( substr($menu,0,10) == "_acao_rele" ){
	echo "teste da acao do rele";
}else if( $menu == "_url_teste" ){

	$url = file_get_contents('http://www.google.com');

	echo $url;
	
}else if( $menu != "_controle_tv_philco" ){

	foreach ($opcoes[$menu] as $i){
		$descricao=$i[0];
		$id_menu=$i[1];
		$acao=acaoLink($parametro,$id_menu);
		echo "<tr> <td class=\"opcao\" onclick=\"$acao;\"> $descricao </td> </tr>";
	}
}else{
?>

<tr> 
	<td>
		<table class="controle" >
			<tr>
				<?php myButtonControleRemoto("1","_acao_um");
				      myButtonControleRemoto("2","_acao_dois");
				      myButtonControleRemoto("3","_acao_tres");
				?>
			</tr>
			<tr>
				<?php myButtonControleRemoto("4","_acao_quatro");
				      myButtonControleRemoto("5","_acao_cinco");
				      myButtonControleRemoto("6","_acao_seis");
				?>
			</tr>
			<tr>
				<?php myButtonControleRemoto("7","_acao_sete");
				      myButtonControleRemoto("8","_acao_oito");
				      myButtonControleRemoto("9","_acao_nove");
				?>
			</tr>
			<tr>
				<?php myButtonControleRemoto("Mudo","_acao_mudo");
				      myButtonControleRemoto("0","_acao_zero");
				      myButtonControleRemoto("Voltar","_acao_voltar");
				?>
			</tr>
			<tr>
				<?php myButtonControleRemoto("Vol +","_acao_volume_mais");
				      myButtonControleRemoto("","_acao_nada");
				      myButtonControleRemoto("Canal +","_acao_canal_mais");
				?>
			</tr>
			<tr>
				<?php myButtonControleRemoto("Vol -","_acao_volume_menos");
				      myButtonControleRemoto("","_acao_nada");
				      myButtonControleRemoto("Canal -","_acao_canal_menos");
				?>
			</tr>
		</table>
	</td>
</tr>

<?php
}
?>

</table>



