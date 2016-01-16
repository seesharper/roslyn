// Copyright (c) Microsoft.  All Rights Reserved.  Licensed under the Apache License, Version 2.0.  See License.txt in the project root for license information.

using System.Threading.Tasks;
using Microsoft.CodeAnalysis.Editor.UnitTests.BraceMatching;
using Microsoft.CodeAnalysis.Editor.UnitTests.Workspaces;
using Microsoft.CodeAnalysis.Text;
using Roslyn.Test.Utilities;
using Xunit;

namespace Microsoft.CodeAnalysis.Editor.CSharp.UnitTests.BraceMatching
{
    public class CSharpBraceMatcherTests : AbstractBraceMatcherTests
    {
        protected override Task<TestWorkspace> CreateWorkspaceFromCodeAsync(string code)
        {
            return TestWorkspaceFactory.CreateCSharpWorkspaceAsync(code);
        }

        [Fact, Trait(Traits.Feature, Traits.Features.BraceMatching)]
        public async Task TestEmptyFile()
        {
            var code = @"$$";
            var expected = @"";

            await TestAsync(code, expected);
        }

        [Fact, Trait(Traits.Feature, Traits.Features.BraceMatching)]
        public async Task TestAtFirstPositionInFile()
        {
            var code = @"$$public class C { }";
            var expected = @"public class C { }";

            await TestAsync(code, expected);
        }

        [Fact, Trait(Traits.Feature, Traits.Features.BraceMatching)]
        public async Task TestAtLastPositionInFile()
        {
            var code = @"public class C { }$$";
            var expected = @"public class C [|{|] }";

            await TestAsync(code, expected);
        }

        [Fact, Trait(Traits.Feature, Traits.Features.BraceMatching)]
        public async Task TestCurlyBrace1()
        {
            var code = @"public class C $${ }";
            var expected = @"public class C { [|}|]";

            await TestAsync(code, expected);
        }

        [Fact, Trait(Traits.Feature, Traits.Features.BraceMatching)]
        public async Task TestCurlyBrace2()
        {
            var code = @"public class C {$$ }";
            var expected = @"public class C { [|}|]";

            await TestAsync(code, expected);
        }

        [Fact, Trait(Traits.Feature, Traits.Features.BraceMatching)]
        public async Task TestCurlyBrace3()
        {
            var code = @"public class C { $$}";
            var expected = @"public class C [|{|] }";

            await TestAsync(code, expected);
        }

        [Fact, Trait(Traits.Feature, Traits.Features.BraceMatching)]
        public async Task TestCurlyBrace4()
        {
            var code = @"public class C { }$$";
            var expected = @"public class C [|{|] }";

            await TestAsync(code, expected);
        }

        [Fact, Trait(Traits.Feature, Traits.Features.BraceMatching)]
        public async Task TestParen1()
        {
            var code = @"public class C { void Foo$$() { } }";
            var expected = @"public class C { void Foo([|)|] { } }";

            await TestAsync(code, expected);
        }

        [Fact, Trait(Traits.Feature, Traits.Features.BraceMatching)]
        public async Task TestParen2()
        {
            var code = @"public class C { void Foo($$) { } }";
            var expected = @"public class C { void Foo([|)|] { } }";

            await TestAsync(code, expected);
        }

        [Fact, Trait(Traits.Feature, Traits.Features.BraceMatching)]
        public async Task TestParen3()
        {
            var code = @"public class C { void Foo($$ ) { } }";
            var expected = @"public class C { void Foo( [|)|] { } }";

            await TestAsync(code, expected);
        }

        [Fact, Trait(Traits.Feature, Traits.Features.BraceMatching)]
        public async Task TestParen4()
        {
            var code = @"public class C { void Foo( $$) { } }";
            var expected = @"public class C { void Foo[|(|] ) { } }";

            await TestAsync(code, expected);
        }

