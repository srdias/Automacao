<?php
echo phpversion();
echo "<br>";
echo $_SERVER['SERVER_NAME'];
echo "<br>";
$_SERVER['v1'] = 10;
echo $_SERVER['v1'];

if (empty($GLOBALS['v2'])){
	 ##$GLOBALS['v2']=100;
}

echo "<br>";
$GLOBALS['v2'] = $GLOBALS['v2'] +1;
echo $GLOBALS['v2'];

echo "<br>";
$_ENV['v3'] = $_ENV['v3'] +1;
echo $_ENV['v3'];


?>