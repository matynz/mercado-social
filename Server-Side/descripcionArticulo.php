<?php

  $url = $_GET['url'];
  $codigo = file_get_contents($url);
  $index = strrpos($codigo, "itemDescription");
  $index = $index + 38;
  $codigo = substr($codigo, $index,strlen($codigo)-$index);

  $indice = strrpos($codigo, "submit");
  $codigo = substr($codigo, 0,$indice);

?>
<html>
  <head>
    <title>HTML source of <?php echo $url; ?></title>
  </head>
  <body>
    <pre>
<?php echo $codigo ?>
    </pre>
  </body>
</html>