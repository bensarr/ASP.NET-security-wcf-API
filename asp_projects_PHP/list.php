<?php
    require_once 'PersonneRepository.php';

    echo json_encode(listePersonne()->fetchAll());
?>