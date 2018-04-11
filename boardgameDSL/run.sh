#! /bin/bash
File : $1
fsyacc FunParser.fsy
fslex --unicode FunLexer.fsl
echo 'module FunParser' | cat - FunParser.fs > temp && mv temp FunParser.fs
fsharpc -r FsLexYacc.Runtime.dll absyn.fs FunParser.fs FunLexer.fs Program.fs
mono program.exe EntryPoint.exe $1
