/*
 * Copyright 2013 S. Webber
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */
using Org.NProlog.Core.Terms;

namespace Org.NProlog.Core.Predicate.Builtin.List;


/* TEST
%TRUE reverse([a],[a])
%TRUE reverse([a,b,c],[c,b,a])
%?- reverse([a,b,c],X)
% X=[c,b,a]
%?- reverse(X,[a,b,c])
% X=[c,b,a]

%TRUE reverse([],[])
%?- reverse([],X)
% X=[]
%?- reverse(X,[])
% X=[]

%FAIL reverse([a,b,c],[])
%FAIL reverse([a,b,c],[a,b,c])
%FAIL reverse([a,b,c],[d,c,b,a])
%FAIL reverse([a,b,c,d],[c,b,a])
%FAIL reverse([a,b,c],'abc')
%FAIL reverse('abc',X)

%FAIL reverse([a,b|c],X)

%?- reverse([a|X], Y)
% X=[]
% Y=[a]
% X=[_]
% Y=[_,a]
% X=[_,_]
% Y=[_,_,a]
% X=[_,_,_]
% Y=[_,_,_,a]
%QUIT

%?- reverse([z,x,x,c,b|X],[a,b,c|Y])
% X=[a]
% Y=[x,x,z]
% X=[c,b,a]
% Y=[b,c,x,x,z]
% X=[_,c,b,a]
% Y=[_,b,c,x,x,z]
% X=[_,_,c,b,a]
% Y=[_,_,b,c,x,x,z]
% X=[_,_,_,c,b,a]
% Y=[_,_,_,b,c,x,x,z]
%QUIT

%?- reverse([a,b,c|Y],[z,x,x,c,b|X])
% X=[a]
% Y=[x,x,z]
% X=[c,b,a]
% Y=[b,c,x,x,z]
% X=[_,c,b,a]
% Y=[_,b,c,x,x,z]
% X=[_,_,c,b,a]
% Y=[_,_,b,c,x,x,z]
% X=[_,_,_,c,b,a]
% Y=[_,_,_,b,c,x,x,z]
%QUIT

%?- reverse([c,b,a|X],[c,b,a|Y])
% X=[b,c]
% Y=[b,c]
% X=[a,b,c]
% Y=[a,b,c]
% X=[_,a,b,c]
% Y=[_,a,b,c]
% X=[_,_,a,b,c]
% Y=[_,_,a,b,c]
% X=[_,_,_,a,b,c]
% Y=[_,_,_,a,b,c]
%QUIT

%?- reverse([b,c|X],[a,b,c|Y])
% X=[b,a]
% Y=[b]
% X=[c,b,a]
% Y=[c,b]
% X=[_,c,b,a]
% Y=[_,c,b]
% X=[_,_,c,b,a]
% Y=[_,_,c,b]
% X=[_,_,_,c,b,a]
% Y=[_,_,_,c,b]
%QUIT

%?- reverse([a,b,c|Y],[b,c|X])
% X=[b,a]
% Y=[b]
% X=[c,b,a]
% Y=[c,b]
% X=[_,c,b,a]
% Y=[_,c,b]
% X=[_,_,c,b,a]
% Y=[_,_,c,b]
% X=[_,_,_,c,b,a]
% Y=[_,_,_,c,b]
%QUIT

%?- X=[d,c|Q],Y=[a,b,c|R],reverse(X,Y)
% Q=[b,a]
% R=[d]
% X=[d,c,b,a]
% Y=[a,b,c,d]
% Q=[c,b,a]
% R=[c,d]
% X=[d,c,c,b,a]
% Y=[a,b,c,c,d]
% Q=[_,c,b,a]
% R=[_,c,d]
% X=[d,c,_,c,b,a]
% Y=[a,b,c,_,c,d]
% Q=[_,_,c,b,a]
% R=[_,_,c,d]
% X=[d,c,_,_,c,b,a]
% Y=[a,b,c,_,_,c,d]
% Q=[_,_,_,c,b,a]
% R=[_,_,_,c,d]
% X=[d,c,_,_,_,c,b,a]
% Y=[a,b,c,_,_,_,c,d]
%QUIT

%?- X=[d,c|Q],Y=[a,b,c|R],reverse(Y,X)
% Q=[b,a]
% R=[d]
% X=[d,c,b,a]
% Y=[a,b,c,d]
% Q=[c,b,a]
% R=[c,d]
% X=[d,c,c,b,a]
% Y=[a,b,c,c,d]
% Q=[_,c,b,a]
% R=[_,c,d]
% X=[d,c,_,c,b,a]
% Y=[a,b,c,_,c,d]
% Q=[_,_,c,b,a]
% R=[_,_,c,d]
% X=[d,c,_,_,c,b,a]
% Y=[a,b,c,_,_,c,d]
% Q=[_,_,_,c,b,a]
% R=[_,_,_,c,d]
% X=[d,c,_,_,_,c,b,a]
% Y=[a,b,c,_,_,_,c,d]
%QUIT

%?- X=[e,d|Q],Y=[a,b,c|R],reverse(X,Y)
% Q=[c,b,a]
% R=[d,e]
% X=[e,d,c,b,a]
% Y=[a,b,c,d,e]
% Q=[_,c,b,a]
% R=[_,d,e]
% X=[e,d,_,c,b,a]
% Y=[a,b,c,_,d,e]
% Q=[_,_,c,b,a]
% R=[_,_,d,e]
% X=[e,d,_,_,c,b,a]
% Y=[a,b,c,_,_,d,e]
% Q=[_,_,_,c,b,a]
% R=[_,_,_,d,e]
% X=[e,d,_,_,_,c,b,a]
% Y=[a,b,c,_,_,_,d,e]
%QUIT

%?- X=[e,d|Q],Y=[a,b,c|R],reverse(Y,X)
% Q=[c,b,a]
% R=[d,e]
% X=[e,d,c,b,a]
% Y=[a,b,c,d,e]
% Q=[_,c,b,a]
% R=[_,d,e]
% X=[e,d,_,c,b,a]
% Y=[a,b,c,_,d,e]
% Q=[_,_,c,b,a]
% R=[_,_,d,e]
% X=[e,d,_,_,c,b,a]
% Y=[a,b,c,_,_,d,e]
% Q=[_,_,_,c,b,a]
% R=[_,_,_,d,e]
% X=[e,d,_,_,_,c,b,a]
% Y=[a,b,c,_,_,_,d,e]
%QUIT

%?- reverse([a,b,c|Y],[X|Z])
% X=c
% Y=[]
% Z=[b,a]
% X=UNINSTANTIATED VARIABLE
% Y=[X]
% Z=[c,b,a]
% X=UNINSTANTIATED VARIABLE
% Y=[_,X]
% Z=[_,c,b,a]
% X=UNINSTANTIATED VARIABLE
% Y=[_,_,X]
% Z=[_,_,c,b,a]
%QUIT

%?- reverse([a,b|X],[a,b|X])
% X=[a]
% X=[b,a]
% X=[X,b,a]
% X=[X,X,b,a]
% X=[X,X,X,b,a]
%QUIT

%?- reverse(X,X), numbervars(X)
% X=[]
% X=[$VAR(0)]
% X=[$VAR(0),$VAR(0)]
% X=[$VAR(0),$VAR(1),$VAR(0)]
% X=[$VAR(0),$VAR(1),$VAR(1),$VAR(0)]
% X=[$VAR(0),$VAR(1),$VAR(2),$VAR(1),$VAR(0)]
% X=[$VAR(0),$VAR(1),$VAR(2),$VAR(2),$VAR(1),$VAR(0)]
%QUIT
*/
/**
 * <code>reverse(X,Y)</code> - reverses the order of elements in a list.
 * <p>
 * The <code>reverse(X,Y)</code> goal succeeds if the elements of list <code>X</code> are in reverse order of the
 * elements in list <code>Y</code>.
 * </p>
 */
