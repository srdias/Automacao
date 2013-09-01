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

function loadContent(parametro) {
	var endereco="menuAction.php?parametro=" + parametro;
	$("#DivMenu").load(endereco);
}

function loadAcao(parametro) {
	var endereco="menuActionAcao.php?parametro=" + parametro;
	$("#DivAcao").load(endereco);
}

function setTexto(texto) {
	$("#caixaTexto").html(texto);
}

</script>

<style type="text/css" >

</style>

</head>
<body onload="loadContent('inicio')">

<div id="DivMenu"></div>

</body>
</html>