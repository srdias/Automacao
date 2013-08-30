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
function loadContent(sourceUrl) {
	var endereco="teste01.php?menu=" + sourceUrl
	
	if( sourceUrl.substring(0, 6) == "_acao_"){
		alert(sourceUrl);
	}else{
		$("#DivMenu").load(endereco);
	}
}

function setTexto(texto) {
	$("#caixaTexto").html(texto);
}

$(document).ready(function() {
$("#linkAltera").click(function() {
//str="XXX"+str+" Este eh o novo texto heehe!";
loadContent('inicio')
});
});

</script>

<style type="text/css" >

</style>

</head>
<body  onload="loadContent('inicio')">


<div id="DivMenu"></div>

</body>
</html>