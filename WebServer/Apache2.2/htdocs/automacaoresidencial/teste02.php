<?php
session_start();
$_SESSION['test'] = $_SESSION['test'] + 1;
echo $_SESSION['test'];

$url = file_get_contents('http://www.bcb.gov.br/');

echo $url;
?>