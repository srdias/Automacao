<?php
session_start();
?>

<html lang="pt-br">
<head>
	<meta http-equiv="content-type" content="text/html; charset=utf-8">
	<title>Automação residencial</title>
	
<link rel="stylesheet" href="style.css" type="text/css" media="screen">
<script type="text/javascript" src="jquery-1.js"></script>
<script type="text/javascript"  language="javascript">
function loadContent(Div, sourceUrl) {
$(Div).load(sourceUrl);
}

function setTexto(texto) {
	$("#caixaTexto").html(texto);
}

$(document).ready(function() {
$("#linkAltera").click(function() {
var str="teste";
//str="XXX"+str+" Este eh o novo texto heehe!";
if( $("#caixaTexto").html() == "SIM" ){
	$("#caixaTexto").html("NAO");
}else{
	$("#caixaTexto").html("SIM");
}
});
});

</script>

<style type="text/css" >
#container {
        border:thin blue solid;
		background-color:3399FF;
		width: 100px; 
		height: 100px; 
}

#DivMenu {
        xborder:thin blue solid;
		background-color:3399FF;
		width: 90%; 
		height: 80%; 
}

</style>

</head>
<body>

<?php

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
setOpcao($opcoes,"c1","TV","_acao_c1-tv");

setOpcao($opcoes,"q1","Lampadas","_acao_q1-lampadas");
setOpcao($opcoes,"q1","Ar condicionado","_acao_q1-ar-condicionado");
setOpcao($opcoes,"q1","TV","_acao_q1-tv");


?> 

<div id="DivMenu">
<table>
<?php

if (!empty($_GET['menu'])){
	$menu=$_GET['menu'];
}else{
	$menu='inicio';
}

for($i=0;$i<count($opcoes[$menu]); $i++) {
	$descricao=$opcoes[$menu][$i][0];
	$id_menu=$opcoes[$menu][$i][1];
	echo "<tr> <td onclick=\"setTexto('$id_menu');\"> $descricao </td> </tr>";
}
?>
</table>
</div>

<div id="container"></div>
<a id="linkAltera">Alterar conteudo</a>
<br/>
<div id="caixaTexto">Este conteudo sera alterado para outra coisa qualquer</div>

</body>
</html>