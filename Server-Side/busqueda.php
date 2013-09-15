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

if(isset($_REQUEST['offset'])):
	$paging = $_REQUEST['offset'];
endif;


if(isset($_REQUEST['price'])):
	$limite = $_REQUEST['price'];
endif;

if(isset($_REQUEST['category'])):
	$categoria = $_REQUEST['category'];
endif;


if(isset($_REQUEST['q'])):
	$query = $_REQUEST['q'];
	
	if(isset($_REQUEST['price']) && isset($_REQUEST['category']))
        {
	  $search = $meli->get('/sites/#{siteId}/search', array(
		'q' => $query,
		'offset' => $paging,
 		'price' => $limite,
		'category' => $categoria)
	  );
	}
	else if (isset($_REQUEST['price']))
	{
		$search = $meli->get('/sites/#{siteId}/search', array(
		'q' => $query,
		'offset' => $paging,
 		'price' => $limite)
	  );
	}
	else
	{
	  $search = $meli->get('/sites/#{siteId}/search', array(
		'q' => $query,
		'offset' => $paging)
	  );
	}

	$search = $search['json'];
	$currenciesJSON = $meli->get('/currencies');
  	$currenciesJSON = $currenciesJSON["json"];
  	$currencies = array();

  	foreach ($currenciesJSON as &$currency):
  		$currencies[$currency["id"]] = $currency;
  	endforeach;
endif;

function add_or_change_parameter($parameter, $value)
{
	$params = array();
	$output = "?";
	$firstRun = true;

	foreach($_GET as $key=>$val):
   		if($key != $parameter):
    		if(!$firstRun):
    			$output .= "&";
    		else:
     			$firstRun = false;
    		endif;

    		$output .= $key."=".urlencode($val);

    	endif;
	endforeach;

	if(!$firstRun)
   		$output .= "&";

   	$output .= $parameter."=".urlencode($value);

   	return htmlentities($output);
}

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

    		echo  "@cantidad1@".($search['paging']['offset'] + 1) ."@cierre1@";
    		echo  "@cantidad2@".($search['paging']['offset']+$search['paging']['limit'])."@cierre2@";
    		echo  "@cantidada@".$search['paging']['total']."@cierrea@";
    		echo "@cantpag@".ceil($search['paging']['total']/$search['paging']['limit'])."@cierre3@";

		if ($search['paging']['offset'] > 0):
			echo "@paganterior@http://adondevoy.zz.mu/mercadolibre/busqueda.php". add_or_change_parameter('offset', max(0,$search['paging']['offset']-$search['paging']['limit'])) ."@cierre4@";
		endif;
		if ($search['paging']['offset'] + $search['paging']['limit'] < $search['paging']['total']):
			echo "@pagsiguiente@http://adondevoy.zz.mu/mercadolibre/busqueda.php".add_or_change_parameter('offset', $search['paging']['offset']+$search['paging']['limit']) ."@cierre5@";
		endif;

            echo '<ol style="counter-reset: item ' . ($search['paging']['offset']) . '">';

		foreach ($search['results'] as &$searchItem){
		   echo "@articulo@";
                   echo "@id@".$searchItem['id']."@cierree@";
		   echo "@titulo@".$searchItem['title']."@cierret@";
		   echo "@img@".$searchItem['thumbnail']."@cierrei@";
		   echo "@url@".$searchItem['permalink']."@cierreu@";
		   echo "@precio@".$currencies[$searchItem['currency_id']]['symbol'].number_format( $searchItem['price'] , $currencies[$searchItem['currency_id']]['decimal_places'] ) ."@cierrep@";
		}
	?>
</body>
</html>