module Program
open System.IO
open Microsoft.FSharp.Text.Lexing
open AbSyn
open System
open System.Globalization
open System.Linq
let matchAll(c : char) =
    let alpha = "ABCDEFGHIJKLMNOPQRSTUVWXYZ"
    let mutable res = 0
    for i in 0 .. (String.length alpha)-1 do
        if (alpha.[i] = c)
        then res <- i
    res

type VarDec() = class
    let mutable varlist = []
    member x.VARLIST = varlist
    member x.SetVariableAreaSeq(n : string, v : string list) =
        let mutable templist = []
        templist <- List.concat [v;templist]
        templist <- List.concat [[n];templist]
        varlist <- List.concat [[templist];varlist]
end 

type Grid() = class
    inherit VarDec()
    let mutable Mgrid = 3
    let mutable Ngrid = 3
    let mutable placepos = (0,0)
    let mutable movepos = []
    
    let mutable (fillX : _ list) = []
    let mutable (fillO : _ list) = []
    let mutable (sleepX : _ list) = []
    let mutable (sleepO : _ list) = []
    let mutable (limitX : _ list) = []
    let mutable (limitO : _ list) = []
    let mutable board = Array2D.init Mgrid Ngrid (fun m n -> ' ')
    let mutable sleeparea = false
    let mutable limitarea = false
    member x.SLEEPAREA = sleeparea
    member x.LIMITAREA = limitarea
    member x.MGRID = Mgrid
    member x.NGRID = Ngrid
    member x.BOARD = board
    member x.FILLX = fillX
    member x.FILLO = fillO
    member x.SLEEPX = sleepX
    member x.SLEEPO = sleepO
    member x.LIMITX = limitX
    member x.LIMITO = limitO
    member x.PLACEPOS = placepos
    member x.MOVEPOS = movepos
    member x.SetGrid(M : int, N : int) =
        Mgrid <- M
        Ngrid <- N
        board <- Array2D.init Ngrid Mgrid (fun m n -> ' ')
    member x.Mark(str : string)= 
        let mutable A = str.[0]
        let mutable B = ""
        match str.Length with
        | 2 -> B <- str.[1] |> string
        | 3 -> B <- ((str.[1] |> string) + (str.[2] |> string))
        | _ -> printfn "Error with Mark."   
        let X = matchAll(A)
        let Y = B |> int
        (Y-1,X)   
    member x.SetPlacePiece(str : string) = 
        placepos <- x.Mark(str)
    member x.GameMarkCell(player : char) = 
        x.BOARD.[fst x.PLACEPOS,snd x.PLACEPOS] <- player
    member x.SetMovePiece(pos1 : String, pos2 : string) =
        let X1, Y1 = x.Mark(pos1)
        let X2, Y2 = x.Mark(pos2)
        movepos <- [(X1,Y1);(X2,Y2)]
    member x.GameMovePiece(player : char) = 
        x.BOARD.[fst movepos.[1], snd movepos.[1]] <- player
        x.BOARD.[fst movepos.[0], snd movepos.[0]] <- ' '
    member x.MarkAreaSeq(player : Char,name : String) = 
        for k in 0..x.VARLIST.Length-1 do
            if (x.VARLIST.[k].[0] = name)
                then 
                    for l in 1.. x.VARLIST.[k].Length-1 do
                        let pos = x.VARLIST.[k].[l]
                        let pos1 = x.Mark(pos.Split('-').[0])
                        let pos2 = x.Mark(pos.Split('-').[1])
                        let mutable a,b = (fst pos1, snd pos1)
                        let mutable x,y = (fst pos2, snd pos2)
                        let N = Ngrid
                        if (a = x)
                        then 
                            let i = a
                            for j in 0..N-1 do
                                if ((j >= b) && (j <= y))
                                then board.[i,j] <- player  
                        else
                            for i in a..x do
                                for j in 0..N-1 do
                                    if (((j >= b) && (i < x)) || (j <= y) && (i = x))
                                    then board.[i,j] <- player     
    member x.MatchSaveType(player : Char, savetype : string, i : int, j : int) =
        match player with
        | 'X' -> match savetype with 
                 | "fill" -> fillX <- List.concat [[i,j];fillX]    
                 | "sleep" -> sleepX <- List.concat [[i,j];sleepX] 
                 | "limit" -> limitX <- List.concat [[i,j];limitX]
                 | _ -> printfn "Error with finding savetype."
        | 'O' -> match savetype with 
                 | "fill" -> fillO <- List.concat [[i,j];fillO]    
                 | "sleep" -> sleepO <- List.concat [[i,j];sleepO] 
                 | "limit" -> limitO <- List.concat [[i,j];limitO]
                 | _ -> printfn "Error with finding savetype."
        | _   -> printfn "Error with saving area sequence."
    member x.SaveAreaSeq(player: Char, name : string, savetype : string) = 
        for k in 0..x.VARLIST.Length-1 do
            if (x.VARLIST.[k].[0] = name) 
            then    
                for l in 1.. x.VARLIST.[k].Length-1 do
                    let pos = x.VARLIST.[k].[l]
                    let pos1 = x.Mark(pos.Split('-').[0])
                    let pos2 = x.Mark(pos.Split('-').[1])
                    let mutable a,b = (fst pos1, snd pos1)
                    let mutable c,d = (fst pos2, snd pos2)
                    let N = Ngrid
                    if (a = c)
                    then 
                        let i = a
                        for j in 0..N-1 do
                            if ((j >= b) && (j <= d))
                            then x.MatchSaveType(player,savetype,i,j)
                    else
                        for i in a..c do
                            for j in 0..N-1 do
                                if (((j >= b) && (i < c)) || (j <= d) && (i = c))
                                then x.MatchSaveType(player,savetype,i,j)    
    member x.SetSleepArea(p : Char, n : string) = 
        sleeparea <- true
        x.SaveAreaSeq(p, n, "sleep")
    member x.SetLimitArea(p : Char, n : string) =
        limitarea <- true
        x.SaveAreaSeq(p, n, "limit")
