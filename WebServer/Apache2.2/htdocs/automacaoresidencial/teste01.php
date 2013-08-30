
<table>
<?php

if (!empty($_GET['menu'])){
	$menu=$_GET['menu'];
}else{
	$menu='inicio';
}

function setOpcao(&$opcoes,$sessao,$nome,$codigo){
	$indice=count($opcoes[$sessao]);
	$opcoes[$sessao][$indice][0] = $nome;
	$opcoes[$sessao][$indice][1] = $codigo;
}

$opcoes = array();
setOpcao($opcoes,"inicio","Cozinha","c1");
setOpcao($opcoes,"inicio","Quarto Casal","q1");
setOpcao($opcoes,"inicio","Quarto Diana","q2");
setOpcao($opcoes,"inicio","Quarto Lucas","q3");
setOpcao($opcoes,"inicio","Banheiro Social","b1");
setOpcao($opcoes,"inicio","Banheiro Suite Casal","b2");

setOpcao($opcoes,"c1","Lampadas Setor 1","_acao_c1-lampadas-s1");
setOpcao($opcoes,"c1","Lampadas Setor 2","_acao_c1-lampadas-s2");
setOpcao($opcoes,"c1","Micro ondas","_acao_c1-micro-ondas");
setOpcao($opcoes,"c1","TV","_controle_tv_philco");

setOpcao($opcoes,"q1","Lampadas","_acao_q1-lampadas");
setOpcao($opcoes,"q1","Ar condicionado","_acao_q1-ar-condicionado");
setOpcao($opcoes,"q1","TV","_acao_q1-tv");

$titulo="Titulo do menu";
?>

<tr> 
	<td class="opcaoTitulo" >
		<table border=1 width="100%" >
			<tr>
				<td class="opcaoTitulo" width="3%" >
					<img src="seta.png" width="40" height="40" onclick="loadContent('<?php echo "inicio"; ?>');">
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

if( $menu != "_controle_tv_philco" ){

	for($i=0;$i<count($opcoes[$menu]); $i++) {
		$descricao=$opcoes[$menu][$i][0];
		$id_menu=$opcoes[$menu][$i][1];
		echo "<tr> <td class=\"opcao\" onclick=\"loadContent('$id_menu');\"> $descricao </td> </tr>";
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



