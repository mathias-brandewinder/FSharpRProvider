﻿// --------------------------------------------------------------------------------------
// Builds the documentation from FSX files in the 'samples' directory
// (the documentation is stored in the 'docs' directory)
// --------------------------------------------------------------------------------------

#I "../../packages/FSharp.Formatting.2.0.1/lib/net40"
#r "FSharp.Literate.dll"
#r "FSharp.CodeFormat.dll"
open System.IO
open FSharp.Literate

let (++) a b = Path.Combine(a, b)
let template = __SOURCE_DIRECTORY__ ++ "template.html"
let sources  = __SOURCE_DIRECTORY__ ++ "../content"
let files = __SOURCE_DIRECTORY__ ++ "../files"
let output   = __SOURCE_DIRECTORY__ ++ "../output"

// Root URL for the generated HTML
let root = "http://bluemountaincapital.github.io/FSharpRProvider" 
// Root URL for local testing
// let root = "file://C:\dev\FSharp.RProvider\generated"

// Generate HTML from all FSX files in samples & subdirectories
let build () =
  // Copy all sample data files to the "data" directory
  let copy = [ sources ++ "../../packages/FSharp.Formatting.2.0.1/literate/content", output ++ "content"
               files ++ "img", output ++ "img"
               files ++ "misc", output ++ "misc" ]
  for source, target in copy do
    if Directory.Exists target then Directory.Delete(target, true)
    Directory.CreateDirectory target |> ignore
    for fileInfo in DirectoryInfo(source).EnumerateFiles() do
        fileInfo.CopyTo(target ++ fileInfo.Name) |> ignore

  Literate.ProcessDirectory
    ( sources, template, output, 
      replacements = [ "root", root ] )

build()