        [Fact, Trait(Traits.Feature, Traits.Features.BraceMatching)]
        public async Task TestParen5()
        {
            var code = @"public class C { void Foo( )$$ { } }";
            var expected = @"public class C { void Foo[|(|] ) { } }";

            await TestAsync(code, expected);
        }

        [Fact, Trait(Traits.Feature, Traits.Features.BraceMatching)]
        public async Task TestParen6()
        {
            var code = @"public class C { void Foo()$$ { } }";
            var expected = @"public class C { void Foo[|(|]) { } }";

            await TestAsync(code, expected);
        }

        [Fact, Trait(Traits.Feature, Traits.Features.BraceMatching)]
        public async Task TestSquareBracket1()
        {
            var code = @"public class C { int$$[] i; }";
            var expected = @"public class C { int[[|]|] i; }";

            await TestAsync(code, expected);
        }

        [Fact, Trait(Traits.Feature, Traits.Features.BraceMatching)]
        public async Task TestSquareBracket2()
        {
            var code = @"public class C { int[$$] i; }";
            var expected = @"public class C { int[[|]|] i; }";

            await TestAsync(code, expected);
        }

        [Fact, Trait(Traits.Feature, Traits.Features.BraceMatching)]
        public async Task TestSquareBracket3()
        {
            var code = @"public class C { int[$$ ] i; }";
            var expected = @"public class C { int[ [|]|] i; }";

            await TestAsync(code, expected);
        }

        [Fact, Trait(Traits.Feature, Traits.Features.BraceMatching)]
        public async Task TestSquareBracket4()
        {
            var code = @"public class C { int[ $$] i; }";
            var expected = @"public class C { int[|[|] ] i; }";

            await TestAsync(code, expected);
        }

        [Fact, Trait(Traits.Feature, Traits.Features.BraceMatching)]
        public async Task TestSquareBracket5()
        {
            var code = @"public class C { int[ ]$$ i; }";
            var expected = @"public class C { int[|[|] ] i; }";

            await TestAsync(code, expected);
        }

        [Fact, Trait(Traits.Feature, Traits.Features.BraceMatching)]
        public async Task TestSquareBracket6()
        {
            var code = @"public class C { int[]$$ i; }";
            var expected = @"public class C { int[|[|]] i; }";

            await TestAsync(code, expected);
        }

        [Fact, Trait(Traits.Feature, Traits.Features.BraceMatching)]
        public async Task TestAngleBracket1()
        {
            var code = @"public class C { Foo$$<int> f; }";
            var expected = @"public class C { Foo<int[|>|] f; }";

            await TestAsync(code, expected);
        }

        [Fact, Trait(Traits.Feature, Traits.Features.BraceMatching)]
        public async Task TestAngleBracket2()
        {
            var code = @"public class C { Foo<$$int> f; }";
            var expected = @"public class C { Foo<int[|>|] f; }";

            await TestAsync(code, expected);
        }

        [Fact, Trait(Traits.Feature, Traits.Features.BraceMatching)]
        public async Task TestAngleBracket3()
        {
            var code = @"public class C { Foo<int$$> f; }";
            var expected = @"public class C { Foo[|<|]int> f; }";

            await TestAsync(code, expected);
        }

        [Fact, Trait(Traits.Feature, Traits.Features.BraceMatching)]
        public async Task TestAngleBracket4()
        {
            var code = @"public class C { Foo<int>$$ f; }";
            var expected = @"public class C { Foo[|<|]int> f; }";

            await TestAsync(code, expected);
        }

        [Fact, Trait(Traits.Feature, Traits.Features.BraceMatching)]
        public async Task TestNestedAngleBracket1()
        {
            var code = @"public class C { Func$$<Func<int,int>> f; }";
            var expected = @"public class C { Func<Func<int,int>[|>|] f; }";

            await TestAsync(code, expected);
        }

        [Fact, Trait(Traits.Feature, Traits.Features.BraceMatching)]
        public async Task TestNestedAngleBracket2()
        {
            var code = @"public class C { Func<$$Func<int,int>> f; }";
            var expected = @"public class C { Func<Func<int,int>[|>|] f; }";

            await TestAsync(code, expected);
        }

