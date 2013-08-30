
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
	echo "<tr> <td onclick=\"setTexto('$id_menu');loadContent('?menu=$id_menu');\"> $descricao </td> </tr>";
}
?>
</table>