end

type PlayerMove() = class
    inherit Grid()
    let mutable movetype = "place"
    member x.MOVETYPE = movetype 
    member x.SetMoveType(s : string) =
        movetype <- s
end

type Restriction() = class
    inherit PlayerMove()
    let mutable steptype = "unlimited"
    let mutable replace = true
    let mutable zigzag = true
    let mutable skip = 0
    let mutable step = 0 
    member x.REPLACE = replace 
    member x.STEPTYPE = steptype
    member x.ZIGZAG = zigzag
    member x.SKIP = skip
    member x.STEP = step
    member x.SetReplace(b : bool) = replace <- b 
    member x.SetZigZag(b : bool) = zigzag <- b 
    member x.SetStepSize(n : int) = 
        step <- n
        steptype <- "fixed"
    member x.SetSkipSize(n : int) = skip <- n
    member x.CheckMoveSkip(X1 : int, Y1 : int, X2 : int, Y2 : int) =
        let TM = (x.SKIP + x.STEP)
        let mutable i = X1
        let mutable j = Y1
        let mutable cnt = 0
        let mutable con = true
        while (cnt <= TM-1) do
            if x.BOARD.[i,j] = ' '
            then con <- false
            else 
                if (X2 > X1) && (Y2 < Y1)
                then i <- i+1
                     j <- j-1
                if (X2 > X1) && (Y2 > Y1)
                then i <- i+1
                     j <- j+1
                     
                if (X2 < X1) && (Y2 < Y1)
                then i <- i-1
                     j <- j-1
                if (X2 < X1) && (Y2 > Y1)
                then i <- i-1
                     j <- j+1

                if (X2 > X1) && (Y2 = Y1)
                then i <- i+1

                if (X2 < X1) && (Y2 = Y1)
                then i <- i-1
                   
                if (X2 = X1) && (Y2 > Y1)
                then j <- j+1

                if (X2 = X1) && (Y2 < Y1)
                then j <- j-1
            cnt <- cnt+1
        let d1 = abs (X2-X1)
        let d2 = abs (Y2-Y1)
        let b1 = (d1 <= TM && d2 <= TM)  
        (con && b1)

