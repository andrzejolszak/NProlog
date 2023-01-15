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
namespace Org.NProlog.Core.Predicate.Builtin.Flow;


/* TEST
%TRUE_NO repeat, !

Write_first_sentence(X) :-
   atom_chars(X, Chars), member(Next, Chars), write(Next), Next=='.', !.

%?- Write_first_sentence('word1 word2 word3. word4 word5 word6.')
%OUTPUT word1 word2 word3.
%YES

a(x, Y) :- Y = 1, !.
a(X, Y) :- Y = 2.

%?- a(x, Y)
% Y=1

%?- a(y, Y)
% Y=2

%?- a(z, Y)
% Y=2
*/
/**
 * <code>!</code> - the "cut".
 * <p>
 * The "cut", represented as a <code>!</code>, is a special mechanism which affects how prolog backtracks.
 * </p>
 */
public class Cut : AbstractPredicateFactory
{

    protected override Predicate GetPredicate() => new CutPredicate();


    public virtual bool IsAlwaysCutOnBacktrack => true;

    private class CutPredicate : Predicate
    {
        private bool retried = false;

        public bool Evaluate()
        {
            if (retried)
                throw CutException.CUT_EXCEPTION;
            retried = true;
            return true;
        }


        public bool CouldReevaluationSucceed => true;
    }
}
