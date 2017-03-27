
<!DOCTYPE html>

<head>
    <?php include 'style.php'; ?>
    <style>
    <?php include 'CSS/style.css'; ?>
    </style>
    <title>American Whore Story</title>

</head>
<body>

<div id="outer" class="container">
<div id="inner" class="container">
<h1 id="sitat"> Situation Atlas </h1>
<h1 id="h2"> American Whore Story </h1>
<div id="poem1" class="container">
<?php
function append(array $a, int $b): string {
      $len = count($a);
      $ran = rand(0,$len-1);
      $str;
      if ($b == 0) {
        $str = $a[$ran] . " ";
      } elseif ($b == 1) {
        $str = $a[$ran] . "<br>";
      } else {
        print "<br> Error in function append <br>";
      }
      return $str;
}

function makepoem(): string {
 define('l1',['I am']);
 define('l2',['bitchslapping a baby','really emotional right now','beating up your tits','putting out','sucking a cock','whoring all over town','offering BJs','smashing your ugly ho face','stealing your boyfriend'
 ,'hitting on your dad','kicking a homless guy','totally freaking out','shoplifting handbags','stealing your favorite shoes','on a brand new diet','telling you to shut your whore mouth'
 ,'like .. so done with guys','like the hottest girl ever','in love with all black guys','really into worldpeace','I need to marry someone rich'
 ,'feeling really bad for ugly people','like .. the nicest person ever']);

 define('l3',['because']);

 define('l4',['I can totally relate to Paris Hilton','I will never be skinny enough for modelling','everyone is totally jealous of me','I can have any boyfriend I want'
 ,'I am sick of looking at your whore face','everything you say is really embarrassing','you make me sick','you dont even know how to twerk'
 ,'I am a total slut','I am a manipulative whore','you are totally trying to copy me','I want you to stay like.. really far away from me','your looks are like.. dated','yo boyfriend is way ugly'
 ,'I see the way you envy me','nobody even likes you','everybody laughs at you behind your back','personally, I do not like your face','you smell like vagina','I really want to be famous','you are totally desperate'
 ,'I do not like lying whores','your asshole is probably really gross','I am really into politics','I am a feminist','your makeup is embarrassing','I think I am half black'
 ,'I hate retarded people','I know someone who is black','I do not feel sorry for your ugly ass life','I do not associate with losers','racism is really bad'
 ,'I am like.. almost a model','everybody tells me that I am pretty','I am totally into astrology','I am kind of psychic']);

 define('l5',['Also I']);

 define('l6',['want to be like','really look up to','am BFF with','dont even care about','really hate','cant even look at','honestly dont appreciate','really envy'
 ,'am on the same level as','have the same tattoo as','am just as pretty as']);
 define('l7',['Paris Hilton','Carrie from Sex and the City','Samantha from Sex and the City','Kim Kardashian','Oprah','Emma Watson','Lindsay Lohan','Beyonce','Kate Moss','Nicky Minaj','Tyra Banks','Kristen Steward','Michelle Obama'
 ,'Tila Tiquila','Kendall Jenner','Iggy Azalea','Fergie','Taylor Swift','Naomi Cambell','Katy Perry','Rihanna']);
 define('l8',['Therefore, I']);
 define('l9',['deleted her from my instagram','send her boyfriend a message on twitter','bought the same pair of jeans that she has','wrote a really nice song about her','made a fake facebook profile pretending to be her'
 ,'want her to see my whore attitude','like.. try to observe her a lot','think I deserve everything that she has','need her to see my true personaly','sleep with someone she has slept with'
 ,'need find out where she lives']);
 define('l10',['Just to']);
 define('l11',['prove a point','show that I dont even care','make it clear that no ugly bitch can fuck with me','punish her stupid vagina','be a really nice person'
 ,'meet her boyfriend','take everything away from her','end her backstapping whore lifestyle','make her beg me for my friendship','make her stop being such a stupid whore'
 ,'make her stop being such a whiny little slut','show her who is in charge of her silly whore life','make her embarrasing face glance with jealousy'
 ,'make her really insecure','seriously make her shut the fuck up']);
 echo "<br>";
 $str = append(l1,0);
 $str .= append(l2,1);
 $str .= append(l3,0);
 $str .= append(l4,1);
 $str .= append(l5,0);
 $str .= append(l6,0);
 $str .= append(l7,1);
 $str .= append(l8,0);
 $str .= append(l9,1);
 $str .= append(l10,0);
 $str .= append(l11,1);
 print($str);
 return($str);
} 
makepoem();
?>

</div>
<br>
<br>
<div id="b2">
    <form action="" method="post">
        <button type="submit" id="t1" name ="sub" value="call">More slutty wisdom</buttom>
    </form>
</div>
<br>
<br>
<div id= "b3">
    <form action=" http://situationatlas.com/index.php/" method="post">
        <button type="submit" id="t1" name ="home" value="call">Go back to Home</buttom>
    </form>
</div>



</div>
</div>

</body>
</html>