end


type Turn() = class
        inherit Restriction()
        let mutable repeatturn = false
        let mutable repeattype = "skip"
        let mutable repeatpiece = "any"
        let mutable repeatint = 0
        member x.REPEATTURN = repeatturn
        member x.REPEATTYPE = repeattype
        member x.REPEATPIECE = repeatpiece
        member x.REPEATINT = repeatint  
        member x.SetRepeatTurn(reptype : string, reppiece : string, repintarg : int) = 
            repeattype <- reptype
            repeatpiece <- reppiece  
            match repeattype with  
            | "skip"          -> repeatint <- repintarg
            | _               -> printfn "Not yet implemented."
        member x.SetGiveRepeatTurn(X1 : int, Y1 : int, X2 : int, Y2 : int) =
            match repeattype with   
            | "skip" -> repeatturn <- x.CheckMoveSkip(X1,Y1,X2,Y2)
            | _      -> repeatturn <- false    
        member x.SetRepeat(b : bool) = 
            repeatturn <- b
end
type ValidChecker() = class
    inherit Turn()
    let mutable prevPos = (0,0)
    member this.PREVPOS = prevPos
    member x.GetXY() =
        let X1 = fst x.MOVEPOS.[0]
        let Y1 = snd x.MOVEPOS.[0]
        let X2 = fst x.MOVEPOS.[1]
        let Y2 = snd x.MOVEPOS.[1]
        (X1,Y1,X2,Y2)
    member x.CheckDistance(X1 : int, Y1: int, X2: int, Y2: int) =
        let D = x.STEP
        let d1 = abs (X2-X1)
        let d2 = abs (Y2-Y1)
        let b1 = (d1 <= D && d2 <= D)                  // Distance
        let b2 = (d1 = d2) || ((X1 = X2) || (Y1 = Y2)) // Diagonal or straight
        if x.ZIGZAG
        then b1
        else (b1 && b2)
    member x.CheckValidStep(X1 : int, Y1 : int, X2 : int, Y2 :int) =
        let b1 = x.CheckDistance(X1,Y1,X2,Y2)
        let b2 = x.CheckMoveSkip(X1,Y1,X2,Y2)
        match x.STEPTYPE with
        | "fixed"       -> (b1 || b2)                         
        | "unlimited"   -> true
        | _             -> printfn "error"
                           false
    member x.CheckValidPlace(str : string) = 
        let mutable pos = (0,0)
        match x.REPLACE with  
        | false ->  pos <- x.Mark(str)
                    match x.BOARD.[fst pos, snd pos] with
                    | ' ' -> true
                    |  _  -> false
        | true -> true
    member x.CheckValidMove(player : char, X1 : int, Y1: int, X2: int, Y2: int) = 
        match x.REPLACE with  
        | false ->  (x.BOARD.[X1,Y1] = player && x.BOARD.[X2,Y2] = ' ')
        | true  ->  (x.BOARD.[X1,Y1] = player)  
    member x.CheckRepeatTurnCond(inpStr : string) =
        if inpStr = "pass"
        then true
        else let positions = inpStr.Split(' ')
             let X1, Y1 = x.Mark(positions.[0])
             if x.REPEATTURN 
             then match x.REPEATPIECE with  
                  | "same" -> ((X1, Y1) = x.PREVPOS) 
                  | "any" -> true
                  | _ -> true
             else true
    member x.CheckValidSleep(player : char, X1 : int, Y1 : int) =
        match x.SLEEPAREA with  
        | true -> match player with
                  | 'X' -> not (List.contains (X1,Y1) x.SLEEPX)         
                  | 'O' -> not (List.contains (X1,Y1) x.SLEEPO)      
                  | _   -> printfn "CheckValidSleep : Error."
                           false
        | false -> true
    member x.CheckValidLimit(player : char, X1 : int, Y1 : int, X2 : int, Y2 : int) =
        match x.LIMITAREA with  
        | true ->   match player with
                    | 'X' -> if List.contains (X1,Y1) x.LIMITX
                             then ((List.contains (X1,Y1) x.LIMITX) && (List.contains (X2,Y2) x.LIMITX))
                             else true

                    | 'O' -> if List.contains (X1,Y1) x.LIMITO 
                             then ((List.contains (X1,Y1) x.LIMITO) && (List.contains (X2,Y2) x.LIMITO))
                             else true
                    | _   -> printfn "CheckValidLimit : Error."
                             false
        | false -> true
    member x.CheckValidMoveAll(player : char) = 
        let (X1,Y1,X2,Y2) = x.GetXY()
        let b1 = x.CheckValidStep(X1,Y1,X2,Y2)
        let b2 = x.CheckValidMove(player,X1,Y1,X2,Y2)
        let b3 = x.CheckValidSleep(player, X1,Y1)
        let b4 = x.CheckValidLimit(player, X1,Y1,X2,Y2)
        x.SetGiveRepeatTurn(X1,Y1,X2,Y2)
        if (b1 && b2 && b3 && b4) 
        then prevPos <- (X2,Y2)
        (b1 && b2 && b3 && b4)