        [Fact, Trait(Traits.Feature, Traits.Features.BraceMatching)]
        public async Task TestNestedAngleBracket3()
        {
            var code = @"public class C { Func<Func$$<int,int>> f; }";
            var expected = @"public class C { Func<Func<int,int[|>|]> f; }";

            await TestAsync(code, expected);
        }

        [Fact, Trait(Traits.Feature, Traits.Features.BraceMatching)]
        public async Task TestNestedAngleBracket4()
        {
            var code = @"public class C { Func<Func<$$int,int>> f; }";
            var expected = @"public class C { Func<Func<int,int[|>|]> f; }";

            await TestAsync(code, expected);
        }

        [Fact, Trait(Traits.Feature, Traits.Features.BraceMatching)]
        public async Task TestNestedAngleBracket5()
        {
            var code = @"public class C { Func<Func<int,int$$>> f; }";
            var expected = @"public class C { Func<Func[|<|]int,int>> f; }";

            await TestAsync(code, expected);
        }

        [Fact, Trait(Traits.Feature, Traits.Features.BraceMatching)]
        public async Task TestNestedAngleBracket6()
        {
            var code = @"public class C { Func<Func<int,int>$$> f; }";
            var expected = @"public class C { Func<Func[|<|]int,int>> f; }";

            await TestAsync(code, expected);
        }

        [Fact, Trait(Traits.Feature, Traits.Features.BraceMatching)]
        public async Task TestNestedAngleBracket7()
        {
            var code = @"public class C { Func<Func<int,int> $$> f; }";
            var expected = @"public class C { Func[|<|]Func<int,int> > f; }";

            await TestAsync(code, expected);
        }

        [Fact, Trait(Traits.Feature, Traits.Features.BraceMatching)]
        public async Task TestNestedAngleBracket8()
        {
            var code = @"public class C { Func<Func<int,int>>$$ f; }";
            var expected = @"public class C { Func[|<|]Func<int,int>> f; }";

            await TestAsync(code, expected);
        }

        [Fact, Trait(Traits.Feature, Traits.Features.BraceMatching)]
        public async Task TestString1()
        {
            var code = @"public class C { string s = $$""Foo""; }";
            var expected = @"public class C { string s = ""Foo[|""|]; }";

            await TestAsync(code, expected);
        }

        [Fact, Trait(Traits.Feature, Traits.Features.BraceMatching)]
        public async Task TestString2()
        {
            var code = @"public class C { string s = ""$$Foo""; }";
            var expected = @"public class C { string s = ""Foo[|""|]; }";

            await TestAsync(code, expected);
        }

        [Fact, Trait(Traits.Feature, Traits.Features.BraceMatching)]
        public async Task TestString3()
        {
            var code = @"public class C { string s = ""Foo$$""; }";
            var expected = @"public class C { string s = [|""|]Foo""; }";

            await TestAsync(code, expected);
        }

        [Fact, Trait(Traits.Feature, Traits.Features.BraceMatching)]
        public async Task TestString4()
        {
            var code = @"public class C { string s = ""Foo""$$; }";
            var expected = @"public class C { string s = [|""|]Foo""; }";

            await TestAsync(code, expected);
        }

        [Fact, Trait(Traits.Feature, Traits.Features.BraceMatching)]
        public async Task TestString5()
        {
            var code = @"public class C { string s = ""Foo$$ ";
            var expected = @"public class C { string s = ""Foo ";

            await TestAsync(code, expected);
        }

        [Fact, Trait(Traits.Feature, Traits.Features.BraceMatching)]
        public async Task TestVerbatimString1()
        {
            var code = @"public class C { string s = $$@""Foo""; }";
            var expected = @"public class C { string s = @""Foo[|""|]; }";

            await TestAsync(code, expected);
        }

