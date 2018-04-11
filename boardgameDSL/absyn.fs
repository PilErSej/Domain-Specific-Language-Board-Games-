module AbSyn
open System

type Num = int
type Bool = bool
type String = string
type Char = char
type Pair = Num * Num
type Player = Char

type Rules =
    | VarDec of VariableName * Variable
    | Grid of BuildInBoardFun
    | PlayerMove of String
    | Win of Player * Condition * BuildInStatesFun
    | Restrict of BuildInActionFun
    | GamePlay of PlayerAction
    | Turn of PieceSpec * BuildInTurn

and VariableName = 
    | Name of String

and Variable =
    | AreaSeq of string list

and BuildInTurn =
    | TurnAction of BuildInActionFun
    | TurnStates of BuildInStatesFun

and BuildInBoardFun = 
    | Set of (Num*Num)
    | Mark of (Player * VariableName)
    | Sleep of (Player * VariableName)
    | Limit of (Player * VariableName)

and BuildInStatesFun = 
    | Cohesive of Player * Num
    | Fill of Player * VariableName

and BuildInActionFun =
    | Replace of Bool
    | ZigZag of Bool
    | Step of Num
    | Skip of Num

and Condition = 
    | If of String

and PlayerAction =
    | PlacePiece of String
    | MovePiece of String * String
    | Pass of String

and PieceSpec = 
    | Same of String
    | Any of String