end

type Win() = class
    inherit ValidChecker()
    let mutable wintype = "none"
    let mutable cohesive = ('P',0)
    member x.WINTYPE = wintype 
    member x.COHESIVE = cohesive
    member x.SetWinTypeFillSeq(p1 : Char, p2 : Char, n : String) = 
        wintype <- "fill"
        x.SaveAreaSeq(p1, n, x.WINTYPE)

    member x.SetWinTypeCohesive(P1 : Char, P2 : Char, n : int) =
        wintype <- "cohesive"
        match (P1 = P2) with
        | true -> cohesive <- ('P',n)
        | false -> cohesive <- ('R',n) 
     
    member x.CheckDiagonals1(mgrid : int, ngrid : int, p : char, r : int) = 
        let mutable col = 0
        let mutable row = 0
        let mutable cnt = 0
        let mutable win = false
        let mutable temprow = 0
        let mutable tempcol = 0 
        while (row < mgrid) do
            col <- 0
            temprow <- row
            while (temprow >= 0) && (win = false) do
                
                if (x.BOARD.[temprow,col] = p) 
                then cnt <- cnt + 1
                     if (cnt = r)
                     then win <- true
                else cnt <- 0
                temprow <- temprow - 1
                col <- col+1
      
            row <- row+1
        if (win)
        then (win)
        else 
            cnt <- 0
            col <- 1
            while (col < ngrid) do
                tempcol <- col
                row <- ngrid - 1
                while (tempcol <= ngrid-1) && (win = false) do
                    if (x.BOARD.[row, tempcol] = p)
                    then cnt <- cnt + 1
                         if cnt = r
                         then win <- true
                    else cnt <- 0
                    row <- row-1
                    tempcol <- tempcol + 1
                col <- col+1
            (win)
    member x.CheckDiagonals2(mgrid : int, ngrid : int, p : char, r : int) = 
        let mutable col = 0
        let mutable row = 0
        let mutable cnt = 0
        let mutable win = false
        let mutable temprow = 0
        let mutable tempcol = 0 
        
        while (row < mgrid) do
            temprow <- row
            col <- 0
            while (temprow < mgrid) && (win = false) do
                if (x.BOARD.[temprow,col] = p) 
                then cnt <- cnt + 1
                     if (cnt = r)
                     then win <- true
                else cnt <- 0
                temprow <- temprow + 1
                col <- col + 1
            row <- row + 1
        if (win)
        then (win)
        else 
            cnt <- 0
            col <- 1
            while (col < ngrid) do
                row <- 0
                tempcol <- col
                while (tempcol < ngrid) && (win = false) do
                    if (x.BOARD.[row, tempcol] = p)
                    then cnt <- cnt + 1
                         if cnt = r
                         then win <- true
                    else cnt <- 0
                    tempcol <- tempcol + 1
                    row <- row + 1
                col <- col + 1
            (win)
    member x.CheckRow(mgrid : int, ngrid : int, p : char, r : int) =
        let mutable win = false
        let mutable cnt = 0
        let mutable j = 0
        for i in 0 .. mgrid-1 do
            j <- 0
            while (win = false) && (j < ngrid) do
                if (x.BOARD.[i,j] = p)
                then     
                    cnt <- cnt+1
                    if (cnt = r)
                    then win <- true
                else 
                    cnt <- 0
                j <- j+1      
        (win)
    member x.CheckColumn(mgrid : int, ngrid : int, p : char, r : int) =
        let mutable win = false
        let mutable cnt = 0
        let mutable i = 0
        for j in 0.. ngrid-1 do
            i <- 0
            while (win = false) && (i < mgrid) do
                if (x.BOARD.[i,j] = p)
                then 
                    cnt <- cnt + 1
                    if (cnt = r)
                    then win <- true
                else  
                    cnt <- 0
                i <- i+1
        (win)
    member x.CheckCohesiveWin(p : char) = 
        let a = x.CheckRow(x.MGRID,x.NGRID,p,(snd x.COHESIVE))
        let b = x.CheckColumn(x.MGRID,x.NGRID,p,(snd x.COHESIVE))
        let c = x.CheckDiagonals1(x.MGRID,x.NGRID,p,(snd x.COHESIVE))
        let d = x.CheckDiagonals2(x.MGRID,x.NGRID,p,(snd x.COHESIVE))
        (a || b || c || d)
    
    member x.CheckPosList(P : char, L : (int32 * int32) list) =
        let mutable a = fst L.[0]
        let mutable b = snd L.[0]
        let mutable i = 0
        let mutable TF = true
        if (x.BOARD.[a,b] = P)
        then
            while (x.BOARD.[a,b] = P && i < L.Length) do
                let a = fst L.[i]
                let b = snd L.[i]
                i <- i+1
                if (x.BOARD.[a,b] <> P)
                then TF <- false
        else TF <- false
        TF
    member x.CheckAreas(P : char) =
        match P with 
        | 'X' -> x.CheckPosList(P, x.FILLX)
        | 'O' -> x.CheckPosList(P, x.FILLO)
        | _   -> printfn "Error." 
                 false
