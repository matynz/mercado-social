<?php
require 'meli.php';

$meli = new Meli(array(
	'appId'  	=> '4846584340867949',
	'secret' 	=> 'Z06tlQjaD8d00ZKoHJAw7i3JJWU9xXUd',
));

$PMSToolId = 'PMSToolId';
$paging = "";
$limite = "";
$categoria = "";


	
	$search = $meli->get('/sites/MLA/featured_items/HP');

	$search = $search['json'];


?>

<!doctype html>
<html>
<head><meta charset="UTF-8"/>
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

		foreach ($search['json']['results'] as &$searchItem){
		   echo "@articulo@";
                   echo "@id@".$searchItem['itemid']."@cierree@";
		   echo "@titulo@".$searchItem['title']."@cierret@";
		   echo "@img@".$searchItem['picture']['url']."@cierrei@";
		   echo "@url@".$searchItem['permalink']."@cierreu@";
		   //echo "@precio@".$currencies[$searchItem['currency_id']]['symbol'].number_format( $searchItem['price'] , $currencies[$searchItem['currency_id']]['decimal_places'] ) ."@cierrep@";
		}
	?>
</body>
</html>