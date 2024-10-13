// 
// using System;
// using System.Linq;
// using FluentAssertions;
// using Org.NProlog.Api;
// 
// namespace CSPrologTest
// {
//     [TestClass]
//     public class ParserTest
//     {
//         [TestMethod]
//         [DataRow(":-(foo,true)", "foo.")]
//         [DataRow(":-(foo,true)", "foo .")]
//         [DataRow(":-(foo,true)", "'foo'.")]
//         public void ParsesSymbols(string expected, string test)
//         {
//             //expected.CanParse();
//             test.CanParse();
//         }
// 
//         [TestMethod]
//         [DataRow("-f_-_.")]
//         [DataRow("-abc.")]
//         [DataRow("%This line is commented out: a :- b.")]
//         public void ParsesSymbolsFalsePositives(string test)
//         {
//             test.CanParse();
//         }
// 
//         [TestMethod]
//         [DataRow(":-(a,Foobar123_)", "a :- Foobar123_.")]
//         [DataRow(":-(a,_)", "a :- _ .")]
//         [DataRow(":-(a,__)", "a :- __ .")]
//         [DataRow(":-(a,A__a___)", "a :- A__a___ .")]
//         public void ParsesVariables(string expected, string test)
//         {
//             //expected.CanParse();
//             test.CanParse();
//         }
// 
//         [TestMethod]
//         [DataRow("foobar123_d.", 1, 0, 12)]
//         [DataRow("\r\n\r\nfoobar123_d.\r\n", 3, 4, 16)]
//         [DataRow("   foobar123_d.   ", 1, 3, 15)]
//         public void PopulatesSourceInfo(string test, int lineNo, int start, int final)
//         {
//             PredicateDescr d = test.CanParse();
//             d.ClauseList.Head.Symbol.LineNoAdjusted.Should().Be(lineNo);
//             d.ClauseList.Head.Symbol.StartAdjusted.Should().Be(start);
//             d.ClauseList.Head.Symbol.FinalAdjusted.Should().Be(final);
//             d.ClauseList.Head.Symbol.Class.Should().Be(BaseParser.SymbolClass.Id);
//             test.Substring(start, final - start).Should().Be("foobar123_d.");
//         }
// 
//         [TestMethod]
//         public void PopulatesMetaData()
//         {
//             string test = @"
// %% foo(++Ground:Type, +Instantiated, -Output, --Unbound, ?BoundToPartial, !Mutable)
// %
// % This comment will be displayed in the code completion menu.
// % The comment header might be used for IDE static analysis in the future.
// %
// foo(A, B, C, D, E, F) :- A = myFooAtom, fail.
// 
// % Unit testing example - using SWI-Prolog style directives:
// :- begin_tests(arithm).
//     testSimpleAdd :- 1 + 2 =:= 3.
//     testSub :- 2 - 1 =:= 1.
// :- end_tests(arithm).
// ";
//             Prolog e = new Prolog();
//             e.ConsultFromString(test);
// 
//             PredicateDescr descrFoo = e.PredTable.Predicates.Where(x => !x.Value.IsPredefined && x.Key == "6foo").Single().Value;
//             descrFoo.ClauseListEnd.Head.CommentHeader.Should().Be("foo(++Ground:Type, +Instantiated, -Output, --Unbound, ?BoundToPartial, !Mutable)");
//             descrFoo.ClauseListEnd.Head.CommentBody.Should().Be(@"
// This comment will be displayed in the code completion menu.
// The comment header might be used for IDE static analysis in the future.
// 
// ");
// 
//             PredicateDescr descrTestSub = e.PredTable.Predicates.Where(x => !x.Value.IsPredefined && x.Key == "0testSub").Single().Value;
//             descrTestSub.ClauseListEnd.Head.TestGroup.Should().Be("arithm");
//         }
// 
//         [TestMethod]
//         [DataRow(":-(struct,true)", "struct().")]
//         [DataRow(":-(struct(abba),true)", "struct(abba).")]
//         [DataRow(":-(struct(abba,babba,Variable),true)", "struct(abba, babba,Variable ).")]
//         [DataRow(":-(struct(innerStruct(a,b,c)),true)", "struct(innerStruct(a,b,c)).")]
//         [DataRow(":-(struct(abba,babba,Variable,innerStruct(a,b,c)),true)",
//             "struct(abba, babba,Variable ,innerStruct(a,b,c)).")]
//         [DataRow(":-(struct,true)", "(struct()).")]
//         public void ParsesStructureCall(string expected, string test)
//         {
//             //expected.CanParse();
//             test.CanParse();
//         }
// 
//         [TestMethod]
//         [DataRow(":-(+(atom,Var),true)", "atom + Var.")]
//         [DataRow(":-(+(atom,Var),true)", "+(atom,Var).")]
//         [DataRow(":-(+(atom,Var),true)", "'+'(atom,Var).")]
//         [DataRow(":-(b(a,c),true)", "a b c.")]
//         [DataRow(":-(-=>(atom,Var),true)", "atom -=> Var.")]
//         [DataRow(":-(+(a,b),true)", "a+b.")]
//         [DataRow(":-(+(+(a,b),c),true)", "a+b+c.")]
//         [DataRow(":-(+(+(a,b),c),true)", "(a+b+c).")]
//         [DataRow(":-(+(aaa,+(b,c)),true)", "aaa+(b+c).")]
//         [DataRow(":-(+(+(a,b),c),true)", "(a+b)+c.")]
//         [DataRow(":-(+(a,*(b,c)),true)", "a+b*c.")]
//         [DataRow(":-(+(*(a,b),c),true)", "a*b+c.")]
//         [DataRow(":-(+(a,b),true)", "(a+b).")]
//         [DataRow(":-(+(a,b),true)", " ( a  +  b ) .")]
//         [DataRow(":-(->(a,b),true)", "a->b.")]
//         [DataRow(":-(=(a,b),true)", "a=b.")]
//         [DataRow(":-(*(+(a,b),c),true)", " (a+b)*c.")]
//         [DataRow(":-(*(+(a,b),c),true)", " ( a  +  b )  *  c .")]
//         [DataRow(":-(+(a,b),true)", "'+'(a,b).")]
//         [DataRow(":-(+(a,b),true)", "('+'(a,b)).")]
//         [DataRow(":-(*(a,b),true)", "*(a,b).")]
//         [DataRow(":-(*(a,b),true)", "'*'(a,b).")]
//         [DataRow(":-(*(+(a,b),c),true)", "'*'('+'(a,b),c).")]
//         [DataRow(":-(xor(a,b),true)", "a xor b.")]
//         [DataRow(":-(xor_new(a,b),true)", "a xor_new b.")]
//         [DataRow(":-(dynamic(/(foo,2)),true)", ":- dynamic( foo/2 ).")]
//         [DataRow(":-(ensure_loaded(testEnsureLoaded.pl),true)", ":- ensure_loaded('testEnsureLoaded.pl').")]
//         public void ParsesStructureBinaryOperator(string expected, string test)
//         {
//             //expected.CanParse();
//             test.CanParse();
//         }
// 
//         [TestMethod]
//         [DataRow(":-(a,b)", "a :- b.")]
//         [DataRow(":-(a,=(X,b))", "a :- X = b.")]
//         [DataRow(":-(a,=(X,b))", "a :- X=b.")]
//         [DataRow(":-(a,,(b,,(c,d)))", "a :- b, c, d.")]
//         public void ParsesPredicates(string expected, string test)
//         {
//             //expected.CanParse();
//             test.CanParse();
//         }
// 
//         [TestMethod]
//         [DataRow(":-(a,b)", "%This is a line comment\r\na :- b.")]
//         [DataRow(":-(a,b)", "  %This is a line comment\r\n   a :- b.")]
//         [DataRow(":-(a,b)", "a :- b. %This is a line comment\r\n")]
//         [DataRow(":-(a,b)", "/**/ a :- b.")]
//         [DataRow(":-(a,b)", "/* This is a block comment */ a :- b.")]
//         [DataRow(":-(a,b)", "/*This is\r\n a multi\r\nline comment\r\n*/\r\na :- b.")]
//         public void ParsesCommentedCode(string expected, string test)
//         {
//             //expected.CanParse();
//             test.CanParse();
//         }
// 
//         [TestMethod]
//         [DataRow("a1. a2. a3.")]
//         [DataRow("foobar123_.\r\n a(Var) :- true. ")]
//         [DataRow("atom. atom.")]
//         [DataRow("aar. a :- atom.")]
//         [DataRow("aar. struct(Param, param2).")]
//         public void ParsesMany(string test)
//         {
//             test.CanParse();
//         }
// 
//         [TestMethod]
//         [DataRow("'a§b'.")]
//         [DataRow("a1.")]
//         [DataRow("a -_w_> a.")]
//         [DataRow("a op->erator a.")]
//         [DataRow("a1 :- b.")]
//         [DataRow("a2 :- b, c.")]
//         [DataRow("a3 :- b, c, d.")]
//         [DataRow("a4 :- b=c .")]
//         [DataRow("a5 :- b(c).")]
//         [DataRow("a6 :- b(c,d).")]
//         [DataRow("a7 :- b(c,d, e).")]
//         [DataRow("  b  . ")]
//         [DataRow(" b(c,d).")]
//         [DataRow(" b(c,d, e).")]
//         [DataRow("a+b.")]
//         [DataRow("a+b :- a=s, a.")]
//         [DataRow("a+b. c.")]
//         [DataRow("a+b. c. d:-e. \r\n f:-g=h.")]
//         [DataRow("a :-a=s,true,1+2.")]
//         [DataRow("a :- a=s, true,1+2.")]
//         [DataRow("a(c+d).")]
//         [DataRow("a(a, c+d).")]
//         [DataRow("a(a, b, c+d, (e+1)).")]
//         [DataRow("a(a, b, c+d) :- a=s, true,1+2.")]
//         [DataRow(":- op(900, xfx, =>).")]
//         [DataRow(":- op(900, fx, [abba]).")]
//         [DataRow(":- op(900, xfx, [=>]).")]
//         [DataRow(":- op(900, xfx, [=>, -->>, -+->]).")]
//         [DataRow("call(true).")]
//         [DataRow("a(X) :- X = true, call(X).")]
//         [DataRow("a :- [a, 2, B, c_].")]
//         [DataRow("a :- [].")]
//         [DataRow("a :-  [  ]  .")]
//         [DataRow("a :-  [ a, 2 ] .")]
//         [DataRow("a :- [B].")]
//         [DataRow("a :- [b;c].")]
//         [DataRow("a :- [b+c].")]
//         [DataRow("a :- X = [], Y = [a], Z = [a, b, 1], [C, a] = [2, a], C=X.")]
//         [DataRow("a :- [[a, 2], B].")]
//         [DataRow("a :- [[a, 2], B, c].")]
//         [DataRow("a :- [[a, 2], [B], []].")]
//         [DataRow("a :- [b|[]].")]
//         [DataRow("a :- [[]|b].")]
//         [DataRow("a :- [b|[c]].")]
//         [DataRow("a :- [b|c].")]
//         [DataRow("a :- [a, b|c].")]
//         [DataRow("a :- [[]|[]].")]
//         [DataRow("a([a+b,c|d]) :- [X,Y|Z] = [1,2,3,4,5,6].")]
//         [DataRow("a :- a ; b.")]
//         [DataRow("a :- a ; b ; c.")]
//         [DataRow("a :- a ; b , c, d;e;f;g,h.")]
//         [DataRow("a :- (a ; b), c, d, (e;f;f).")]
//         [DataRow("a :- (e;f;(g,h), (i;j; [true])).")]
//         [DataRow("a :- (1+2=3 -> call(true) ; call(false)).")]
//         [DataRow("a :- ((1+2==3) -> call(true) ; call(false)).")]
//         [DataRow("a :- {a}.")]
//         [DataRow("a :- {a, b}.")]
//         [DataRow("a :- {a; b}.")]
//         [DataRow("a :- {a+b}.")]
//         [DataRow("a :- {a, b, c}.")]
//         [DataRow("a :- {}.")]
//         [DataRow("a :- !,a.")]
//         [DataRow("a :- a,!.")]
//         [DataRow("a :- a,!,c.")]
//         [DataRow("a :- fail.")]
//         [DataRow("a :- X is 2+2.")]
//         [DataRow("a :- X is 2.")]
//         [DataRow("a :- X is 'is'.")]
//         [DataRow("a :- X is xor.")]
//         [DataRow("a :- X is +.")]
//         [DataRow("a :- X is is.")]
//         [DataRow("a :- {x:1, y:2}.")]
//         [DataRow("a :- point{x:1, y:2}.")]
//         [DataRow("a :- \"abc d - 1,\'. :- sd\".")]
//         [DataRow("a :- \"\".")]
//         [DataRow("a :- \" \".")]
//         [DataRow(
//             "a :- X=1.\r\n a :- X=2. \r\n b :- true. \r\n a :- X=3. a() :- X=4. a(1,2):-X=5. a(1):-X=5. a((a,b,c)):-X=5. a.")]
//         [DataRow("a :- X = 2.1234.")]
//         [DataRow("a :- (1 + 2) * 2.")]
//         [DataRow("a :- 2.0 + 40.")]
//         [DataRow("a :- -1 + 2.")]
//         [DataRow("a :- -12.23230 -40.")]
//         [DataRow("a :- -12.23230 - 40.")]
//         [DataRow("a :- -12.23230 -4.001.")]
//         [DataRow("a :- -12.23230 - 0.001.")]
//         [DataRow("a :- 0.00001 + -40.")]
//         [DataRow("a :- X = 1 + +29.")]
//         [DataRow("a :- X = 1 + (-40) + -(40) + (1).")]
//         [DataRow("a :- X=''.")]
//         [DataRow("a :- X='single quoted string', nl.")]
//         [DataRow("a :- \\+(a = b, c = d).")]
//         [DataRow("a :- (a = b, c = d ; p = t).")]
//         [DataRow("a :- \\+ a.")]
//         [DataRow("a :- \\+a.")]
//         [DataRow("a :- \\+ (a).")]
//         [DataRow("a :- \\+ (a,b,c).")]
//         [DataRow("a :- \\+(a,b,c).")]
//         [DataRow("a :- + (a,b).")]
//         [DataRow("a :- +(a,b).")]
//         [DataRow("a :- f (a,b).")]
//         [DataRow("a :- f(a,b).")]
//         [DataRow("a :- a().")]
//         [DataRow("a :- (a()).")]
//         [DataRow("a/0.")]
//         [DataRow("a/1.")]
//         [DataRow("a :- 1 + (2).")]
//         [DataRow("a :- 1 + -(2).")]
//         [DataRow("a :- 1 -(2).")]
//         [DataRow("a :- 1 - (2).")]
//         [DataRow("a. a :- X=2. a:-true.")]
//         [DataRow("a :- X=2.")]
//         [DataRow("aaa(X) :- X=1. bbb(X,a(b),1+3):-true. a([1,3|b]). bbb(X,2,X). aaa(5) :- fail. aaa(Y) :- Y=a.")]
//         [DataRow("a. a :- fail. a :- X=1,true.")]
//         [DataRow("a :- asserta((bar(X) :- X)), clause(bar(X), B). a(b) :- c.")]
//         [DataRow("a(GAR, foobar(DEDAL), [KAIN, ABEL], banan) :-GAR = IAR, IAR = [DEDAL], DEDAL = abc.")]
//         [DataRow("a :- atom_chars(A,['p','r','o','l','o','g']), A = 'prolog'.")]
//         [DataRow("a :- atom_chars([],L), L=['[',']'].")]
//         [DataRow("a :- ['n', s, 3] = [n|[Y, Z]], Y = s, Z = 3.")]
//         [DataRow("a :- bagof(f(X,Y),(X=a;Y=b),L), L = [f(a, _), f(_, b)].")]
//         [DataRow("a :- bagof(X,Y^Z,L).")]
//         [DataRow("a :- findall(X+Y,(X=1),S), S=[1+_].")]
//         [DataRow("a :- '->'(true, (X=1; X=2)), (X=1;X=2).")]
//         [DataRow("a :- X = 1 + 2, 'is'(Y, X * 3), X = 1+2, Y=9.")]
//         [DataRow("a :- once(!), (X=1; X=2), (X=1;X=2).")]
//         [DataRow("a :- ((X=1, !); X=2), X=1.")]
//         [DataRow("a :- (!; call(3)).")]
//         [DataRow("a :- retract((atom(_) :- X == '[]')).")]
//         [DataRow("a :- setof(X, X ^ (true; 4), L).")]
//         [DataRow("a(F) :- F =..[a|F2].")]
//         [DataRow(
//             "a :- '='(f(A, B, C), f(g(B, B), g(C, C), g(D, D))), A=g(g(g(D, D), g(D, D)), g(g(D, D), g(D, D))), B=g(g(D, D), g(D, D)), C=g(D, D).")]
//         [DataRow("a :- once(!).")]
//         [DataRow("a :- (X=1; X=2), (X=1;X=2).")]
//         [DataRow("a :- (X=1; X=2).")]
//         [DataRow("a :- (X=1;X=2).")]
//         [DataRow("a :- (X=1;X=2); (a).")]
//         [DataRow("a :- (X=1; X=2), (a).")]
//         [DataRow("a :- X = +(1,2), Y = + (1,2), X=Y.")]
//         [DataRow("a :- X = +(1,2,3,4), Y = + (1,2,3,4), X=Y.")]
//         [DataRow("aaa(X) :- X=1. bbb(X,Y,Z):-true. aaa(Y) :- fail. aaa(Y) :- Y=a.")]
//         public void CanParsePredicates(string test)
//         {
//             test.CanParse();
//         }
// 
//         [TestMethod]
//         public void MultiHeadRecursive()
//         {
//             string def = @"
// %% This is a comment
// % This is a comment line 2
// fooBar(X).
// 
// % Baz comm
// baz(a).
// 
// baz(F).
// 
// concatenate([], L, L).
// 
// % More comment
// % Second line
// concatenate([X|L1], L2, [X|L3]) :-
//     concatenate(L1, L2, L3).";
// 
//             "concatenate([], [], [])".True(def);
//             "concatenate([a,b], [c,d], [a,b,c,d])".True(def);
//             "concatenate([a,b], [], [a,b])".True(def);
//             "concatenate([], [c], [c])".True(def);
//             "concatenate([], [c,d], [c,d])".True(def);
//             "concatenate([s], [], [s])".True(def);
//             "concatenate([a,b], [], [a,b])".True(def);
//         }
// 
//         [TestMethod]
//         public void MultiHeadRecursiveCall()
//         {
//             string def = @"
// concatenate([], L, L).
// concatenate([X|L1], L2, [X|L3]) :-
//     concatenate(L1, L2, L3).";
// 
//             "call(concatenate([], [], []))".True(def);
//             "call(concatenate([a,b], [c,d], [a,b,c,d]))".True(def);
//             "call(concatenate([a,b], [], [a,b]))".True(def);
//             "call(concatenate([], [c], [c]))".True(def);
//             "call(concatenate([], [c,d], [c,d]))".True(def);
//             "call(concatenate([s], [], [s]))".True(def);
//             "call(concatenate([a,b], [], [a,b]))".True(def);
//         }
// 
//         [TestMethod]
//         public void SubAtom()
//         {
//             //TODO
//             "sub_atom(abracadabra, 0, 5, _, S2), S2='abrac'".True();
// 
//             @"test([])".False(@"test(X):- '\='(X, []).");
//             @"test(1)".True(@"test(X):- '\='(X, []).");
//             @"true".True(@"test(X):- X \= [].");
//             @"atom(atom)".True(
//                 "foo(GAR, foobar(DEDAL), [KAIN, ABEL], banan) :- GAR = IAR, IAR = [DEDAL], DEDAL = abc.");
// 
//             /*
//             'sub_atom', [sub_atom(abracadabra, _, 5, 0, S2), [[S2<-- 'dabra']]]).
// 'sub_atom', [sub_atom(abracadabra, 3, Length, 3, S2), [[Length<-- 5, S2<-- 'acada']]]).
// 'sub_atom', [sub_atom(abracadabra, Before, 2, After, ab),
// 
//                      [[Before <-- 0, After <-- 9],
//                      [Before<-- 7, After<-- 2]]]).
// 'sub_atom', [sub_atom('Banana', 3, 2, _, S2), [[S2<-- 'an']]]).
// 'sub_atom', [sub_atom('charity', Before, 3, After, S2),
// 
//             [[Before <-- 0, After <-- 4, S2 <-- 'cha'],
//                      [Before<-- 1, After<-- 3, S2<-- 'har'],
//                      [Before<-- 2, After<-- 2, S2<-- 'ari'],
//                      [Before<-- 3, After<-- 1, S2<-- 'rit'],
//                      [Before<-- 4, After<-- 0, S2<-- 'ity']]]).
// 'sub_atom', [sub_atom('ab', Before, Length, After, Sub_atom),
// 
//                     [[Before <-- 0, Length <-- 0, After <-- 2, Sub_atom <-- ''],
//                      [Before<-- 0, Length<-- 1, After<-- 1, Sub_atom<-- 'a'],
//                      [Before<-- 0, Length<-- 2, After<-- 0, Sub_atom<-- 'ab'],
//                      [Before<-- 1, Length<-- 0, After<-- 1, Sub_atom<-- ''],
//                      [Before<-- 1, Length<-- 1, After<-- 0, Sub_atom<-- 'b'],
//                      [Before<-- 2, Length<-- 0, After<-- 0, Sub_atom<-- '']]]).
// 'sub_atom', [sub_atom(Banana, 3, 2, _, S2), instantiation_error]).
// 'sub_atom', [sub_atom(f(a), 2, 2, _, S2), type_error(atom, f(a))]).
// 'sub_atom', [sub_atom('Banana', 4, 2, _, 2), type_error(atom, 2)]).
// 'sub_atom', [sub_atom('Banana', a, 2, _, S2), type_error(integer, a)]).
// 'sub_atom', [sub_atom('Banana', 4, n, _, S2), type_error(integer, n)]).
// 'sub_atom', [sub_atom('Banana', 4, _, m, S2), type_error(integer, m)]).
//              */
//         }
// 
//         [TestMethod]
//         public void SetPrologFlag()
//         {
//             "set_prolog_flag(unknown, fail), current_prolog_flag(unknown, V), V=fail".True();
//             /*
// 'set_prolog_flag', [set_prolog_flag(X, warning), instantiation_error]).
// 'set_prolog_flag', [set_prolog_flag(5, decimals), type_error(atom,5)]).
// 'set_prolog_flag', [set_prolog_flag(date, 'July 1999'), domain_error(prolog_flag,date)]).
// 'set_prolog_flag', [set_prolog_flag(debug, no), domain_error(flag_value,debug+no)]).
// 'set_prolog_flag', [set_prolog_flag(max_arity, 40), permission_error(modify, flag, max_arity)]).
// 'set_prolog_flag', [set_prolog_flag(double_quotes, atom), success]).
// %'set_prolog_flag', [X = ""fred"", [[X <-- fred]]]).
// %Use read/2 because the fileContents predicate is already parsed.
// 'set_prolog_flag', [read(""\""fred\"". "", X), [[X<-- fred]]]).
// 'set_prolog_flag', [set_prolog_flag(double_quotes, chars), success]).
// %'set_prolog_flag', [X = ""fred"", [[X<-- [f, r, e, d]]]]).
// 'set_prolog_flag', [read(""\""fred\"". "", X), [[X<-- [f, r, e, d]]]]).
// 'set_prolog_flag', [set_prolog_flag(double_quotes, codes), success]).
// %'set_prolog_flag', [X = ""fred"", [[X<-- [102,114,101,100]]]]).
// 'set_prolog_flag', [read(""\""fred\"". "", X), [[X<-- [102,114,101,100]]]]).
//              */
//         }
// 
//         [TestMethod]
//         public void NumberChars()
//         {
//             //TODO
//             "number_chars(33,L), L = ['3', '3']".True();
//             "number_chars(33,['3','3'])".True();
//             "number_chars(33.0,L), L =['3', '3', '.', '0]".True();
//             "number_chars(X,['3','.','3','E','+','0']), X=3.3".True();
//             "number_chars(3.3,['3','.','3'])".True();
// 
//             string d = @"'number_chars', [number_chars(A,['-','2','5']), [[A <-- (-25)]]]).
// 'number_chars', [number_chars(A,['\n',' ','3']), [[A <-- 3]]]).
// 'number_chars', [number_chars(A,['3',x]), syntax_error(_)]).
// 'number_chars', [number_chars(A,['0',x,f]), [[A <-- 15]]]).
// 'number_chars', [number_chars(A,['0','''','A']), [[A <-- 65]]]).
// 'number_chars', [number_chars(A,['4','.','2']), [[A <-- 4.2]]]).
// 'number_chars', [number_chars(A,['4','2','.','0','e','-','1']), [[A <-- 4.2]]]).
// 'number_chars', [number_chars(A,L), instantiation_error]).
// 'number_chars', [number_chars(a,L), type_error(number, a)]).
// 'number_chars', [number_chars(A,4), type_error(list, 4)]).
// 'number_chars', [number_chars(A,['4',2]), type_error(character, 2)]).
// %'number_codes', [number_codes(33,L), [[L <-- [0'3,0'3]]]]).
// %'number_codes', [number_codes(33,[0'3,0'3]), success]).
// 'number_codes', [number_codes(33.0,L), [[L <-- [51,51,46,48]]]]) :- intAndFloatAreDifferent.
// %'number_codes', [number_codes(33.0,[0'3,0'3,0'.,0'0]), success]) :- intAndFloatAreDifferent.
// %'number_codes', [number_codes(A,[0'-,0'2,0'5]), [[A <-- (-25)]]]).
// %'number_codes', [number_codes(A,[0' ,0'3]), [[A <-- 3]]]).
// %'number_codes', [number_codes(A,[0'0,0'x,0'f]), [[A <-- 15]]]).
// %'number_codes', [number_codes(A,[0'0,39,0'a]), [[A <-- 97]]]).
// %'number_codes', [number_codes(A,[0'4,0'.,0'2]), [[A <-- 4.2]]]).
// %'number_codes', [number_codes(A,[0'4,0'2,0'.,0'0,0'e,0'-,0'1]), [[A <-- 4.2]]]).
// 'number_codes', [number_codes(A,L), instantiation_error]).
// 'number_codes', [number_codes(a,L), type_error(number,a)]).
// 'number_codes', [number_codes(A,4), type_error(list,4)]).
// %'number_codes', [number_codes(A,[ 0'1, 0'2, '3']), representation_error(character_code)]).";
//         }
// 
//         [TestMethod]
//         public void AtomCodes()
//         {
//             //TODO
//             "atom_codes('',L), L=[]".True();
// 
//             /*
// %'atom_codes', [atom_codes([],L), [[L <-- [ 0'[, 0'] ]]]]).
// 'atom_codes', [atom_codes('''',L), [[L <-- [ 39 ]]]]).
// %'atom_codes', [atom_codes('iso',L), [[L <-- [ 0'i, 0's, 0'o ]]]]).
// %'atom_codes', [atom_codes(A,[ 0'p, 0'r, 0'o, 0'l, 0'o, 0'g]), [[A <-- 'prolog']]]).
// %'atom_codes', [atom_codes('North',[0'N | L]), [[L <-- [0'o, 0'r, 0't, 0'h]]]]).
// %'atom_codes', [atom_codes('iso',[0'i, 0's]), failure]).
// 'atom_codes', [atom_codes(A,L), instantiation_error]).
// 'atom_codes', [atom_codes(f(a),L), type_error(atom,f(a))]).
// %'atom_codes', [atom_codes(A, 0'x), type_error(list,0'x)]).
// %'atom_codes', [atom_codes(A,[ 0'i, 0's, o]), representation_error(character_code)]).
//              */
//         }
//     }
// }