end
let C = new Win()
let rec DefineGame(inpStr : string) =
    if inpStr <> "pass" && inpStr.[0] <> '#'
    then 
        let lexbuf = LexBuffer<char>.FromString inpStr
        let res = FunParser.start FunLexer.token lexbuf
        match (res) with
        | Grid (Set (m :int, n : int))  -> 
            C.SetGrid(m,n)
        | PlayerMove (s : string) -> 
            C.SetMoveType(s)
        | Win (p1 : Char,If "if",Cohesive (p2 : char, I : Num)) -> 
            C.SetWinTypeCohesive(p1,p2,I)
        | Win (p1 : Char,If "if",Fill (p2 : char, Name n : VariableName)) -> 
            C.SetWinTypeFillSeq(p1,p2,n)  
        | Restrict (Replace (b : Bool)) -> 
            C.SetReplace(b)
        | Restrict (ZigZag (b : Bool)) ->
            C.SetZigZag(b)
        | Restrict (Step (n : Num)) ->
            C.SetStepSize(n)
        | Restrict (Skip (n : Num)) ->
            C.SetSkipSize(n)          
        | VarDec (Name n : VariableName, AreaSeq v : Variable) -> 
            C.SetVariableAreaSeq(n,v)
        | Grid (Mark (p : Char, Name n : VariableName)) -> 
            C.MarkAreaSeq(p,n)
        | Grid (Sleep (p : Char, Name n : VariableName)) ->
            C.SetSleepArea(p,n)
        | Grid (Limit (p : Char, Name n : VariableName)) ->
            C.SetLimitArea(p,n)
        | Turn (Same (s : string),TurnAction (Skip (n : Num))) ->
            C.SetRepeatTurn("skip",s,n)
        | _  -> printfn "%A Not yet implemented" (res)
   
