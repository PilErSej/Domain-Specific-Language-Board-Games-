
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
   define('l1', ["I am"]);
   define('l2', ["feeling","fucking up my life","sleeping","hating om everybody","bringing darkness to my neighborhood","closing the door to my heart","feeling so fucking special","walking this world alone","kissing this rose of death"
   ,"sitting alone in a corner","killing time","leaving this world","stabbing kittens","crying","drowning","suffocating","kneeling","hoping for death","choking","sinking","ripping my chest"]);
   define('l3', ["like a"]);
   define('l4', ["pleasant","sencere","bittersweet","beasty","gruesome","slender","grim","depressed","careless","manipulative","obidient","burried","pointless","fallen","messed up","corrupt","hollow","horrific","broken","bloody","desperate","lying","joyless","dying","backstabbing","cowardly","lonely","retarded","lame","fake","dark","fascist","sulking","subbing"]);

   define('l6',["Degusted by","Embracing","Praying for","Smashed under","Thrown over","Trapped inside","Left outside","Floating towards","Hiding behind","Dragged into"]);
   define('l7',["the"]);
   define('l5', ["symptom of cancer","choice of drugoverdose","embrace","chain reaction","fucking poser","slut","sellout","sinner","whore","pit of doom","chaos","dark sky","ocean of broken dreams","sea of corpses","crocodile tear","kiss made of spikes","teardrop","shithole","corpse","death angel","parasite","demise","parasite"]);
   define('l8',["judgemental","anorexic","twisted","greedy","self absorbed","deadly","embarrassing","ridiculous","sinful","hateful","destructive","pathetic","conformist","imperfect","perfect","lifeless","silent","blind","ignorant","stupid","fucking"]);
   define('l9', ["lies","hatred","screams","longing","disgust","betrayal","despair","fear","sins","love","ignorance","abnormality","horror","terror","loss","murder","beauty","dishonesty"]);
   define('l10', ["of my"]);
   define('l11', ["nemesis","suicidal neck","unholy bodyscent","choir of sorrows","mind fortress","so called friends","ignorant classmates","black cup of coffee","sliced wrists","mom","parents","diary","room","own living hell","own emotions","emotional abyss","life in darkness","pale skin","ugly face","eternal pain"]);
   //
   define('l12', ["My"]);
   define('l13', ["eyes are","eyelids are","veins are","hands are","nails are","uniqueness","feelings are", "joy of life is","good intensions are","pityful social games are"]);
   define('l14', ["ruined by","teared up by","drowned in","poisoned with","struck by","staged by","killed by"]);
   define('l15', ["the virus that is humanity","agony","apathy","dead flowers","what does not seem to be any of your business","an outbrake of stupid fakers","decay","a black widow","my own fucking emotions","baby birds","crusified souls","nazi authorities","inivitable death","miserable faces","satans horses","the coma my life is"]);
   //
   define('l16', ["I"]);
   define('l17', ["will penetrate you like","will kiss the reaper with","will strangulate my only love,","will scream silently at","show you the reality of","will be raped by","will hang myself with","will commit the perfect suicide along with","will bring disgrace to","will spread rabies to","will never forgive","will feel forever torn, in love with","will never forget","will forever kill","will forever punish","will tear apart","cry for","will bury","visit the graveyard with","cut myself for being in love with"]);
   define('l18', ["a dead stuffed animal","these mindless sheeps","the bacteria of conformism","the attention whore that is my mother","the infection that is society","the white dove","the perfect person","the dead flowers","the full moon","the innocent child","the fly on the wall","this longsome decay of humanity","my low self esteem","the reincarnated imbodyment of pain","the lifeless crowd of annoying yuppies","the imbodyment of materialism"]);
   define('l19', ["Karma is a fucking joke","So fucking typical","Fuck you, you priviliged doll","God is obviously unholy","Time well spend, gosh","Diary sometimes you are the only one who gets me","Why even bother trying","This cosmic bitch can suck my dick","Seriousleeeh..","Sadness is an unfortunate destiny","Roses are red, but I will forever be blue","Life is a cosmic joke","Destiny is a tease","Save my soul from this darkness","Just call me sick and tired...","Promises are ment to be broken","This rotten world is going to hell","If you love me just let me be..","Lonelyness is my sancturary","I tried to be nice, but I don't care anymore","This so called 'life' makes me laugh.. hah haaah..","I might scare you, but you don't even know me","So freaking ironic..","Stop pretending.. We're not friends..","You losers don't even know me..","You know my face but you don't know my story","Im a mystery.. Not because I want to be. Because I have to be.","I am the ruler of my own world..","You are not the boss of me","You don't get me at all","Gosh.. Scenario.","Why make sense of something that is not even there.."]);
   echo "<br>";

   $str = append(l1,0);
   $str .= append(l2,1);
   $str .= append(l3,0);
   $str .= append(l4,0);
   $str .= append(l5,1);
   $str .= append(l6,0);
   $str .= append(l7,0);
   $str .= append(l8,0);
   $str .= append(l9,0);
   $str .= append(l10,0);
   $str .= append(l11,1);
   $str .= append(l12,0);
   $str .= append(l13,1);
   $str .= append(l14,0);
   $str .= append(l15,1);
   $str .= append(l16,0);
   $str .= append(l17,0);
   $str .= append(l18,1);
   $str .= append(l19,1);
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
        <button type="submit" id="t1" name ="sub" value="call">More dark wisdom</buttom>
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
