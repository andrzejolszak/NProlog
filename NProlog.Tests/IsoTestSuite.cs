namespace CSPrologTest
{
    [TestClass]
    public class IsoTestSuite
    {
        [TestMethod]
        [DataRow(@"R: abolish(abolish/1)")]
        [DataRow(@"R: abolish(foo/a)")]
        [DataRow(@"R: abolish(foo/(-1))")]
        [DataRow(@"R: current_prolog_flag(max_arity,A), X is A + 1, abolish(foo/X)")]
        [DataRow(@"R: abolish(5/2)")]
        public void Abolish(string test)
        {
            test.Evaluate();
        }

        [TestMethod]
        [DataRow(@"F: X=1, var(X)")]
        [DataRow(@"T: var(X), X=1")]
        [DataRow(@"F: fail, call(3)")]
        [DataRow(@"F: nofoo(X), call(X)")]
        [DataRow(@"T: X = true, call(X)")]
        public void And(string test)
        {
            test.Evaluate();
        }

        [TestMethod]
        [DataRow(@"T: arg(1,foo(a,b),a)")]
        [DataRow(@"T: arg(1,foo(a,b),X), X=a")]
        [DataRow(@"T: arg(1,foo(a,b),X)")]
        [DataRow(@"T: arg(1,foo(X,b),a), X=a")]
        [DataRow(@"T: arg(1,foo(X,b),a)")]
        [DataRow(@"T: arg(2,foo(a, f(X,b), c), f(a, Y)), X=a, Y=b")]
        [DataRow(@"T: arg(1,foo(X,b),Y), X=Y")]
        [DataRow(@"T: arg(1,foo(X,b),Y), X=a, Y=a")]
        [DataRow(@"F: arg(1,foo(a,b),b)")]
        [DataRow(@"F: arg(0,foo(a,b),foo)")]
        [DataRow(@"F: arg(3,foo(3,4),N)")]
        [DataRow(@"R: arg(X,foo(a,b),a)")]
        [DataRow(@"R: arg(0,atom,A)")]
        [DataRow(@"R: arg(0,3,A)")]
        [DataRow(@"R: arg(-3,foo(a,b),A)")]
        [DataRow(@"R: arg(a,foo(a,b),X)")]
        [DataRow(@"T3: arg(Position,f(a,b,c),Term)")]
        [DataRow(@"T1: arg(Position,f(a,b,c),b)")]
        public void Arg(string test)
        {
            test.Evaluate();
        }

        [TestMethod]
        [DataRow(@"T: '=\='(0,1)")]
        [DataRow(@"F: '=\='(1.0,1)")]
        [DataRow(@"F: '=\='(3 * 2,7 - 1)")]
        [DataRow(@"R: '=\='(N,5)")]
        [DataRow(@"R: '=\='(floot(1),5)")]
        public void ArithDiff(string test)
        {
            test.Evaluate();
        }

        [TestMethod]
        [DataRow(@"F: '=:='(0,1)")]
        [DataRow(@"T: '=:='(1.0,1)")]
        [DataRow(@"T: '=:='(3 * 2,7 - 1)")]
        [DataRow(@"R: '=:='(N,5)")]
        [DataRow(@"R: '=:='(floot(1),5)")]
        public void ArithEq(string test)
        {
            test.Evaluate();
        }

        [TestMethod]
        [DataRow(@"F: '>'(0,1)")]
        [DataRow(@"F: '>'(1.0,1)")]
        [DataRow(@"F: '>'(3*2,7-1)")]
        [DataRow(@"R: '>'(X,5)")]
        [DataRow(@"R: '>'(2 + floot(1),5)")]
        public void ArithGt(string test)
        {
            test.Evaluate();
        }

        [TestMethod]
        [DataRow(@"F: '>='(0,1)")]
        [DataRow(@"T: '>='(1.0,1)")]
        [DataRow(@"T: '>='(3*2,7-1)")]
        [DataRow(@"R: '>='(X,5)")]
        [DataRow(@"R: '>='(2 + floot(1),5)")]
        public void ArithGtEq(string test)
        {
            test.Evaluate();
        }

        [TestMethod]
        [DataRow(@"T: '<'(0,1)")]
        [DataRow(@"F: '<'(1.0,1)")]
        [DataRow(@"F: '<'(3*2,7-1)")]
        [DataRow(@"R: '<'(X,5)")]
        [DataRow(@"R: '<'(2 + floot(1),5)")]
        public void ArithLt(string test)
        {
            test.Evaluate();
        }

        [TestMethod]
        [DataRow(@"T: '=<'(0,1)")]
        [DataRow(@"T: '=<'(1.0,1)")]
        [DataRow(@"T: '=<'(3*2,7-1)")]
        [DataRow(@"R: '=<'(X,5)")]
        [DataRow(@"R: '=<'(2 + floot(1),5)")]
        public void ArithLtEq(string test)
        {
            test.Evaluate();
        }

        [TestMethod]
        [DataRow(@"T: asserta((bar(X) :- X)), clause(bar(X), B)")]
        [DataRow(@"R: asserta(_)")]
        [DataRow(@"R: asserta(4)")]
        [DataRow(@"R: asserta((foo :- 4))")]
        [DataRow(@"R: asserta((atom(_) :- true))")]
        public void Asserta(string test)
        {
            test.Evaluate();
        }

        [TestMethod]
        [DataRow(@"R: assertz((foo(X) :- X -> call(X)))")]
        [DataRow(@"R: assertz(_)")]
        [DataRow(@"R: assertz(4)")]
        [DataRow(@"R: assertz((foo :- 4))")]
        [DataRow(@"R: assertz((atom(_) :- true))")]
        public void Assertz(string test)
        {
            test.Evaluate();
        }

        [TestMethod]
        [DataRow(@"T: atom('string')")]
        [DataRow(@"F: atom(Var)")]
        [DataRow(@"F: atom(a(b))")]
        [DataRow(@"F: atom([])")]
        [DataRow(@"F: atom(6)")]
        [DataRow(@"F: atom(3.3)")]
        public void Atom(string test)
        {
            test.Evaluate();
        }

        [TestMethod]
        [DataRow(@"T: atom_chars('',L), L=[]")]
        [DataRow(@"T: atom_chars('iso',L), L=['i', 's', 'o']")]
        [DataRow(@"T: atom_chars(A,['p','r','o','l','o','g']), A = 'prolog'")]
        [DataRow(@"F: atom_chars('iso',['i','s'])")]
        [DataRow(@"R: atom_chars(A,L)")]
        [DataRow(@"R: atom_chars(A,[a,E,c])")]
        [DataRow(@"R: atom_chars(A,[a,b|L])")]
        [DataRow(@"R: atom_chars(f(a),L)")]
        [DataRow(@"R: atom_chars(A,iso)")]
        [DataRow(@"R: atom_chars(A,[a,f(b)])")]
        [DataRow(@"R: atom_chars(X,['1','2']), Y is X + 1")]
        [DataRow(@"T: atom_chars('North',['N'|X]), X = ['o','r','t','h']")]
        [DataRow(@"R: atom_chars([],L), L=['[',']']")]
        [DataRow(@"T: atom_chars('''',L), L=['''']")]
        public void AtomChars(string test)
        {
            test.Evaluate();
        }

        [TestMethod]
        [DataRow(@"T: [1, 2, 3] = [X|Y], X = 1, Y = [2, 3]")]
        [DataRow(@"T: [1, s] = [X|Y], X = 1, Y = [s]")]
        [DataRow(@"T: [1, s] = [X|[Y]], X = 1, Y = s")]
        [DataRow(@"T: [1, s, 3] = [X|[Y, Z]], X = 1, Y = s, Z = 3")]
        [DataRow(@"T: ['N', s, 3] = ['N'|[Y, Z]], Y = s, Z = 3")]
        [DataRow(@"T: [n, s, 3] = ['n'|[Y, Z]], Y = s, Z = 3")]
        [DataRow(@"T: ['n', s, 3] = [n|[Y, Z]], Y = s, Z = 3")]
        public void ListSyntax(string test)
        {
            test.Evaluate();
        }

        [TestMethod]
        [DataRow(@"T: atom_concat('hello',' world',A), A = 'hello world'")]
        [DataRow(@"T: atom_concat('hello','world',A), A = helloworld")]
        [DataRow(@"T: atom_concat(T,' world','small world'), T = 'small'")]
        [DataRow(@"T: atom_concat('small', T, 'small world'), T = ' world'")]
        [DataRow(@"F: atom_concat(hello,' world','small world')")]
        [DataRow(@"T: atom_concat(T1,T2,'hello'),T1 = he, T2=llo")]
        [DataRow(@"R: atom_concat(A1,'iso',A3)")]
        [DataRow(@"R: atom_concat('iso',A2,A3)")]
        [DataRow(@"R: atom_concat(f(a),'iso',A3)")]
        [DataRow(@"R: atom_concat('iso',f(a),A3)")]
        [DataRow(@"R: atom_concat(A1,A2,f(a))")]
        public void AtomConcat(string test)
        {
            test.Evaluate();
        }

        [TestMethod]
        [DataRow(@"T1: member(X, [1,2,3]), X = 3")]
        [DataRow(@"T3: member(X, [1,2,3])")]
        [DataRow(@"T2: member(1, [1,2,1])")] // t;t
        [DataRow(@"T2: member(1, [1,2,1,3])")] //t;t;f
        [DataRow(@"T2: L=[1,X,1,3,Y],member(foo,L)")] //t;t
        [DataRow(@"T1: member(3, [1,2,3])")]
        [DataRow(@"T1: member(1,[3,4,2,1,6|stop])")] // t;f
        [DataRow(@"F: member(3, [1,2,4])")]
        [DataRow(@"F: member(3, [])")]
        // TODO: [DataRow(@"T: member(3, X), X = [1|Y]")]
        // [DataRow(@"T: member(3, X), X = [Z, 1|Y]")]
        // [DataRow(@"T: member(P, X), P = 1, X = [Z, 1|Y]")]
        public void Member(string test)
        {
            test.Evaluate();
        }

        [TestMethod]
        [DataRow(@"F: memberchk(X, [1,2,3]), X = 3")]
        [DataRow(@"T1: memberchk(X, [1,2,3])")]
        [DataRow(@"T1: memberchk(1, [1,2,1])")]
        [DataRow(@"T1: memberchk(1, [1,2,1,3])")]
        [DataRow(@"T1: L=[1,X,1,3,Y],memberchk(foo,L)")]
        [DataRow(@"T1: memberchk(3, [1,2,3])")]
        [DataRow(@"T1: memberchk(1,[3,4,2,1,6|stop])")]
        [DataRow(@"F: memberchk(3, [1,2,4])")]
        [DataRow(@"F: memberchk(3, [])")]
        [DataRow(@"F: memberchk(3, X), X = [1|Y]")]
        [DataRow(@"T1: memberchk(3, X), X = [Z, 1|Y]")]
        [DataRow(@"T1: memberchk(P, X), P = 1, X = [Z, 1|Y]")]
        public void Memberchk(string test)
        {
            test.Evaluate();
        }


        [TestMethod]
        [DataRow(@"T: atom_length('enchanted evening', N), N = 17")]
        [DataRow(@"T: atom_length('', N), N=0")]
        [DataRow(@"F: atom_length('scarlet', 5)")]
        [DataRow(@"R: atom_length(Atom, 4)")]
        [DataRow(@"T: atom_length(1.23, 4)")]
        [DataRow(@"R: atom_length(atom, '4')")]
        [DataRow(@"T: X = abc, atom_length(X, 3)")]
        public void AtomLength(string test)
        {
            test.Evaluate();
        }

        [TestMethod]
        [DataRow(@"T: atomic(atom)")]
        [DataRow(@"F: atomic(a(b))")]
        [DataRow(@"F: atomic(Var)")]
        [DataRow(@"T: atomic([])")]
        [DataRow(@"T: atomic(6)")]
        [DataRow(@"T: atomic(3.3)")]
        public void Atomic(string test)
        {
            test.Evaluate();
        }

        [TestMethod]
        [DataRow(@"T: bagof(X,(X=1;X=2),L), L = [1, 2]")]
        [DataRow(@"T: bagof(X,(X=1;X=2),X), X = [1, 2]")]
        [DataRow(@"T: bagof(X,(X=Y;X=Z),L)")]
        [DataRow(@"F: bagof(X,fail,L)")]
        [DataRow(@"T: bagof(1,(Y=1;Y=2),L), L = [1, 1]")]
        [DataRow(@"T: bagof(f(X,Y),(X=a;Y=b),L), L = [f(a, _), f(_, b)]")]
        [DataRow(@"T: bagof(X,Y^((X=1,Y=1);(X=2,Y=2)),S), S = [1, 2]")]
        [DataRow(@"T: bagof(X,Y^((X=1;Y=1);(X=2,Y=2)),S), S = [1, _, 2]")]
        [DataRow(@"T: bagof(X,(Y^(X=1;Y=1);X=3),S)), S = [3]")]
        [DataRow(@"T: bagof(X,(X=Y;X=Z;Y=1),L), (L = [Y, Z]; (L=[_], Y=1))")]
        [DataRow(@"R: bagof(X,Y^Z,L)")]
        [DataRow(@"R: bagof(X,1,L)")]
        [DataRow(@"R: findall(X,call(4),S)")]
        public void Bagof(string test)
        {
            test.Evaluate();
        }

        [TestMethod]
        [DataRow(@"T: call(!)")]
        [DataRow(@"F: call(fail)")]
        [DataRow(@"F: call((fail, X))")]
        [DataRow(@"F: call((fail, call(1)))")]
        [DataRow(@"R: call((write(3), X))")]
        [DataRow(@"R: call((write(3), call(1)))")]
        [DataRow(@"R: call(X)")]
        [DataRow(@"R: call(1)")]
        [DataRow(@"R: call((fail, 1))")]
        [DataRow(@"R: call((write(3), 1))")]
        [DataRow(@"R: call((1; true))")]
        public void Call(string test)
        {
            test.Evaluate();
        }

        [TestMethod]
        [DataRow(@"R: catch(true, C, write('something')), throw(blabla)")]
        [DataRow(@"F: catch(foo(abc,L), error, fail)")]
        public void CatchAndThrow(string test)
        {
            test.Evaluate();
        }

        [TestMethod]
        [DataRow(@"T: char_code(a,Code)")]
        [DataRow(@"T: char_code(Char,99)")]
        [DataRow(@"T: char_code(b,98)")]
        [DataRow(@"R: char_code('ab',Code)")]
        [DataRow(@"R: char_code(a,x)")]
        [DataRow(@"R: char_code(Char,Code)")]
        [DataRow(@"R: char_code(Char,-2)")]
        public void CharCode(string test)
        {
            test.Evaluate();
        }

        [TestMethod]
        [DataRow(@"F: clause(x,Body)")]
        [DataRow(@"R: clause(_,B)")]
        [DataRow(@"R: clause(4,B)")]
        [DataRow(@"F: clause(f(_),5)")]
        [DataRow(@"R: clause(atom(_),Body)")] //
        [DataRow(@"F: clause(natnum, X)")]
        [DataRow(@"F: clause(single(0), _)")]
        [DataRow(@"T: clause(single(1), true)")]
        [DataRow(@"T: clause(single(X), true), X = 1")]
        [DataRow(@"T: clause(natnum(0), true)")]
        [DataRow(@"T: clause(natnum(1), true)")]
        [DataRow(@"F: clause(natnum(2), _)")]
        [DataRow(@"T: clause(natnum(X), true), X = 0")]
        [DataRow(@"T: clause(natnum(X), true), X = 1")]
        [DataRow(@"T: clause(natnum(X), natnum(Z)), X = s(Z)")]
        [DataRow(@"T: clause(natnum(X), F), X = s(Z)")]
        [DataRow(@"T: clause(natnum(s(X)), natnum(X))")]
        [DataRow(@"T: clause(natnum(s(Y)), natnum(Y))")]
        [DataRow(@"T: clause(natnum(s(1)), natnum(1))")]
        [DataRow(@"T: clause(natnum(s(X, 1)), (natnum(X), true, Z = C, natnum(1)))")]
        [DataRow(@"T: clause(natnum(s(X, Y)), (natnum(X), true, Z = C, natnum(1))), Y = 1")]
        [DataRow(@"T: clause(natnum0(s(0)), X), X = (natnum(0), true, Y = Z)")]
        [DataRow(@"T: clause(natnum0(s(s(0))), X), X = (natnum(s(0)), true, Y = Z)")]
        [DataRow(@"T: A = s(0), clause(natnum0(A), X), X = (natnum(0), true, Y = Z)")]
        [DataRow(@"T: A = s(s(0)), clause(natnum0(A), X), X = (natnum(s(0)), true, Y = Z)")]
        [DataRow(@"T: A = s(s(0)), B = natnum0(A), clause(B, X), X = (natnum(s(0)), true, Y = Z)")]
        [DataRow(@"T: clause(banan(P), R), R = (X = 3)")]
        [DataRow(@"T: clause(banan(P), R), R = (clause(banan(Z), X))")]
        public void Clause(string test)
        {
            string consult =
@"
natnum0(0).
natnum0(s(X)) :- natnum(X), true, Y = Z.

banan(X) :- clause(banan(Z), X).
banan(X) :- X = 3.

single(1).
natnum(0) :- true.
natnum(1).
natnum(s(X)) :- natnum(X).
natnum(s(X, 1)) :- natnum(X), true, Z = C, natnum(1).
";
            test.Evaluate(consult);
        }

        [TestMethod]
        [DataRow(@"F: compound(33.3)")]
        [DataRow(@"F: compound(-33.3)")]
        [DataRow(@"T: compound(-a)")]
        [DataRow(@"F: compound(_)")]
        [DataRow(@"F: compound(a)")]
        [DataRow(@"T: compound(a(b))")]
        [DataRow(@"T: compound([a])")]
        public void Compound(string test)
        {
            test.Evaluate();
        }

        [TestMethod]
        [DataRow(@"T: copy_term(X,Y)")]
        [DataRow(@"T: copy_term(X,3)")]
        [DataRow(@"T: copy_term(_,a)")]
        [DataRow(@"T: copy_term(a+X,X+b), X = a")]
        [DataRow(@"T: copy_term(_,_)")]
        [DataRow(@"T: copy_term(X+X+Y,A+B+B), A = B")]
        [DataRow(@"T: copy_term(X+X+Y,A+B+B), A=1, B=1")]
        [DataRow(@"T: copy_term(a,a)")]
        [DataRow(@"F: copy_term(a,b)")]
        [DataRow(@"T: copy_term(f(a),f(X)), X=a")]
        [DataRow(@"F: copy_term(a+X,X+b),copy_term(a+X,X+b)")]
        public void CopyTerm(string test)
        {
            test.Evaluate();
        }

        [TestMethod]
        [DataRow(@"T: current_predicate(current_predicate/1)")]
        [DataRow(@"F: current_predicate(nofoo/1)")]
        [DataRow(@"T: current_predicate(copy_term/2)")]
        [DataRow(@"T: functor(copy_term(a,a), Name, _),current_predicate(Name/2)")]
        [DataRow(@"R: current_predicate(4)")]
        [DataRow(@"R: current_predicate(dog)")]
        [DataRow(@"R: current_predicate(0/dog)")]
        [DataRow(@"T: current_predicate(X)")]
        public void CurrentPredicate(string test)
        {
            test.Evaluate();
        }

        [TestMethod]
        [DataRow(@"T: !")]
        [DataRow(@"F: !,fail;true")]
        [DataRow(@"T: call(!),fail;true")]
        public void Cut(string test)
        {
            test.Evaluate();
        }

        [TestMethod]
        [DataRow(@"F: fail")]
        [DataRow(@"F: undef_pred")]
        [DataRow(@"T: \+ fail")]
        public void Fail(string test)
        {
            test.Evaluate();
        }

        [TestMethod]
        [DataRow(@"T: findall(X,(X=1 ; X=2),S), S=[1,2]")]
        [DataRow(@"T: findall(X+Y,(X=1),S), S=[1+_]")]
        [DataRow(@"T: findall(X,fail,L), L=[]")]
        [DataRow(@"T: findall(X,(X=1 ; X=1),S), S=[1,1]")]
        [DataRow(@"F: findall(X,(X=2 ; X=1),[1,2])")]
        [DataRow(@"T: findall(X,(X=1 ; X=2),[X,Y]), X=1, Y=2")]
        [DataRow(@"R: findall(X,Goal,S)")]
        [DataRow(@"R: findall(X,4,S)")]
        [DataRow(@"R: findall(X,call(1),S)")]
        public void Findall(string test)
        {
            test.Evaluate();
        }

        [TestMethod]
        [DataRow(@"T: float(3.3)")]
        [DataRow(@"T: float(-3.3)")]
        [DataRow(@"F: float(3)")]
        [DataRow(@"F: float(atom)")]
        [DataRow(@"F: float(X)")]
        public void Float(string test)
        {
            test.Evaluate();
        }

        [TestMethod]
        [DataRow(@"T: functor(foo(a,b,c),foo,3)")]
        [DataRow(@"T: functor(foo(a,b,c),X,Y), X=foo, Y=3")]
        [DataRow(@"T: functor(X,foo,3), X=foo(A, B, C)")]
        [DataRow(@"T: functor(X,foo,0), X=foo")]
        [DataRow(@"T: functor(mats(A,B),A,B), A=mats, B=2")]
        [DataRow(@"F: functor(foo(a),foo,2)")]
        [DataRow(@"F: functor(foo(a),fo,1)")]
        [DataRow(@"T: functor(1,X,Y), X=1, Y=0")]
        [DataRow(@"T: functor(X,1.1,0), X=1.1")]
        [DataRow(@"T: functor([_|_],'.',2)")]
        [DataRow(@"T: functor([],[],0)")]
        [DataRow(@"R: functor(X, Y, 3)")]
        [DataRow(@"R: functor(X, foo, N)")]
        [DataRow(@"R: functor(X, foo, a)")]
        [DataRow(@"R: functor(F, 1.5, 1)")]
        [DataRow(@"R: functor(F,foo(a),1)")]
        [DataRow(@"R: current_prolog_flag(max_arity,A), X is A + 1, functor(T, foo, X)")]
        [DataRow(@"R: functor(T, foo, -1)")]
        public void Functor(string test)
        {
            test.Evaluate();
        }

        [TestMethod]
        [DataRow(@"T: '->'(true, true)")]
        [DataRow(@"F: '->'(true, fail)")]
        [DataRow(@"F: '->'(fail, true)")]
        [DataRow(@"T: '->'(true, X=1), X=1")]
        [DataRow(@"T: '->'((X=1; X=2), true), X=1")]
        [DataRow(@"T: '->'(true, (X=1; X=2)), (X=1;X=2)")]
        public void IfThen(string test)
        {
            test.Evaluate();
        }

        [TestMethod]
        [DataRow(@"T: '->'(true, true); fail")]
        [DataRow(@"T: '->'(fail, true); true")]
        [DataRow(@"F: '->'(true, fail); fail")]
        [DataRow(@"F: '->'(fail, true); fail")]
        [DataRow(@"T: ('->'(true, X=1); X=2), X=1")]
        [DataRow(@"T: ('->'(fail, X=1); X=2), X=2")]
        [DataRow(@"T: ('->'(true, (X=1; X=2)); true), (X=1 ; X=2)")]
        [DataRow(@"T: ('->'((X=1; X=2), true); true), X=1")]
        public void IfThenElse(string test)
        {
            test.Evaluate();
        }

        [TestMethod]
        [DataRow(@"T: integer(3)")]
        [DataRow(@"T: integer(-3)")]
        [DataRow(@"F: integer(3.3)")]
        [DataRow(@"F: integer(X)")]
        [DataRow(@"F: integer(atom)")]
        public void Integer(string test)
        {
            test.Evaluate();
        }

        [TestMethod]
        [DataRow(@"T: 'is'(Result,3 + 11.0), Result=14.0")]
        [DataRow(@"T: X = 1 + 2, 'is'(Y, X * 3), X = 1+2, Y=9")]
        [DataRow(@"F: 'is'(foo,77)")]
        [DataRow(@"R: 'is'(77, N)")]
        [DataRow(@"R: 'is'(77, foooo)")]
        [DataRow(@"F: 'is'(X,float(3)), X=3")]
        [DataRow(@"T: 'is'(X,float(3)), X=3.0")]
        [DataRow(@"T: X is 6 + 7, X = 13")]
        [DataRow(@"T: X is max(6,7), X = 7")]
        [DataRow(@"T: X is floor(4.42), X = 4")]
        public void Is(string test)
        {
            test.Evaluate();
        }

        [TestMethod]
        [DataRow(@"T: nonvar(33.3)")]
        [DataRow(@"T: nonvar(foo)")]
        [DataRow(@"F: nonvar(Foo)")]
        [DataRow(@"T: foo=Foo,nonvar(Foo), Foo=foo")]
        [DataRow(@"F: nonvar(_)")]
        [DataRow(@"T: nonvar(a(b))")]
        public void NoVar(string test)
        {
            test.Evaluate();
        }

        [TestMethod]
        [DataRow(@"F: \+(true)")]
        [DataRow(@"F: \+(!)")]
        [DataRow(@"T: (X=1;X=2), \+((!,fail)), (X=1;X=2)")]
        [DataRow(@"T: \+(4 = 5)")]
        [DataRow(@"R: \+(3)")]
        [DataRow(@"T: \+((!,fail))")]
        [DataRow(@"R: \+(X)")]
        public void NotProvable(string test)
        {
            test.Evaluate();
        }

        [TestMethod]
        [DataRow(@"F: '\='(1,1)")]
        [DataRow(@"F: '\='(X,1)")]
        [DataRow(@"F: '\='(X,Y)")]
        [DataRow(@"F: '\='(X,Y),'\='(X,abc)")]
        [DataRow(@"F: '\='(f(X,def),f(def,Y))")]
        [DataRow(@"T: '\='(1,2)")]
        [DataRow(@"T: '\='(1,1.0)")]
        [DataRow(@"T: '\='(g(X),f(f(X)))")]
        [DataRow(@"T: '\='(f(X,1),f(a(X)))")]
        [DataRow(@"T: '\='(f(X,Y,X),f(a(X),a(Y),Y,2))")]
        public void NotUnify(string test)
        {
            test.Evaluate();
        }

        [TestMethod]
        [DataRow(@"T: number(3)")]
        [DataRow(@"T: number(3.3)")]
        [DataRow(@"T: number(-3)")]
        [DataRow(@"F: number(a)")]
        [DataRow(@"F: number(X)")]
        public void Number(string test)
        {
            test.Evaluate();
        }

        [TestMethod]
        [DataRow(@"T: once(!)")]
        [DataRow(@"T: once(!), (X=1; X=2), (X=1;X=2)")]
        [DataRow(@"T: once(repeat)")]
        [DataRow(@"F: once(fail)")]
        [DataRow(@"R: once(3)")]
        [DataRow(@"R: once(X)")]
        public void Once(string test)
        {
            test.Evaluate();
        }

        [TestMethod]
        [DataRow(@"T: true; fail")]
        [DataRow(@"F: (!, fail); true")]
        [DataRow(@"T: (!; call(3))")]
        [DataRow(@"T: ((X=1, !); X=2), X=1")]
        [DataRow(@"T: (X=1; X=2)")]
        public void Or(string test)
        {
            test.Evaluate();
        }

        [TestMethod]
        [DataRow(@"F: repeat,!,fail")]
        public void Repeat(string test)
        {
            test.Evaluate();
        }

        [TestMethod]
        [DataRow(@"F: retract((4 :- X))")]
        [DataRow(@"F: retract((atom(_) :- X == '[]'))")]
        public void Retract(string test)
        {
            test.Evaluate();
        }

        [TestMethod]
        [DataRow(@"T: setof(X, (X = 1; X=2),L), L=[1, 2]")]
        [DataRow(@"T: setof(X, (X = 1; X=2),X), X=[1, 2]")]
        [DataRow(@"T: setof(X, (X = 2; X=1),L), L=[1, 2]")]
        [DataRow(@"T: setof(X, (X = 2; X=2),L), L=[2]")]
        [DataRow(@"F: setof(X, fail, L)")]
        [DataRow(@"T: setof(1, (Y = 2; Y=1),L), ((L=[1],Y=1);(L=[1],Y=2))")]
        [DataRow(@"T: setof(f(X, Y), (X = a;Y = b), L), L=[f(_,b), f(a,_)]")]
        [DataRow(@"T: setof(X, Y ^ ((X = 1, Y = 1);(X=2,Y=2)),S), S=[1, 2]")]
        [DataRow(@"T: setof(X, Y ^ ((X = 1; Y=1);(X=2,Y=2)),S), S=[_, 1, 2]")]
        [DataRow(@"T: setof(X, (Y ^ (X = 1; Y = 1); X = 3),S), S=[3]")]
        [DataRow(@"T: setof(X, Y ^ (X = 1; Y = 1; X = 3),S), S=[_,1,3]")]
        [DataRow(@"T: setof(X, (X = Y; X=Z;Y=1),L), (L=[Y,Z]; (L=[_],Y=1))")]
        [DataRow(@"R: setof(X, X ^ (true; 4), L)")]
        [DataRow(@"R: setof(X, 1, L)")]
        public void Setof(string test)
        {
            test.Evaluate();
        }

        [TestMethod]
        [DataRow(@"F: '\=='(1,1)")]
        [DataRow(@"F: '\=='(X, X)")]
        [DataRow(@"T: '\=='(1,2)")]
        [DataRow(@"T: '\=='(X,1)")]
        [DataRow(@"T: '\=='(X, Y)")]
        [DataRow(@"T: '\=='(_, _)")]
        [DataRow(@"T: '\=='(X, a(X))")]
        [DataRow(@"F: '\=='(f(a), f(a))")]
        public void TermDiff(string test)
        {
            test.Evaluate();
        }

        [TestMethod]
        [DataRow(@"T: '=='(1,1)")]
        [DataRow(@"T: '=='(X, X)")]
        [DataRow(@"F: '=='(1,2)")]
        [DataRow(@"F: '=='(X,1)")]
        [DataRow(@"F: '=='(X, Y)")]
        [DataRow(@"F: '=='(_, _)")]
        [DataRow(@"F: '=='(X, a(X))")]
        [DataRow(@"T: '=='(f(a), f(a))")]
        public void TermEq(string test)
        {
            test.Evaluate();
        }

        [TestMethod]
        [DataRow(@"F: '@>'(1.0,1)")]
        [DataRow(@"F: '@>'(aardvark, zebra)")]
        [DataRow(@"F: '@>'(short, short)")]
        [DataRow(@"F: '@>'(short, shorter)")]
        [DataRow(@"T: '@>'(foo(b), foo(a))")]
        [DataRow(@"F: '@>'(X, X)")]
        [DataRow(@"F: '@>'(foo(a, X), foo(b, Y))")]
        [DataRow(@"F: '@>='(1.0,1)")]
        [DataRow(@"F: '@>='(aardvark, zebra)")]
        [DataRow(@"T: '@>='(short, short)")]
        [DataRow(@"F: '@>='(short, shorter)")]
        [DataRow(@"T: '@>='(foo(b), foo(a))")]
        [DataRow(@"T: '@>='(X, X)")]
        [DataRow(@"F: '@>='(foo(a, X), foo(b, Y))")]
        public void TermGt(string test)
        {
            test.Evaluate();
        }

        [TestMethod]
        [DataRow(@"T: '@<'(1.0,1)")]
        [DataRow(@"T: '@<'(aardvark, zebra)")]
        [DataRow(@"F: '@<'(short, short)")]
        [DataRow(@"T: '@<'(short, shorter)")]
        [DataRow(@"F: '@<'(foo(b), foo(a))")]
        [DataRow(@"F: '@<'(X, X)")]
        [DataRow(@"T: '@<'(foo(a, X), foo(b, Y))")]
        [DataRow(@"T: '@=<'(1.0,1)")]
        [DataRow(@"T: '@=<'(aardvark, zebra)")]
        [DataRow(@"T: '@=<'(short, short)")]
        [DataRow(@"T: '@=<'(short, shorter)")]
        [DataRow(@"F: '@=<'(foo(b), foo(a))")]
        [DataRow(@"T: '@=<'(X, X)")]
        [DataRow(@"T: '@=<'(foo(a, X), foo(b, Y))")]
        public void TermLt(string test)
        {
            test.Evaluate();
        }

        [TestMethod]
        [DataRow(@"T: true")]
        public void True(string test)
        {
            test.Evaluate();
        }

        [TestMethod]
        [DataRow(@"T: '='(1,1)")]
        [DataRow(@"T: 1=1")]
        [DataRow(@"T: '='(X,1)")]
        [DataRow(@"T: '='(X,Y), X=a, Y='a'")]
        [DataRow(@"T: '='(X, Y), '='(X, abc), (X=abc; Y=abc)")]
        [DataRow(@"T: '='(f(X, def), f(def, Y)), (X=def; Y=def)")]
        [DataRow(@"F: '='(1,2)")]
        [DataRow(@"F: '='(1,1.0)")]
        [DataRow(@"F: '='(1,1.1)")]
        [DataRow(@"F: '=='(1,1.0)")]
        [DataRow(@"F: '=='(1,1.1)")]
        [DataRow(@"T: '=:='(1,1.0)")]
        [DataRow(@"F: '=:='(1,1.1)")]
        [DataRow(@"F: '='(g(X), f(f(X)))")]
        [DataRow(@"F: '='(f(X, 1), f(a(X)))")]
        [DataRow(@"F: '='(f(X, Y, X), f(a(X), a(Y), Y, 2))")]
        [DataRow(
            @"T: '='(f(A, B, C), f(g(B, B), g(C, C), g(D, D))), A=g(g(g(D, D), g(D, D)), g(g(D, D), g(D, D))), B=g(g(D, D), g(D, D)), C=g(D, D)")]
        public void Unify(string test)
        {
            test.Evaluate();
        }
    }
}