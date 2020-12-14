<?php   
function getConnection(){
$host='127.0.0.1';
$mysqluser='root';
$mysqlpassword = '';
$dbname='asp_cons_api';
$dns="mysql:host=$host;dbname=$dbname";
try {
    $db=new PDO($dns,$mysqluser,$mysqlpassword);
} catch (PDOException $ex) {
 die('Error :' .$ex->getMessage());
}
return $db;
}
?>