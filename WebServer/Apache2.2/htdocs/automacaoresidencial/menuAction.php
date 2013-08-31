
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

function myLink($descricao,$opcoes_menu,$parametro_retorno){
	return "<a class=\"linkCaminhoMenu\" href=\"#\" onclick=\"loadContent('$parametro_retorno')\"> $descricao</a>";
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

setOpcao($opcoes,"q1","Lampadas","_acao_q1-lampadas");
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
				</td>
				<td class="opcaoTitulo" width="3%" >
				</td>
			</tr>
		</table>
	</td>
</tr>

<?php

if( $menu == "_url_teste" ){

	$url = file_get_contents('http://www.google.com');

	echo $url;
	
}else if( $menu != "_controle_tv_philco" ){

	foreach ($opcoes[$menu] as $i){
		$descricao=$i[0];
		$id_menu=$i[1];
		echo "<tr> <td class=\"opcao\" onclick=\"loadContent('$parametro;$id_menu');\"> $descricao </td> </tr>";
	}
}else{
?>

<tr> 
	<td>
		<table class="controle" >
			<tr>
				<td class="controle" onclick="loadContent('_acao_um');">1</td>
				<td class="controle" onclick="loadContent('_acao_dois');">2</td>
				<td class="controle" onclick="loadContent('_acao_tres');">3</td>
			</tr>
			<tr>
				<td class="controle" onclick="loadContent('_acao_quatro');">4</td>
				<td class="controle" onclick="loadContent('_acao_cinco');">5</td>
				<td class="controle" onclick="loadContent('_acao_seis');">6</td>
			</tr>
			<tr>
				<td class="controle" onclick="loadContent('_acao_sete');">7</td>
				<td class="controle" onclick="loadContent('_acao_oito');">8</td>
				<td class="controle" onclick="loadContent('_acao_nove');">9</td>
			</tr>
			<tr>
				<td class="controle" onclick="loadContent('_acao_mudo');">Mudo</td>
				<td class="controle" onclick="loadContent('_acao_zero');">0</td>
				<td class="controle" onclick="loadContent('_acao_voltar');">Voltar</td>
			</tr>
			<tr>
				<td class="controle" onclick="loadContent('_acao_volume_mais');">Vol +</td>
				<td class="controle" onclick="loadContent('_acao_nada');">x</td>
				<td class="controle" onclick="loadContent('_acao_canal_mais');">Canal +</td>
			</tr>
			<tr>
				<td class="controle" onclick="loadContent('_acao_volume_menos');">Vol -</td>
				<td class="controle" onclick="loadContent('_acao_nada');">x</td>
				<td class="controle" onclick="loadContent('_acao_canal_menos');">Canal -</td>
			</tr>
		</table>
	</td>
</tr>

<?php
}
?>

</table>



