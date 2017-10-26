
<?php
/*switch ($_SERVER["REQUEST_METHOD"]) {
    case 'GET':
        $aNum = $_GET['num1'];
        $bNum = $_GET['num2'];
        $oPpa = $_GET['opr'];
        break;
    case 'POST':
        $aNum = $_POST['num1'];
        $bNum = $_POST['num2'];
        $oPpa = $_POST['opr'];
        break;
}*/

$aNum = $_REQUEST['num1'];
$bNum = $_REQUEST['num2'];
$oPpa = $_REQUEST['opr'];

$res = null;
switch ($oPpa) {
    case "-":
        $res = $aNum - $bNum;
        break;
    case "plus":
        $res = $aNum + $bNum;
        break;
    case "*":
        $res = $aNum * $bNum;
        break;
    case "/":
        if ($bNum == 0) {
            $res = "Err. Division by zero";
        }else{
            $res = $aNum / $bNum;
        }
        break;
    default:
        $res = "err";
        break;
}
echo $res;
?>