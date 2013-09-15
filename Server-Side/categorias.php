<?php

require 'meliLV.php';
$meli = new Meli('4846584340867949', 'Z06tlQjaD8d00ZKoHJAw7i3JJWU9xXUd');
if(isset($_REQUEST['c'])):
	$cat = $_REQUEST['c'];
endif;
?>
<!doctype html>
<html>
<head>
<?php Header('Content-Type: text/html; charset=utf-8');?>
<style type="text/css">
LI { display: block }
LI:before {
content: counter(item) ". ";
counter-increment: item;
}
</style>
</head>
<body>
	<?php
	if (isset($cat)) {
		
		$categorias = $meli->get('/categories/'.$cat);
		foreach ($categorias["body"]->{'children_categories'} as $key => $value) {
			echo "@categoria@";
			echo "@id@".$value->{'id'}."@fid";
			echo "@nombre@".$value->{'name'}."@fnombre@";
			echo "@fcategoria@";
		}
	}else{

		$categorias = $meli->get('/sites/MLA/categories');
		foreach ($categorias['body'] as $key => $value) {
			echo "@categoria@";
			echo "@id@".$value->{'id'}."@fid";
			echo "@nombre@".$value->{'name'}."@fnombre@";
			echo "@fcategoria@";
		}
	}
	?>
</body>
</html>