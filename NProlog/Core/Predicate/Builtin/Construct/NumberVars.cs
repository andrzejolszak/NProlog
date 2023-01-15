/*
 * Copyright 2018 S. Webber
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

namespace Org.NProlog.Core.Predicate.Builtin.Construct;



/* TEST
%?- numbervars(a,0,Y)
% Y=0

%?- numbervars(X,0,Y)
% X=$VAR(0)
% Y=1

%?- numbervars(X,42,Y)
% X=$VAR(42)
% Y=43

%?- X=p(A,B,p(C,B,C,D,[E,A,F,F,G,D])), numbervars(X,0,Y)
% A=$VAR(0)
% B=$VAR(1)
% C=$VAR(2)
% D=$VAR(3)
% E=$VAR(4)
% F=$VAR(5)
% G=$VAR(6)
% X=p($VAR(0), $VAR(1), p($VAR(2), $VAR(1), $VAR(2), $VAR(3), [$VAR(4),$VAR(0),$VAR(5),$VAR(5),$VAR(6),$VAR(3)]))
% Y=7

%?- X=p(A,B,p(C,B,C,D,[E,A,F,F,G,D])), numbervars(X,42,Y)
% A=$VAR(42)
% B=$VAR(43)
% C=$VAR(44)
% D=$VAR(45)
% E=$VAR(46)
% F=$VAR(47)
% G=$VAR(48)
% X=p($VAR(42), $VAR(43), p($VAR(44), $VAR(43), $VAR(44), $VAR(45), [$VAR(46),$VAR(42),$VAR(47),$VAR(47),$VAR(48),$VAR(45)]))
% Y=49

% numbervars(X) operates in the same way as numbervars(X,0,_)

%?- X=p(A,B,p(C,B,C,D,[E,A,F,F,G,D])), numbervars(X)
% A=$VAR(0)
% B=$VAR(1)
% C=$VAR(2)
% D=$VAR(3)
% E=$VAR(4)
% F=$VAR(5)
% G=$VAR(6)
% X=p($VAR(0), $VAR(1), p($VAR(2), $VAR(1), $VAR(2), $VAR(3), [$VAR(4),$VAR(0),$VAR(5),$VAR(5),$VAR(6),$VAR(3)]))

%?- X=p(A,B,p(x(x(x(A,p(C),B,p(D,B))))),E), numbervars(X,0,Y)
% A=$VAR(0)
% B=$VAR(1)
% C=$VAR(2)
% D=$VAR(3)
% E=$VAR(4)
% X=p($VAR(0), $VAR(1), p(x(x(x($VAR(0), p($VAR(2)), $VAR(1), p($VAR(3), $VAR(1)))))), $VAR(4))
% Y=5

%?- X=p(A,B,p(x(x(x(A,p(C),B,p(D,B))))),E), numbervars(X,-42,Y)
% A=$VAR(-42)
% B=$VAR(-41)
% C=$VAR(-40)
% D=$VAR(-39)
% E=$VAR(-38)
% X=p($VAR(-42), $VAR(-41), p(x(x(x($VAR(-42), p($VAR(-40)), $VAR(-41), p($VAR(-39), $VAR(-41)))))), $VAR(-38))
% Y=-37
*/
/**
 * <code>numbervars(Term,Start,End)</code> - unifies free variables of a term.
 * <p>
 * Unifies the free variables in <code>Term</code> with a structure of the form <code>$VAR(N)</code> where
 * <code>N</code> is the number of the variable. Numbering of the variables starts with the numeric value represented by
 * <code>Start</code>. <code>End</code> is unified with the number that would of been given to the next variable.
 * </p>
 */
public class NumberVars : AbstractSingleResultPredicate
{

    protected override bool Evaluate(Term arg)
    {
        DoNumberVars(arg, 0);
        return true;
    }


    protected override bool Evaluate(Term arg1, Term arg2, Term arg3)
    {
        var start = TermUtils.CastToNumeric(arg2).Long;
        var end = DoNumberVars(arg1, start);
        return arg3.Unify(IntegerNumberCache.ValueOf(end));
    }

    private static long DoNumberVars(Term term, long start)
    {
        if (term.IsImmutable) return start;

        Stack<Term> stack = new();
        long ctr = start;
        stack.Push(term);
        while (stack.Count > 0)
        {
            var next = stack.Pop();
            if (next.Type.isVariable)
            {
                next.Unify(Structure.CreateStructure("$VAR", new Term[] { IntegerNumberCache.ValueOf(ctr++) }));
            }
            else
            {
                for (int i = next.NumberOfArguments - 1; i > -1; i--)
                {
                    var child = next.GetArgument(i);
                    if (!child.IsImmutable)
                        stack.Push(child);
                }
            }
        }
        return ctr;
    }
}