        [Fact, Trait(Traits.Feature, Traits.Features.BraceMatching)]
        public async Task TestVerbatimString2()
        {
            var code = @"public class C { string s = @$$""Foo""; }";
            var expected = @"public class C { string s = @""Foo[|""|]; }";

            await TestAsync(code, expected);
        }

        [Fact, Trait(Traits.Feature, Traits.Features.BraceMatching)]
        public async Task TestVerbatimString3()
        {
            var code = @"public class C { string s = @""$$Foo""; }";
            var expected = @"public class C { string s = @""Foo[|""|]; }";

            await TestAsync(code, expected);
        }

        [Fact, Trait(Traits.Feature, Traits.Features.BraceMatching)]
        public async Task TestVerbatimString4()
        {
            var code = @"public class C { string s = @""Foo$$""; }";
            var expected = @"public class C { string s = [|@""|]Foo""; }";

            await TestAsync(code, expected);
        }

        [Fact, Trait(Traits.Feature, Traits.Features.BraceMatching)]
        public async Task TestVerbatimString5()
        {
            var code = @"public class C { string s = @""Foo""$$; }";
            var expected = @"public class C { string s = [|@""|]Foo""; }";

            await TestAsync(code, expected);
        }

        [Fact, Trait(Traits.Feature, Traits.Features.BraceMatching)]
        public async Task TestInterpolatedString1()
        {
            var code = @"public class C { void M() { var x = ""Hello""; var y = ""World""; var s = $""$${x}, {y}""; }";
            var expected = @"public class C { void M() { var x = ""Hello""; var y = ""World""; var s = $""{x[|}|], {y}""; }";

            await TestAsync(code, expected);
        }

        [Fact, Trait(Traits.Feature, Traits.Features.BraceMatching)]
        public async Task TestInterpolatedString2()
        {
            var code = @"public class C { void M() { var x = ""Hello""; var y = ""World""; var s = $""{$$x}, {y}""; }";
            var expected = @"public class C { void M() { var x = ""Hello""; var y = ""World""; var s = $""{x[|}|], {y}""; }";

            await TestAsync(code, expected);
        }

        [Fact, Trait(Traits.Feature, Traits.Features.BraceMatching)]
        public async Task TestInterpolatedString3()
        {
            var code = @"public class C { void M() { var x = ""Hello""; var y = ""World""; var s = $""{x$$}, {y}""; }";
            var expected = @"public class C { void M() { var x = ""Hello""; var y = ""World""; var s = $""[|{|]x}, {y}""; }";

            await TestAsync(code, expected);
        }

        [Fact, Trait(Traits.Feature, Traits.Features.BraceMatching)]
        public async Task TestInterpolatedString4()
        {
            var code = @"public class C { void M() { var x = ""Hello""; var y = ""World""; var s = $""{x}$$, {y}""; }";
            var expected = @"public class C { void M() { var x = ""Hello""; var y = ""World""; var s = $""[|{|]x}, {y}""; }";

            await TestAsync(code, expected);
        }

        [Fact, Trait(Traits.Feature, Traits.Features.BraceMatching)]
        public async Task TestInterpolatedString5()
        {
            var code = @"public class C { void M() { var x = ""Hello""; var y = ""World""; var s = $""{x}, $${y}""; }";
            var expected = @"public class C { void M() { var x = ""Hello""; var y = ""World""; var s = $""{x}, {y[|}|]""; }";

            await TestAsync(code, expected);
        }

        [Fact, Trait(Traits.Feature, Traits.Features.BraceMatching)]
        public async Task TestInterpolatedString6()
        {
            var code = @"public class C { void M() { var x = ""Hello""; var y = ""World""; var s = $""{x}, {$$y}""; }";
            var expected = @"public class C { void M() { var x = ""Hello""; var y = ""World""; var s = $""{x}, {y[|}|]""; }";

            await TestAsync(code, expected);
        }

