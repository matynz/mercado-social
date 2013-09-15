<?php
echo "hola Mundo";
require 'meliLV.php';
$meli = new Meli('4846584340867949', 'Z06tlQjaD8d00ZKoHJAw7i3JJWU9xXUd', $_SESSION['access_token'], $_SESSION['refresh_token']);

if($_GET['access_token']) {
		
		$access_token=$_GET['access_token'];
	
		$titulo = $_GET['titulo']?$_GET['titulo']: "";
		$subtitle = $_GET['subtitle']?$_GET['subtitle']: "";
		$category_id = $_GET['cat']?$_GET['']:"";
		$price =$_GET['price']?$_GET['price']:0;
		$currency_id = "ARS";
		$cuantity =$_GET['cant']?$_GET['cant']:0;
		$mode = $_GET['modo']?$_GET['modo']:"";
		$condition =$_GET['condicion']?$_GET['condicion']:"";
		$tipo= $_GET['tipo']?$_GET['tipo']:"";
		$descripcion = $_GET['descripcion']?$_GET['descripcion']:"";
		$warranty =$_GET['garantia']?$_GET['garantia']:"";

		// We construct the item to GET
		$item = array(
			"title" => $title,
			"subtitle" => $subtitle,
			"category_id" => $cat,
			"price" => $price,
			"currency_id" => "ARS",
			"available_quantity" => $cuantity,
			"buying_mode" => $mode,
			"listing_type_id" => $tipo,
			"condition" => $condicion,
			"description" => $description,
			 "warranty" => $garantia
			// "pictures" => array(
			// 	array(
			// 		"source" => "http://upload.wikimedia.org/wikipedia/commons/f/fd/Ray_Ban_Original_Wayfarer.jpg"
			// 	),
			// 	array(
			// 		"source" => "http://en.wikipedia.org/wiki/File:Teashades.gif"
			// 	)
			//)
		);

		// We call the post request to list a item
		echo '<pre>';
		echo "Item: ";
		print_r($item);
		$result = $meli->post('/items', $item, array('access_token' => $access_token));
		if($result['body']->{'message'}){
			echo "Error: ";
			print_r ($result['body']->{'message'});
			print_r ($result['body']->{'error'});

		}else{
			echo "Permalink: ";
			print_r($result['body']->{'permalink'});
		}
		echo '</pre>';
} else {
	echo "@error@@message@No se logueo Usuario@fmessage@@codigo@falta_access_token@fcodigo@@ferror@";
}


?>