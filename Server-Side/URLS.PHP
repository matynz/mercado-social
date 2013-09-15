<?php $url=$_GET['url'];
$html=file_get_contents($url);

$c = curl_init($url);
curl_setopt($c, CURLOPT_RETURNTRANSFER, true);

$html = curl_exec($c);

if (curl_error($c))
    die(curl_error($c));

$status = curl_getinfo($c, CURLINFO_HTTP_CODE);

curl_close($c);

echo $html;

$indice = strrpos($html,"Publicaciones");

if($indice === false)
{
}
else
{
echo $indice;
}


?>