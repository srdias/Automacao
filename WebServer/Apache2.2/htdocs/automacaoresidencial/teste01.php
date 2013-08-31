
<table>
<?php

if (!empty($_GET['parametro'])){
	$parametro=$_GET['parametro'];
}else{
	$parametro='inicio,0';
}

$partes_parametro = explode (";",$parametro);

$ultimo=count($partes_parametro) -1;

$partes = explode (",",$partes_parametro[$ultimo]);
$menu=$partes[0];
$indice=$partes[1];

$partes = explode (",",$partes_parametro[$ultimo -1]);
$menu_antes=$partes[0];
$indice_antes=$partes[1];

function setOpcao(&$opcoes,$sessao,$nome,$codigo){
	$indice_adicionar=count($opcoes[$sessao]);
	$opcoes[$sessao][$indice_adicionar][0] = $nome;
	$opcoes[$sessao][$indice_adicionar][1] = $codigo;
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

echo "Teste<br/>";
echo "Parametro: '$parametro'<br/>";
echo "Menu: $menu, Indice: $indice<br/>";
echo "Menu: $menu_antes, Indice: $indice_antes<br/>";

$titulo = $opcoes[$menu_antes][$indice][0];

?>

<tr> 
	<td class="opcaoTitulo" >
		<table border=1 width="100%" >
			<tr>
				<td class="opcaoTitulo" width="3%" >
					<img src="seta.png" width="40" height="40" onclick="loadContent('<?php echo "inicio"; ?>','Automação Residencial');">
				</td>
				<td class="opcaoTitulo" width="94%" >
					<?php echo $titulo; ?>
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

	for($i=0;$i<count($opcoes[$menu]); $i++) {
		$descricao=$opcoes[$menu][$i][0];
		$id_menu=$opcoes[$menu][$i][1];
		echo "<tr> <td class=\"opcao\" onclick=\"loadContent('$parametro;$id_menu,$i');\"> $descricao </td> </tr>";
	}
}else{
?>

<tr> 
	<td>
		<table class="controle" >
			<tr>
				<td class="controle" onclick="loadContent('_acao_um','Controle Remoto',);">1</td>
				<td class="controle" onclick="loadContent('_acao_dois','Controle Remoto');">2</td>
				<td class="controle" onclick="loadContent('_acao_tres','Controle Remoto');">3</td>
			</tr>
			<tr>
				<td class="controle" onclick="loadContent('_acao_quatro','Controle Remoto');">4</td>
				<td class="controle" onclick="loadContent('_acao_cinco','Controle Remoto');">5</td>
				<td class="controle" onclick="loadContent('_acao_seis','Controle Remoto');">6</td>
			</tr>
			<tr>
				<td class="controle" onclick="loadContent('_acao_sete','Controle Remoto');">7</td>
				<td class="controle" onclick="loadContent('_acao_oito','Controle Remoto');">8</td>
				<td class="controle" onclick="loadContent('_acao_nove','Controle Remoto');">9</td>
			</tr>
			<tr>
				<td class="controle" onclick="loadContent('_acao_mudo','Controle Remoto');">Mudo</td>
				<td class="controle" onclick="loadContent('_acao_zero','Controle Remoto');">0</td>
				<td class="controle" onclick="loadContent('_acao_voltar','Controle Remoto');">Voltar</td>
			</tr>
			<tr>
				<td class="controle" onclick="loadContent('_acao_volume_mais','Controle Remoto');">Vol +</td>
				<td class="controle" onclick="loadContent('_acao_nada','Controle Remoto');">x</td>
				<td class="controle" onclick="loadContent('_acao_canal_mais','Controle Remoto');">Canal +</td>
			</tr>
			<tr>
				<td class="controle" onclick="loadContent('_acao_volume_menos','Controle Remoto');">Vol -</td>
				<td class="controle" onclick="loadContent('_acao_nada','Controle Remoto');">x</td>
				<td class="controle" onclick="loadContent('_acao_canal_menos','Controle Remoto');">Canal -</td>
			</tr>
		</table>
	</td>
</tr>

<?php
}
?>

</table>