        [Fact, Trait(Traits.Feature, Traits.Features.BraceMatching)]
        public async Task TestInterpolatedString7()
        {
            var code = @"public class C { void M() { var x = ""Hello""; var y = ""World""; var s = $""{x}, {y$$}""; }";
            var expected = @"public class C { void M() { var x = ""Hello""; var y = ""World""; var s = $""{x}, [|{|]y}""; }";

            await TestAsync(code, expected);
        }

        [Fact, Trait(Traits.Feature, Traits.Features.BraceMatching)]
        public async Task TestInterpolatedString8()
        {
            var code = @"public class C { void M() { var x = ""Hello""; var y = ""World""; var s = $""{x}, {y}$$""; }";
            var expected = @"public class C { void M() { var x = ""Hello""; var y = ""World""; var s = $""{x}, [|{|]y}""; }";

            await TestAsync(code, expected);
        }

        [Fact, Trait(Traits.Feature, Traits.Features.BraceMatching)]
        public async Task TestInterpolatedString9()
        {
            var code = @"public class C { void M() { var x = ""Hello""; var y = ""World""; var s = $$[||]$""{x}, {y}""; }";
            var expected = @"public class C { void M() { var x = ""Hello""; var y = ""World""; var s = $""{x}, {y}[|""|]; }";

            await TestAsync(code, expected);
        }

        [Fact, Trait(Traits.Feature, Traits.Features.BraceMatching)]
        public async Task TestInterpolatedString10()
        {
            var code = @"public class C { void M() { var x = ""Hello""; var y = ""World""; var s = $[||]$$""{x}, {y}""; }";
            var expected = @"public class C { void M() { var x = ""Hello""; var y = ""World""; var s = $""{x}, {y}[|""|]; }";

            await TestAsync(code, expected);
        }

        [Fact, Trait(Traits.Feature, Traits.Features.BraceMatching)]
        public async Task TestInterpolatedString11()
        {
            var code = @"public class C { void M() { var x = ""Hello""; var y = ""World""; var s = $$[||]$@""{x}, {y}""; }";
            var expected = @"public class C { void M() { var x = ""Hello""; var y = ""World""; var s = $@""{x}, {y}[|""|]; }";

            await TestAsync(code, expected);
        }

        [Fact, Trait(Traits.Feature, Traits.Features.BraceMatching)]
        public async Task TestInterpolatedString12()
        {
            var code = @"public class C { void M() { var x = ""Hello""; var y = ""World""; var s = $[||]$$@""{x}, {y}""; }";
            var expected = @"public class C { void M() { var x = ""Hello""; var y = ""World""; var s = $@""{x}, {y}[|""|]; }";

            await TestAsync(code, expected);
        }

        [Fact, Trait(Traits.Feature, Traits.Features.BraceMatching)]
        public async Task TestInterpolatedString13()
        {
            var code = @"public class C { void M() { var x = ""Hello""; var y = ""World""; var s = $@$$""{x}, {y}""; }";
            var expected = @"public class C { void M() { var x = ""Hello""; var y = ""World""; var s = $@""{x}, {y}[|""|]; }";

            await TestAsync(code, expected);
        }

