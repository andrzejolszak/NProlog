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

namespace Org.NProlog.Core.Predicate.Builtin.Compare;


/* TEST
%FAIL X == Y
%FAIL _ == _

%?- X == X
% X=UNINSTANTIATED VARIABLE

%?- _1 == _1
% _1=UNINSTANTIATED VARIABLE

%?- X = Y, X == Y, Y = 1
% X=1
% Y=1

%FAIL X == Y, Y = 1, X = Y

%TRUE a=a
%TRUE 1=1
%TRUE 1.0=1.0
%FAIL 1=1.0
%TRUE '+'(1,2)=1+2

%FAIL Append([A|B],C) == Append(X,Y)

%?- Append([A|B],C) == Append([A|B],C)
% A=UNINSTANTIATED VARIABLE
% B=UNINSTANTIATED VARIABLE
% C=UNINSTANTIATED VARIABLE
*/
/**
 * <code>X==Y</code> - a strict equality test.
 * <p>
 * If <code>X</code> can be matched with <code>Y</code> the goal succeeds else the goal fails. A <code>X==Y</code> goal
 * will only consider an uninstantiated variable to be equal to another uninstantiated variable that is already sharing
 * with it.
 * </p>
 */
public class StrictEquality : AbstractSingleResultPredicate
{

    protected override bool Evaluate(Term arg1, Term arg2)
        => TermUtils.TermsEqual(arg1, arg2);
}