let mutable playerScoreX = 0
let mutable playerScoreO = 0

let SetPlayerScore(p : char) =
    match p with  
    | 'X' -> playerScoreX <- playerScoreX + 1
    | 'O' -> playerScoreO <- playerScoreO + 1
    | _ -> printfn "SetPlayerScore : Error."
    printfn " Current Score: %d %d \n" (playerScoreX) (playerScoreO)



let mutable filename = " "
let rec BeginGame(player : char) =
    let mutable endgame = false
    Console.Write(" \n")
    printfn "%A" C.BOARD
    let rec Go() =
        Console.Write(" \n Player {0} | Enter your command: ", player)
        let inpStr = Console.ReadLine()
        Console.Write("\n ============================== \n")
        let lexbuf = LexBuffer<char>.FromString inpStr
        let res = FunParser.start FunLexer.token lexbuf
        if C.CheckRepeatTurnCond(inpStr)
        then match res with
             | GamePlay (PlacePiece (pos : string)) ->  
                match C.MOVETYPE with  
                | "place" -> C.SetPlacePiece(pos) 
                             match C.CheckValidPlace(pos) with
                             | true -> C.GameMarkCell(player)
                             | false -> printfn "\n Invalid move. \n"
                                        Go()

                | _       -> printfn "\n You need to move a piece from one position to another. \n"
                             Go()
             | GamePlay (MovePiece (pos1 : string,pos2 : string)) ->  
                match C.MOVETYPE with  
                | "move"  -> C.SetPlacePiece(pos2) 
                             C.SetMovePiece(pos1,pos2)
                             match C.CheckValidMoveAll(player) with
                             | true -> C.GameMovePiece(player)
                             | false -> printfn "\n Invalid move. \n"
                                        Go()
                | _       -> printfn "\n You are not allowed to move pieces. \n"
                             Go()
             | GamePlay (Pass ("pass"))   -> 
                C.SetRepeat(false)
                Console.Write("Player {0} passes turn. \n", player)      
             | _     ->  printfn "\n Something is wrong. \n"
             match C.WINTYPE with
             | "cohesive" -> endgame <- C.CheckCohesiveWin(player)
             | "fill"     -> endgame <- C.CheckAreas(player)
             | _          -> endgame <- false
             if (endgame = false)
             then match player with
                    | 'X' -> match C.REPEATTURN with
                             | false -> BeginGame('O')
                             | true -> BeginGame('X')
                    | 'O' -> match C.REPEATTURN with
                             | false -> BeginGame('X')
                             | true -> BeginGame('O')
                    | _ -> printfn "Error with next turn."
             else printfn "%A" C.BOARD
                  printfn "\n === Player %c Won ===" player
                  SetPlayerScore(player)
                  let lines = File.ReadLines(filename)                
                  lines |> Seq.iter(fun x -> DefineGame(x)) 
                  BeginGame('X')
         else 
            printfn "\n Invalid move.\n" 
            Go()
    Go()




[<EntryPoint>]
let main args =
    printfn "\n\n ... Reading program : %s" args.[1]
    filename <- args.[1] |> string
    let lines = File.ReadLines(filename)                
    lines |> Seq.iter(fun x -> DefineGame(x)) 
    BeginGame('X')
    0