        [WorkItem(7120, "https://github.com/dotnet/roslyn/issues/7120")]
        [WpfFact, Trait(Traits.Feature, Traits.Features.BraceMatching)]
        public async Task TestConditionalDirectiveWithSingleMatchingDirective()
        {
            var code = @"
public class C 
{
#if$$ CHK 
#endif
}";
            var expected = @"
public class C 
{
#if$$ CHK 
[|#endif|]
}";

            await TestAsync(code, expected);
        }

        [WorkItem(7120, "https://github.com/dotnet/roslyn/issues/7120")]
        [WpfFact, Trait(Traits.Feature, Traits.Features.BraceMatching)]
        public async Task TestConditionalDirectiveWithTwoMatchingDirectives()
        {
            var code = @"
public class C 
{
#if$$ CHK 
#else
#endif
}";
            var expected = @"
public class C 
{
#if$$ CHK 
[|#else|]
#endif
}";

            await TestAsync(code, expected);
        }

        [WorkItem(7120, "https://github.com/dotnet/roslyn/issues/7120")]
        [WpfFact, Trait(Traits.Feature, Traits.Features.BraceMatching)]
        public async Task TestConditionalDirectiveWithAllMatchingDirectives()
        {
            var code = @"
public class C 
{
#if CHK 
#elif RET
#else
#endif$$
}";
            var expected = @"
public class C 
{
[|#if|] CHK 
#elif RET
#else
#endif
}";

            await TestAsync(code, expected);
        }

        [WorkItem(7120, "https://github.com/dotnet/roslyn/issues/7120")]
        [WpfFact, Trait(Traits.Feature, Traits.Features.BraceMatching)]
        public async Task TestRegionDirective()
        {
            var code = @"
public class C 
{
$$#region test
#endregion
}";
            var expected = @"
public class C 
{
#region test
[|#endregion|]
}";

            await TestAsync(code, expected);
        }

        [WorkItem(7120, "https://github.com/dotnet/roslyn/issues/7120")]
        [WpfFact, Trait(Traits.Feature, Traits.Features.BraceMatching)]
        public async Task TestInterleavedDirectivesInner()
        {
            var code = @"
#define CHK
public class C 
{
    void Test()
    {
#if CHK
$$#region test
    var x = 5;
#endregion
#else
    var y = 6;
#endif
    }
}";
            var expected = @"
#define CHK
public class C 
{
    void Test()
    {
#if CHK
#region test
    var x = 5;
[|#endregion|]
#else
    var y = 6;
#endif
    }
}";

            await TestAsync(code, expected);
        }

        [WorkItem(7120, "https://github.com/dotnet/roslyn/issues/7120")]
        [WpfFact, Trait(Traits.Feature, Traits.Features.BraceMatching)]
        public async Task TestInterleavedDirectivesOuter()
        {
            var code = @"
#define CHK
public class C 
{
    void Test()
    {
#if$$ CHK
#region test
    var x = 5;
#endregion
#else
    var y = 6;
#endif
    }
}";
            var expected = @"
#define CHK
public class C 
{
    void Test()
    {
#if CHK
#region test
    var x = 5;
#endregion
[|#else|]
    var y = 6;
#endif
    }
}";

            await TestAsync(code, expected);
        }

        [WorkItem(7120, "https://github.com/dotnet/roslyn/issues/7120")]
        [WpfFact, Trait(Traits.Feature, Traits.Features.BraceMatching)]
        public async Task TestUnmatchedDirective1()
        {
            var code = @"
public class C 
{
$$#region test
}";
            var expected = @"
public class C 
{
#region test
}";

            await TestAsync(code, expected);
        }

        [WorkItem(7120, "https://github.com/dotnet/roslyn/issues/7120")]
        [WpfFact, Trait(Traits.Feature, Traits.Features.BraceMatching)]
        public async Task TestUnmatchedDirective2()
        {
            var code = @"
#d$$efine CHK
public class C 
{
}";
            var expected = @"
#define CHK
public class C 
{
}";

            await TestAsync(code, expected);
        }

        [WorkItem(7534, "https://github.com/dotnet/roslyn/issues/7534")]
        [WpfFact, Trait(Traits.Feature, Traits.Features.BraceMatching)]
        public async Task TestUnmatchedConditionalDirective()
        {
            var code = @"
class Program
{
    static void Main(string[] args)
    {#if$$

    }
}";
            var expected = @"
class Program
{
    static void Main(string[] args)
    {#if

    }
}";

            await TestAsync(code, expected);
        }

        [WorkItem(7534, "https://github.com/dotnet/roslyn/issues/7534")]
        [WpfFact, Trait(Traits.Feature, Traits.Features.BraceMatching)]
        public async Task TestUnmatchedConditionalDirective2()
        {
            var code = @"
class Program
{
    static void Main(string[] args)
    {#else$$

    }
}";
            var expected = @"
class Program
{
    static void Main(string[] args)
    {#else

    }
}";

            await TestAsync(code, expected);
        }
    }
}
