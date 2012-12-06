namespace FSharpRefactor.Tests.RenameTests

open NUnit.Framework

open FSharpRefactor.Engine.Ast
open FSharpRefactor.Refactorings.Rename

[<TestFixture>]
type ScopeTreeModule() =
    [<Test>]
    member this.``Creates a scope tree for a simple let statement``() =
        let source = "let a = 1 in a"
        let rootNode = Ast.Parse source
        let scopeTree = makeScopeTree (rootNode.Value)

        match scopeTree with
            | [ScopeTree.Declaration(["a",_],[ScopeTree.Usage("a",_)])] -> ()
            | _ -> Assert.Fail("The scope tree for 'let a = 1 in a' was incorrect:\n" + (sprintf "%A" scopeTree))

    [<Test>]
    member this.``Creates a scope tree for a more elaborate sequence of let statements``() =
        let source = "let a =\n  let b = 1\n  let c = 2 + b + b\n  let d = 1\n  b+c\nlet b = 3+a"
        let rootNode = Ast.Parse source
        let scopeTree = makeScopeTree (rootNode.Value)

        match scopeTree with
            | [ScopeTree.Declaration(["a",_],[ScopeTree.Usage("a",_)])] -> ()
            | _ -> Assert.Fail("The scope tree for 'let a = 1 in a' was incorrect:\n" + (sprintf "%A" scopeTree))