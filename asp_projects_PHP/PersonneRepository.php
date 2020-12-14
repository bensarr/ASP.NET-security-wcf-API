<?php
require_once 'db.php';

    //echo json_encode(listePersonne()->fetchAll());

    function addPersonne($nom,$prenom,$email,$tel,$dateNaissance)
    {
        $sql ="INSERT INTO `personne` VALUES(Null,'$nom','$prenom','$email','$tel','$dateNaissance')";
        $conn = getConnection();
        $conn->exec($sql);
    }
    
    function updatePersonnet($id,$nom,$prenom,$email,$tel,$dateNaissance){
        $sql ="UPDATE  personne SET nom =$nom, prenom =$prenom,email=$email,tel=$tel,dateNaissance=$dateNaissance WHERE id=$id)";
        $conn = getConnection();
        return $conn->exec($sql);
    }
    function deletePersonne($id){
        $sql="DELETE FROM personne where id=$id";
        $conn = getConnection();
        return $conn->exec($sql);
    }
    function listePersonne(){
        $sql ="SELECT * from personne";
        $conn = getConnection();
        return $conn->query($sql);
    }
    function onePersonne($id){
        $sql ="SELECT * from personne where id=$id";
        $conn = getConnection();
        return $conn->query($sql);
    }
?>

