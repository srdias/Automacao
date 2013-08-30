<html xmlns="http://www.w3.org/1999/xhtml>
<head>
<script type="text/javascript">
<!--
function changeURL(id){
alert(id);
newURL="http://www.terra.com.br";
document.getElementById(id).src=newURL;
}
//-->
</script>
</head>
<body>
<form name="testForm">
<iframe id="test" name="testName" src="http://www.uol.com.br"></iframe>;
</form>
<a href="#" onclick="changeURL('test’);">teste </a>
</body>
</html>