public class Reverse : AbstractPredicateFactory
{

    protected override Predicate GetPredicate(Term list1, Term list2) 
        => new ReversePredicate(list1, list2);

    public class ReversePredicate : Predicate
    {
        Term arg1;
        Term arg2;
        Term arg3;
        Term arg4;
        bool retrying;

        public ReversePredicate(Term list1, Term list2)
        {
            // reverse(Xs, [], Ys, Ys).
            this.arg1 = list1;
            this.arg2 = EmptyList.EMPTY_LIST;
            this.arg3 = list2;
            this.arg4 = list2;
        }


        public virtual bool Evaluate()
        {
            while (true)
            {
                // reverse([], Ys, Ys, []).
                if (!retrying && arg1.Unify(EmptyList.EMPTY_LIST) && arg4.Unify(EmptyList.EMPTY_LIST) && arg2.Unify(arg3))
                {
                    retrying = true;
                    return true;
                }
                retrying = false;

                arg1.Backtrack();
                arg2.Backtrack();
                arg3.Backtrack();
                arg4.Backtrack();

                // reverse([X|Xs], Rs, Ys, [_|Bound]) :-
                //   reverse(Xs, [X|Rs], Ys, Bound).
                Term x;
                Term xs;
                if (arg1.Type == TermType.LIST)
                {
                    x = arg1.GetArgument(0);
                    xs = arg1.GetArgument(1);
                }
                else if (arg1.Type.IsVariable)
                {
                    x = new Variable("X");
                    xs = new Variable("Xs");
                    arg1.Unify(new Terms.List(x, xs));
                }
                else
                {
                    return false;
                }

                if (arg4.Type == TermType.LIST)
                {
                    arg4 = arg4.GetArgument(1);
                }
                else if (arg4.Type.IsVariable)
                {
                    Variable v = new Variable("Bound");
                    arg4.Unify(new Terms.List(new Variable(), v));
                    arg4 = v;
                }
                else
                {
                    return false;
                }

                arg1 = xs.Term;
                arg2 = new Terms.List(x.Term, arg2.Term);
                arg3 = arg3.Term;
                arg4 = arg4.Term;
            }
        }


        public virtual bool CouldReevaluationSucceed => !retrying || (arg1 != EmptyList.EMPTY_LIST && arg4 != EmptyList.EMPTY_LIST);
    }
}
