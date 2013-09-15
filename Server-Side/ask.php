<?php

require 'meliLV.php';
$meli = new Meli('4846584340867949', 'Z06tlQjaD8d00ZKoHJAw7i3JJWU9xXUd', $_SESSION['access_token'], $_SESSION['refresh_token']);

if($_GET['at'] || $_SESSION['access_token']) {
	if ($_GET['at']) {
		$_SESSION['access_token']= $_GET['at'];
	}

	if($_GET['q'] && $_GET["i"]){
		echo "entre";
		$question = array(
			"text" => "".$_GET['q'],
			"item_id" => "".$_GET['i']
			
		);
	}

	// We call the post request to list a item
	echo '<pre>';
	print_r($meli->post('/questions', $question, array('access_token' => $_SESSION['access_token'])));
	echo '</pre>';
} else {
	echo '<a href="' . $meli->getAuthUrl('http://prueba.casanovam.com/MercadoSocial/ask.php') . '">Login using MercadoLibre oAuth 2.0</a>';
}


?>